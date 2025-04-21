namespace Camp_FourthWeek_Basic_C__;

public class MainMenuAction : ActionBase
{
    public MainMenuAction()
    {
        InitializeMainActions(this);
    }

    public override string Name => "마을";

    public void InitializeMainActions(MainMenuAction mainAction)
    {
        SubActionMap[1] = new EnterCharacterInfoAction(mainAction);
        SubActionMap[2] = new EnterInventoryAction(mainAction);
        SubActionMap[3] = new EnterShopAction(mainAction);
        SubActionMap[4] = new EnterDungeonAction(mainAction);
        SubActionMap[5] = new EnterRestAction(mainAction);
        SubActionMap[6] = new EnterResetAction(mainAction);
    }

    public override void OnExcute()
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

        SelectAndRunAction(SubActionMap);
    }
}