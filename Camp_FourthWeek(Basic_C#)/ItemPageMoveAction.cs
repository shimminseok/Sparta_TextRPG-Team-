namespace Camp_FourthWeek_Basic_C__;

public class ItemPageMoveAction : ActionBase
{
    private readonly int dir;
    private readonly EquipItemManagementAction equipAction;

    public ItemPageMoveAction(IAction _prevAction, EquipItemManagementAction _equipAction, int _dir)
    {
        equipAction = _equipAction;
        PrevAction = _prevAction;
        dir = _dir;
    }

    public override string Name => string.Empty;

    public override void OnExcute()
    {
        equipAction.MovePage(dir);
        PrevAction?.Execute();
    }
}