namespace Camp_FourthWeek_Basic_C__;

public class EquipAction : ActionBase
{
    private readonly Item item;

    public EquipAction(Item _item, IAction _prevAction)
    {
        item = _item;
        PrevAction = _prevAction;
    }

    public override string Name => $"{item.Name} 아이템 {(item.IsEquipment ? "해제" : "장착")}";

    public override void OnExcute()
    {
        var message = string.Empty;
        if (item.IsEquipment)
        {
            message = $"{item.Name}이 장착 해제 되었습니다.";
            EquipmentManager.UnequipItem(item.ItemType);
        }
        else
        {
            message = $"{item.Name}이 장착 되었습니다.";
            EquipmentManager.EquipmentItem(item);
        }

        Console.WriteLine(message);
        PrevAction?.Execute();
    }
}