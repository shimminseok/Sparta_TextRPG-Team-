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
        GameManager.Instance.Init(Monster, CharacterName);
        var main = new MainMenuAction();
        main.Execute();
    }
}