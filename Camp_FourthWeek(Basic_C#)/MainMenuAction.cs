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
        SubActionMap[(int)MainManu.Character] = new EnterCharacterInfoAction(mainAction);
        SubActionMap[(int)MainManu.Inventory] = new EnterInventoryAction(mainAction);
        SubActionMap[(int)MainManu.Shop] = new EnterShopAction(mainAction);
        SubActionMap[(int)MainManu.Stage] = new EnterStageAction(mainAction);
        SubActionMap[(int)MainManu.Quest] = new EnterQuestAction(mainAction);
        SubActionMap[(int)MainManu.Collection] = new EnterCollectionAction(mainAction);
        SubActionMap[(int)MainManu.Rest] = new EnterRestAction(mainAction);
        SubActionMap[(int)MainManu.Reset] = new EnterResetAction(mainAction);
    }

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Main));
    }
}