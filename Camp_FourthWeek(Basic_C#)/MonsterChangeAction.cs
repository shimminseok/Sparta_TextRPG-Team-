namespace Camp_FourthWeek_Basic_C__;

public class MonsterChangeAction : ActionBase
{
    public override string Name => "포켓몬 관리";
    private int curPokemonPage = 0;
    private const int PagepokemonSize = 3;

    public MonsterChangeAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>()
        {
            //테스트용 포켓몬 추가
            { 99, new TestMonsterModify(this) },
        };
    }

    public override void OnExcute()
    {
        Console.WriteLine("이곳에서 포켓몬을 교체 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[포켓몬 목록]");

        var monsters = InventoryManager.Instance.MonsterBox;
        var totalMonster = monsters.Count;
        int maxPage = (int)Math.Ceiling(totalMonster / (float)PagepokemonSize);
        int pageStart = curPokemonPage * PagepokemonSize;
        int pageEnd = Math.Min(pageStart + PagepokemonSize, totalMonster);

        int index = 1;

        for (int i = pageStart; i < pageEnd; i++)
        {
            var m = InventoryManager.Instance.MonsterBox[i];
            var isEquipped = (m == PlayerInfo.Monster) ? "[E]" : " ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.ItemId != 0 ? ItemTable.GetItemById(m.ItemId)?.Name ?? "알수 없음" : "없음";
            Console.WriteLine($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");

            if (!SubActionMap.ContainsKey(index))
            {
                SubActionMap[index] = new ChangeMonsterSelectAction(m, this);
            }

            index++;
        }

        Console.WriteLine();
        Console.WriteLine("-1. 다음페이지");
        Console.WriteLine("-2. 이전페이지");
        Console.WriteLine();
        SubActionMap[-1] = new PokemonChangePageMoveAction(this, this, 1);
        SubActionMap[-2] = new PokemonChangePageMoveAction(this, this, -1);

        SelectAndRunAction(SubActionMap, true);
    }

    public void MovePage(int _dir)
    {
        SetFeedBackMessage("");
        var totalPage = InventoryManager.Instance.MonsterBox.Count;
        int maxPage = (int)Math.Ceiling(totalPage / (float)PagepokemonSize);
        int nextPage = curPokemonPage + _dir;

        if (nextPage < 0 || nextPage >= maxPage)
        {
            SetFeedBackMessage("더이상 페이지가 없습니다.");
            return;
        }

        curPokemonPage = nextPage;
    }
}