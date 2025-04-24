namespace Camp_FourthWeek_Basic_C__;

public class IntroSceneAction : ActionBase
{
    public override string Name => "인트로 씬입니다.";


    public override void OnExcute()
    {
        Console.WriteLine("아무키나 누르면 게임 시작 쌉가능");
        Console.ReadKey();
        GameManager.Instance.LoadGame();
    }
}