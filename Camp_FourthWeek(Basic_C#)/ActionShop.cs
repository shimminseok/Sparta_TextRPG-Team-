namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EnterShopAction : PagedListActionBase
{
    public List<Item> SaleItems = new();

    public EnterShopAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new BuyItemAction(this) },
            { 2, new SellItemAction(this) }
        };
        Page = _page;
        SaleItems = ItemTable.ItemDic.Values.ToList();
    }

    public override string Name => "상점";

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();

        MaxPage = (int)Math.Ceiling(SaleItems.Count / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, SaleItems.Count);
        for (var i = pageStart; i < pageEnd; i++)
        {
            var item = SaleItems[i];
            var sb = UiManager.ItemPrinter(item, i);
            sb.Append(" | ");
            if (InventoryManager.Instance.Inventory.Exists(x => x.Name == item.Name))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                sb.Append($"{PadRightWithKorean("구매완료", 8)}");
            }
            else
            {
                sb.Append($"{PadRightWithKorean($"{item.Cost}G", 6)}");
            }

            sb.Append(" | ");
            output.Add(sb.ToString());
            Console.ResetColor();
        }

        return output;
    }

    public override void OnExcute()
    {
        var lines = GetPageContent();
        base.OnExcute();

        int LineCount = 10;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(9, $"{PlayerInfo.Gold} G");

        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount + i, lines[i]);
        }

        if (Page > 0)
            lineDic.Add(13, "-1. 이전 페이지");
        if (Page < MaxPage - 1)
            lineDic.Add(14, "-2. 다음 페이지");

        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Shop_Main, null, (8, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EnterShopAction(PrevAction, newPage);
    }
}

public class BuyItemAction : PagedListActionBase
{
    private List<Item> SaleItems = new();

    public BuyItemAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SaleItems = ItemTable.ItemDic.Values.ToList();
        MaxPage = (int)Math.Ceiling(SaleItems.Count / (float)VIEW_COUNT);
    }

    public override string Name => "상점 - 아이템 구매";

    protected override List<string> GetPageContent()
    {
        isView = false;
        // SaleItems = ItemTable.ItemDic.Values
        //     .Where(item => !InventoryManager.Instance.Inventory.Any(x => x.Key == item.Key))
        //     .ToList();
        var output = new List<string>();
        SubActionMap.Clear();
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, SaleItems.Count);
        for (var i = 0; i < SaleItems.Count; i++)
        {
            SubActionMap[i + 1] = new BuyAction(SaleItems[i], this);
        }

        for (var i = pageStart; i < pageEnd; i++)
        {
            var item = SaleItems[i];
            var sb = UiManager.ItemPrinter(item, i);
            sb.Append($"{PadRightWithKorean($"{item.Cost}G", 10)}");

            sb.Append(" | ");
            output.Add(sb.ToString());
        }

        Console.WriteLine();
        return output;
    }

    public override void OnExcute()
    {
        var lines = GetPageContent();
        base.OnExcute();

        int LineCount = 8;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(7, $"{PlayerInfo.Gold} G");

        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount + i, lines[i]);
        }

        if (Page > 0)
            lineDic.Add(11, "-1. 이전 페이지");
        if (Page < MaxPage - 1)
            lineDic.Add(12, "-2. 다음 페이지");

        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Shop_Buy, null, (8, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new BuyItemAction(PrevAction, newPage);
    }
}

public class BuyAction : ActionBase
{
    private readonly Item item;

    public BuyAction(Item _item, IAction _prevAction)
    {
        item = _item;
        NextAction = _prevAction;
    }

    public override string Name => $"";

    public override void OnExcute()
    {
        var message = string.Empty;
        if (PlayerInfo != null)
        {
            if (PlayerInfo.Gold < item.Cost)
            {
                message = "골드가 부족합니다.";
            }
            else
            {
                PlayerInfo.Gold -= item.Cost;
                InventoryManager.Instance.AddItem(item.Copy());
                QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Item, QuestConditionType.Buy), item.Key);
                message = $"{item.Name}을(를) 구매했습니다!";
            }
        }
    }
}

public class SellItemAction : PagedListActionBase
{
    public SellItemAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "상점 - 아이템 판매";

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        isView = false;

        int totalInvenCount = InventoryManager.Instance.Inventory.Count;
        MaxPage = (int)Math.Ceiling(totalInvenCount / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, totalInvenCount);

        SubActionMap.Clear();

        for (var i = 0; i < totalInvenCount; i++)
            SubActionMap.Add(i + 1, new SellAction(InventoryManager.Instance.Inventory[i], this));

        for (var i = pageStart; i < pageEnd; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = UiManager.ItemPrinter(item, i, false);
            sb.Append($"| {PadRightWithKorean($"{item.Cost * 0.85}G", 5)}");

            output.Add(sb.ToString());
        }

        return output;
    }

    public override void OnExcute()
    {
        var lines = GetPageContent();
        base.OnExcute();

        int LineCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();

        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount + i, lines[i]);
        }

        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Shop_Sell, null, (8, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new SellItemAction(PrevAction, newPage);
    }
}

public class SellAction : ActionBase
{
    private readonly Item item;

    public SellAction(Item _item, IAction _prevAction)
    {
        PrevAction = _prevAction;
        item = _item;
    }

    public override string Name => $"{item.Name} 판매하기";

    public override void OnExcute()
    {
        PlayerInfo.Gold += (int)(item.Cost * 0.85);
        InventoryManager.Instance.RemoveItem(item);
        QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Item, QuestConditionType.Sell), item.Key);
        NextAction = PrevAction;
    }
}