namespace Camp_FourthWeek_Basic_C__;

public class PokemonChangePageMoveAction: ActionBase
{
    private readonly int dir;
    private readonly MonsterChangeAction changeAction;
    public override string Name =>"";
    public PokemonChangePageMoveAction(IAction _prevAction, MonsterChangeAction _changeAction, int _dir)
    {
        changeAction = _changeAction;
        PrevAction = _prevAction;
        dir = _dir;
    }
    public override void OnExcute()
    {
        changeAction.MovePage(dir);
        PrevAction?.Execute();
    }
}