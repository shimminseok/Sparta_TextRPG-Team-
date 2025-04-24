namespace Camp_FourthWeek_Basic_C__;

public class SelectMonsterAction : ActionBase
{
    public SelectMonsterAction(Monster _monster, string _name)
    {
        Monster = _monster;
        CharacterName = _name;
    }

    public override string Name => Monster.Name;
    public Monster Monster { get; }

    public string CharacterName { get; }

    public override void OnExcute()
    {
        Console.WriteLine($"직업 : {Monster.Name}을 선택하셨습니다.");

        Console.WriteLine("잠시 후 게임이 시작됩니다.");
        GameManager.Instance.Init(Monster, CharacterName);
        var main = new MainMenuAction();
        Thread.Sleep(1000);
        main.Execute();
    }
}