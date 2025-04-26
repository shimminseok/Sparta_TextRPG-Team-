namespace Camp_FourthWeek_Basic_C__;

public class ChangeMonsterSelectAction : ActionBase
{
    private Monster monster;

    public ChangeMonsterSelectAction(Monster _monster, IAction _prevAction)
    {
        monster = _monster;
        NextAction = _prevAction;
    }

    public override string Name => string.Empty;

    public override void OnExcute()
    {
        GameManager.Instance.PlayerInfo.ChangeMonsterStat(monster);
    }
}