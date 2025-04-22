namespace Camp_FourthWeek_Basic_C__;

public class ChangeMonsterSelectAction :ActionBase
{

    private Monster monster;
    public ChangeMonsterSelectAction(Monster _monster, IAction _prevAction)
    {
        monster = _monster;
        PrevAction = _prevAction;
    }

    public override string Name => $"{monster.Name} 교체";

    public override void OnExcute()
    {
        GameManager.Instance.PlayerInfo.ChangeMonsterStat(monster);
        Console.WriteLine($"{monster.Name}으로 교체했습니다.");
        PrevAction?.Execute();
    }
}