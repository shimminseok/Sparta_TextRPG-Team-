namespace Camp_FourthWeek_Basic_C__;

public class EnterInventoryAction : ActionBase
{
    public EnterInventoryAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new EquipItemManagementAction(this) }
        };
    }

    public override string Name => "인벤토리";

    public override void OnExcute()
    {
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

        Console.WriteLine("[아이템 목록]");
        for (var i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = UiManager.ItemPrinter(item, i);

            if (item.IsEquipment)
            {
                var idx = sb.ToString().IndexOf(item.Name);
                Console.ForegroundColor = ConsoleColor.Green;
                sb.Insert(idx, "[E]");
            }

            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }

        SelectAndRunAction(SubActionMap);
    }
}