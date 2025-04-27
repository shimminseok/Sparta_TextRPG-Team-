namespace Camp_FourthWeek_Basic_C__;

public class ActionMainMenu : ActionBase
{
    public ActionMainMenu()
    {
        InitializeMainActions(this);
    }

    public override string Name => "마을";

    public void InitializeMainActions(ActionMainMenu _actionMain)
    {
        SubActionMap[(int)MainManu.Character] = new EnterCharacterInfoAction(_actionMain);
        SubActionMap[(int)MainManu.Inventory] = new EnterInventoryAction(_actionMain);
        SubActionMap[(int)MainManu.Shop] = new EnterShopAction(_actionMain);
        SubActionMap[(int)MainManu.Stage] = new EnterStageAction(_actionMain);
        SubActionMap[(int)MainManu.Quest] = new EnterQuestAction(_actionMain);
        SubActionMap[(int)MainManu.Collection] = new EnterCollectionAction(_actionMain);
        SubActionMap[(int)MainManu.Rest] = new EnterRestAction(_actionMain);
    }

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Main));
    }
}