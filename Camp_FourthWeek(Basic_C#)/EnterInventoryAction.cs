namespace Camp_FourthWeek_Basic_C__;

public class EnterInventoryAction : ActionBase
{

    public EnterInventoryAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new EquipItemManagementAction(this,GameManager.Instance.PlayerInfo,
                                                InventoryManager.Instance) },
            { 2, new MonsterBoxAction(this) },
            {3, new UseItemAction(this)},
        };
    }

    public override string Name => "인벤토리";

    public override void OnExcute()
    {
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

        SelectAndRunAction(SubActionMap);
    }
}