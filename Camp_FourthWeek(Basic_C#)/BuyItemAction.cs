namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

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