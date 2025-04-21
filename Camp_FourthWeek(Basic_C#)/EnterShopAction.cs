namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EnterShopAction : ActionBase
{
    public List<Item> SaleItems = new();

    public EnterShopAction(IAction _prevAction)
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

    public override void OnExcute()
    {
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        Console.WriteLine();
        ShowSaleItems();
        SelectAndRunAction(SubActionMap);
    }

    private void ShowSaleItems()
    {
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{PlayerInfo.Gold}G");
        Console.WriteLine();
        Console.WriteLine("[아이템 목록]");
        for (var i = 0; i < SaleItems.Count; i++)
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
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
    }
}