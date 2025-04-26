namespace Camp_FourthWeek_Basic_C__;

public class MonsterChangeAction : PagedListActionBase
{
    public override string Name => "포켓몬 관리";

    public MonsterChangeAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        isViewSubMap = false;
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
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

        return output;
    }

    public override void OnExcute()
    {
        base.OnExcute();
        int LineCount = 7;
        var lines = GetPageContent();
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount + i, lines[i]);
        }

        if (Page > 0)
            lineDic.Add(8, "-1. 이전 페이지");
        if (Page < MaxPage - 1)
            lineDic.Add(9, "-2. 다음 페이지");
        SelectAndRunAction(SubActionMap, isViewSubMap,
            () => UiManager.UIUpdater(UIName.SetPokectmon, null, (5, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new MonsterChangeAction(PrevAction, newPage);
    }
}