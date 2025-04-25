namespace Camp_FourthWeek_Basic_C__;

public class EnterInventoryAction : ActionBase
{
    public EnterInventoryAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {            
            { 1, new MonsterBoxAction(this) },
            { 2, new EquipItemManagementAction(this) },
            { 3, new UseItemAction(this) },
        };
    }

    public override string Name => "인벤토리";

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Inventory));
    }
}