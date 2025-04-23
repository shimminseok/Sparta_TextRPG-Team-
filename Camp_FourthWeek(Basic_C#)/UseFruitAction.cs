namespace Camp_FourthWeek_Basic_C__;

public class UseFruitAction : ActionBase
{
    public UseFruitAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "열매 사용";

    public override void OnExcute()
    {
        InventoryManager.Instance.UseFruit();
        PrevAction?.Execute();
    }
}