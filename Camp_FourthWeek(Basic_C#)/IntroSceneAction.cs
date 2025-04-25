namespace Camp_FourthWeek_Basic_C__;

public class IntroSceneAction : ActionBase
{
    public override string Name { get; }

    public IntroSceneAction()
    {
        Thread introAnimation = new Thread(IntroAnimation);
        introAnimation.Start();
        SubActionMap[1] = GameManager.Instance.LoadGame();
    }

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false);
    }

    public void IntroAnimation()
    {
        UiManager.IntroRoop();
    }
}