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

        SaleItems = ItemTable.ItemDic.Values.ToList();
    }

    public override string Name => "상점";

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();

        
        
        MaxPage = (int)Math.Ceiling(SaleItems.Count / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, SaleItems.Count);
        output.Add("필요한 아이템을 얻을 수 있는 상점입니다.");
        output.Add("[보유 골드]");
        output.Add($"{PlayerInfo.Gold}G");
        output.Add("[아이템 목록]");
        for (var i = pageStart; i < pageEnd; i++)
        {
            var item = SaleItems[i];
            var sb = UiManager.ItemPrinter(item, i);
            sb.Append(" | ");
            if (InventoryManager.Instance.Inventory.Exists(x => x.Name == item.Name))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                sb.Append($"{PadRightWithKorean("구매완료", 10)}");
            }
            else
            {
                sb.Append($"{PadRightWithKorean($"{item.Cost}G", 10)}");
            }

            sb.Append(" | ");
            output.Add(sb.ToString());
            Console.ResetColor();
        }
        return output;
    }
    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EnterShopAction(PrevAction, newPage);
    }
}