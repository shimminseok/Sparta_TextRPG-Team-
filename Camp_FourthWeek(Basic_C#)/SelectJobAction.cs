namespace Camp_FourthWeek_Basic_C__;

public class SelectJobAction : ActionBase
{
    public SelectJobAction(Job _job, string _name)
    {
        Job = _job;
        CharacterName = _name;
    }

    public override string Name => Job.Name;
    public Job Job { get; }

    public string CharacterName { get; }

    public override void OnExcute()
    {
        Console.WriteLine($"직업 : {Job.Name}을 선택하셨습니다.");

        Console.WriteLine("잠시 후 게임이 시작됩니다.");
        GameManager.Instance.Init(Job.Type, CharacterName);
        var main = new MainMenuAction();
        Thread.Sleep(1000);
        main.Execute();
    }
}