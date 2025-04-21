namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class BuyItemAction : ActionBase
{
    private List<Item> SaleItems = new();

    public BuyItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "상점 - 아이템 구매";

    public override void OnExcute()
    {
        SaleItems = ItemTable.ItemDic.Values
            .Where(item => !InventoryManager.Instance.Inventory.Any(x => x.Key == item.Key))
            .ToList();
        SubActionMap.Clear();
        for (var i = 0; i < SaleItems.Count; i++)
            if (SubActionMap.ContainsKey(i + 1))
                SubActionMap[i + 1] = new BuyAction(SaleItems[i], this);
            else
                SubActionMap.Add(i + 1, new BuyAction(SaleItems[i], this));

        Console.WriteLine("필요한 아이템을 구매할 수 있습니다.");
        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{PlayerInfo.Gold}G");
        Console.WriteLine("[아이템 목록]");
        ShowItemInfo();


        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }

    private void ShowItemInfo()
    {
        for (var i = 0; i < SaleItems.Count; i++)
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
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
    }
}