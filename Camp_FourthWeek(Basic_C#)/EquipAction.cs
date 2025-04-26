namespace Camp_FourthWeek_Basic_C__;

public class EquipAction : ActionBase
{
    private readonly Item item;

    public EquipAction(Item _item, IAction _prevAction)
    {
        item = _item;
        PrevAction = _prevAction;
        NextAction = _prevAction;
    }

    public override string Name => $"{item.Name} 아이템 {(item.IsEquipment ? "해제" : "장착")}";

    public override void OnExcute()
    {
        var player = PlayerInfo;
        var currentMonster = player.Monster;
        if (currentMonster.Item == item)
        {
            EquipmentManager.UnequipItem(currentMonster);
        }
        else
        {
            EquipmentManager.EquipmentItem(item);
        }
    }
}