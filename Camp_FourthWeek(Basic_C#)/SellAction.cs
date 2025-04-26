namespace Camp_FourthWeek_Basic_C__;

public class SellAction : ActionBase
{
    private readonly Item item;

    public SellAction(Item _item, IAction _prevAction)
    {
        PrevAction = _prevAction;
        item = _item;
    }

    public override string Name => $"{item.Name} 판매하기";

    public override void OnExcute()
    {
        PlayerInfo.Gold += (int)(item.Cost * 0.85);
        InventoryManager.Instance.RemoveItem(item);
        QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Item, QuestConditionType.Sell), item.Key);
        NextAction = PrevAction;
    }
}