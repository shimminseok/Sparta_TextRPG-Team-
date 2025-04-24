namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class BuyItemAction : PagedListActionBase
{
    private List<Item> SaleItems = new();

    public BuyItemAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        SaleItems = ItemTable.ItemDic.Values.ToList();
    }

    public override string Name => "상점 - 아이템 구매";

    protected override List<string> GetPageContent()
    {
        isView=false;
        // SaleItems = ItemTable.ItemDic.Values
        //     .Where(item => !InventoryManager.Instance.Inventory.Any(x => x.Key == item.Key))
        //     .ToList();
        var output = new List<string>();
        SubActionMap.Clear();
        MaxPage = (int)Math.Ceiling(SaleItems.Count / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, SaleItems.Count);
        for (var i = 0; i < SaleItems.Count; i++)
        {
            SubActionMap[i + 1] = new BuyAction(SaleItems[i], this);
        }

        output.Add("필요한 아이템을 구매할 수 있습니다.");
        output.Add("[보유 골드]");
        output.Add($"{PlayerInfo.Gold}G");
        output.Add("[아이템 목록]");
        for (var i = pageStart; i < pageEnd; i++)
        {
            var item = SaleItems[i];
            var sb = UiManager.ItemPrinter(item, i);
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

        Console.WriteLine();
        return output;
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new BuyItemAction(PrevAction, newPage);
    }
}