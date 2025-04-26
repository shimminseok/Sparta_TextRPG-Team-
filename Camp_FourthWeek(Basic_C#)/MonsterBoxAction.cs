namespace Camp_FourthWeek_Basic_C__;

public class MonsterBoxAction : PagedListActionBase
{
    public override string Name => "포켓몬 박스";

    public MonsterBoxAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 101, new TestMonsterModify(this) }
        };
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        //output.Add("포켓몬에 관한 행동을 볼 수 있습니다.");

        var monsters = InventoryManager.Instance.MonsterBox;
        int totalMonster = monsters.Count;
        MaxPage = (int)Math.Ceiling(totalMonster / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, totalMonster);
        int index = 1;
        for (int i = pageStart; i < pageEnd; i++)
        {
            var m = monsters[i];
            var isEquipped = (m == PlayerInfo.Monster) ? "[E]" : "   ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.Item == null ? "없음" : m.Item.Name;

            output.Add($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");
            SubActionMap[index++] = new ChangeMonsterSelectAction(m, this);
        }

        return output;
    }

    public override void OnExcute()
    {
        var lines = GetPageContent();
        base.OnExcute();
        int LineCount = 7;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount++, lines[i]);
        }

        lineDic[10] = "";
        // if (Page > 0)
        lineDic[11] = Page > 0 ? "-1. 이전 페이지" : "";
        // if (Page < MaxPage - 1)
        lineDic[12] = Page < MaxPage - 1 ? "-2. 다음 페이지" : "";
        SelectAndRunAction(SubActionMap, isViewSubMap,
            () => UiManager.UIUpdater(UIName.SetPokectmon, null, (5, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new MonsterBoxAction(PrevAction, newPage);
    }
}