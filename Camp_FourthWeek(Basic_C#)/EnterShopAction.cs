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