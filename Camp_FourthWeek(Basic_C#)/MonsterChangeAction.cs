namespace Camp_FourthWeek_Basic_C__;

public class MonsterChangeAction : PagedListActionBase
{
    public override string Name => "포켓몬 관리";

    public MonsterChangeAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>()
        {
            //테스트용 포켓몬 추가
            { 99, new TestMonsterModify(this) },
        };
        isViewSubMap = false;
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        output.Add("이곳에서 포켓몬을 교체 할 수 있습니다.");
        output.Add("");
        var monsters = InventoryManager.Instance.MonsterBox;
        var totalMonster = monsters.Count;
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, totalMonster);
        MaxPage = (int)Math.Ceiling(totalMonster / (float)VIEW_COUNT);

        int index = 1;

        for (int i = pageStart; i < pageEnd; i++)
        {
            var m = InventoryManager.Instance.MonsterBox[i];
            var isEquipped = (m == PlayerInfo.Monster) ? "[E]" : "   ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.Item == null ? "없음" : m.Item.Name;
            output.Add($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");

            if (!SubActionMap.ContainsKey(index))
            {
                SubActionMap[index] = new ChangeMonsterSelectAction(m, this);
            }

            index++;
        }

        output.Add("99. [테스트용 랜덤 포켓몬 추가]");
        output.Add("");


        return output;
    }
    public override void OnExcute()
    {
        base.OnExcute();
        SubActionMap.Clear();
        var lines = GetPageContent();
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
        Console.WriteLine($"[{Page + 1}/{MaxPage}] 페이지");
        Console.WriteLine();

        if (Page > 0)
            Console.WriteLine("-1. 이전 페이지");
        if (Page < MaxPage - 1)
            Console.WriteLine("-2. 다음 페이지");
        if (isView)
        {
            SelectAndRunAction(SubActionMap, isViewSubMap);
        }
        else if (!isView)
        {
            SelectAndRunAction(SubActionMap, false);
        }

    }
    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new MonsterChangeAction(PrevAction, newPage);
    }
}