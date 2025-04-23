namespace Camp_FourthWeek_Basic_C__;

public class PokemonPageMoveAction: ActionBase
{
    private readonly int dir;
    private readonly MonsterBoxAction boxAction;
    private readonly MonsterChangeAction changeAction;
    public override string Name =>"";
    public PokemonPageMoveAction(IAction _prevAction, MonsterBoxAction _boxAction, int _dir)
    {
        boxAction = _boxAction;
        PrevAction = _prevAction;
        dir = _dir;
    }
    public override void OnExcute()
    {
        boxAction.MovePage(dir);
        PrevAction?.Execute();
    }
}