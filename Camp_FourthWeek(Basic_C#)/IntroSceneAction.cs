namespace Camp_FourthWeek_Basic_C__;

public class IntroSceneAction
{
    public void StartGame()
    {
        Console.WriteLine("아무키나 누르면 시작할 수 있지롱");
        Console.ReadKey();
        GameManager.Instance.LoadGame();
    }
}