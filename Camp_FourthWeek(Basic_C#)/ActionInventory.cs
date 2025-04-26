namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EnterInventoryAction : ActionBase
{
    public EnterInventoryAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new MonsterBoxAction(this) },
            { 2, new EquipItemManagementAction(this) },
            { 3, new UseItemAction(this) },
        };
    }

    public override string Name => "인벤토리";

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Inventory));
    }
}

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
        lineDic[11] = Page > 0 ? "-1. 이전 페이지" : "";
        lineDic[12] = Page < MaxPage - 1 ? "-2. 다음 페이지" : "";
        SelectAndRunAction(SubActionMap, isViewSubMap,
            () => UiManager.UIUpdater(UIName.SetPokectmon, null, (5, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new MonsterBoxAction(PrevAction, newPage);
    }
}

public class ChangeMonsterSelectAction : ActionBase
{
    private Monster monster;

    public ChangeMonsterSelectAction(Monster _monster, IAction _prevAction)
    {
        monster = _monster;
        NextAction = _prevAction;
    }

    public override string Name => string.Empty;

    public override void OnExcute()
    {
        GameManager.Instance.PlayerInfo.ChangeMonsterStat(monster);
    }
}

public class EquipItemManagementAction : PagedListActionBase
{
    public override string Name => "도구관리";


    public EquipItemManagementAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
    }


    protected override List<string> GetPageContent()
    {
        SubActionMap.Clear();
        MaxPage = (int)Math.Ceiling(InventoryManager.Instance.Inventory.Count / (float)VIEW_COUNT);
        var output = new List<string>();


        var inventory = InventoryManager.Instance.Inventory;
        var currentMonster = PlayerInfo.Monster;

        int start = Page * VIEW_COUNT;
        int end = Math.Min(start + VIEW_COUNT, inventory.Count);

        for (var i = start; i < end; i++)
        {
            var item = inventory[i];
            var sb = new StringBuilder();
            sb.Append($"{PadRightWithKorean($"- {i + 1}", 5)}");


            if (item.IsEquippedBy(currentMonster))
            {
                sb.Append("[E]");
            }

            sb.Append($"{PadRightWithKorean($"{item.Name}", 10)}");
            for (var j = 0; j < item.Stats.Count; j++)
            {
                sb.Append(
                    $" | {PadRightWithKorean($"{item.Stats[j].GetStatName()} +{item.Stats[j].FinalValue}", 10)} ");
                sb.Append(" | ");
            }

            Monster? monster = InventoryManager.Instance.MonsterBox.FirstOrDefault(m => m.Item == item);
            string equippedMonsterName = string.Empty;

            if (item.IsEquipment)
            {
                if (monster != null)
                    sb.Append($"{PadRightWithKorean($"장착 중인 포켓몬: {monster.Name}", 25)} | ");
            }

            sb.Append($"{PadRightWithKorean($"{item.Description}{equippedMonsterName}", 20)}");

            output.Add(sb.ToString());

            bool equippedByOther = monster != null && monster != currentMonster;
            if (!equippedByOther)
                SubActionMap[i + 1] = new EquipAction(item, this);
        }

        return output;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        var lines = GetPageContent();
        base.OnExcute();
        int LineCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount++, lines[i]);
        }

        for (int i = LineCount; i < 9; i++)
        {
            lineDic.Add(LineCount++, "");
        }

        lineDic[9] = Page > 0 ? "-1. 이전 페이지" : "";
        lineDic[10] = Page < MaxPage - 1 ? "-2. 다음 페이지" : "";

        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Equipment, null, (5, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EquipItemManagementAction(PrevAction, newPage);
    }
}

public class UseItemAction : ActionBase
{
    public override string Name => "열매 관리";
    private readonly int fruitCount = InventoryManager.Instance.FruitCount;

    public UseItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        var player = GameManager.Instance.PlayerInfo;
        var monster = player.Monster;
        var curHp = monster.Stats[StatType.CurHp].FinalValue;
        SubActionMap.Clear();
        if (fruitCount > 0)
        {
            SubActionMap[1] = new UseFruitAction(this);
        }
        else
        {
            Console.WriteLine("열매가 없습니다.");
        }

        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(5, $"이곳에서 열매를 사용하여 포켓몬의 체력을 {InventoryManager.HEAL_AMOUNT}회복시킬 수 있습니다. (열매 수: {fruitCount})");
        lineDic.Add(6, $"현재체력: {curHp}");


        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Fruit, null, (5, lineDic)));
    }
}

public class UseFruitAction : ActionBase
{
    public UseFruitAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        NextAction = PrevAction;
    }

    public override string Name => "열매 사용";

    public override void OnExcute()
    {
        InventoryManager.Instance.UseFruit();
    }
}

public class EquipAction : ActionBase
{
    private readonly Item item;

    public EquipAction(Item _item, IAction _prevAction)
    {
        item = _item;
        PrevAction = _prevAction;
        NextAction = _prevAction;
    }

    public override string Name => $"{item.Name} 아이템 {(item.IsEquipment ? "해제" : "장착")}";

    public override void OnExcute()
    {
        var player = PlayerInfo;
        var currentMonster = player.Monster;
        if (currentMonster.Item == item)
        {
            EquipmentManager.UnequipItem(currentMonster);
        }
        else
        {
            EquipmentManager.EquipmentItem(item);
        }
    }
}