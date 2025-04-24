namespace Camp_FourthWeek_Basic_C__;

public class BuyAction : ActionBase
{
    private readonly Item item;

    public BuyAction(Item _item, IAction _prevAction)
    {
        item = _item;
        PrevAction = _prevAction;
    }

    public override string Name => $"";

    public override void OnExcute()
    {
        var message = string.Empty;
        if (PlayerInfo != null)
        {
            if (PlayerInfo.Gold < item.Cost)
            {
                message = "골드가 부족합니다.";
            }
            else
            {
                PlayerInfo.Gold -= item.Cost;
                InventoryManager.Instance.AddItem(item.Copy());
                message = $"{item.Name}을(를) 구매했습니다!";
            }
        }

        if (PrevAction != null)
        {
            PrevAction.SetFeedBackMessage(message);
            PrevAction.Execute();
        }
    }
}