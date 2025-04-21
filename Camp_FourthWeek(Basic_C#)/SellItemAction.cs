namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class SellItemAction : ActionBase
{
    public SellItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "상점 - 아이템 판매";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        for (var i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
            SubActionMap.Add(i + 1, new SellAction(InventoryManager.Instance.Inventory[i], this));
        ShowItemInfo();
        Console.WriteLine();
        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }

    private void ShowItemInfo()
    {
        for (var i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = UiManager.ItemPrinter(item, i, false);
            sb.Append($"| {PadRightWithKorean($"{item.Cost * 0.85}G", 5)}");

            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }
    }
}