namespace Camp_FourthWeek_Basic_C__;

public class MonsterBoxAction : PagedListActionBase
{
    public override string Name => "포켓몬 박스";

    public MonsterBoxAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new MonsterChangeAction(this) }
        };
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        output.Add("포켓몬에 관한 행동을 볼 수 있습니다.");

        var monsters = InventoryManager.Instance.MonsterBox;
        int totalMonster = monsters.Count;
        MaxPage = (int)Math.Ceiling(totalMonster / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, totalMonster);

        for (int i = pageStart; i < pageEnd; i++)
        {
            var m = monsters[i];
            var isEquipped = (m == PlayerInfo.Monster) ? "[E]" : "   ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.ItemId != 0 ? ItemTable.GetItemById(m.ItemId)?.Name ?? "알수 없음" : "없음";

            output.Add($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");
        }

        return output;
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new MonsterBoxAction(PrevAction, newPage);
    }
}