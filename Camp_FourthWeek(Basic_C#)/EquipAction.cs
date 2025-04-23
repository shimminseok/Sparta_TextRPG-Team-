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
        var player = GameManager.Instance.PlayerInfo;
        var currentMonster = player.Monster;
        var message = string.Empty;


        if (item.IsEquippedBy(currentMonster))
        {
            message = $"{item.Name}장착해제 했습니다.";
            EquipmentManager.UnequipItem((item.ItemType));
            currentMonster.ItemId = 0;
        }
        else
        {
            message = $"{item.Name}장착 되었습니다.";
            foreach (var m in player.Monsters)
            {
                if (m.ItemId == item.Key)
                {
                    m.ItemId = 0;
                }
            }
            
            player.Monster.ItemId = item.Key;
            EquipmentManager.EquipmentItem(item);
        }

        Console.WriteLine(message);
        PrevAction?.Execute();
    }
}