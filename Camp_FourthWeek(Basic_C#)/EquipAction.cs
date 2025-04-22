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
        var message = string.Empty;

        foreach (var m in player.Monsters)
        {
            m.ItemId = 0;
        }

        var equippedTypes = InventoryManager.Instance.Inventory.
                                                Where(i => i.IsEquipment).
                                                Select(i => i.ItemType).
                                                Distinct();
        
        foreach (var type in  equippedTypes)
        {
            EquipmentManager.UnequipItem(type);
        }

        // if (item.IsEquipment)
        // {
        //     message = $"{item.Name}이 장착 해제 되었습니다.";
        //     EquipmentManager.UnequipItem(item.ItemType);
        //     player.Monster.ItemId = 0;
        // }
        // else
        // {
        EquipmentManager.EquipmentItem(item);
        
        player.Monster.ItemId = item.Key;
        
        message = $"{item.Name}이 장착 되었습니다.";


        //}

        Console.WriteLine(message);
        PrevAction?.Execute();
    }
}