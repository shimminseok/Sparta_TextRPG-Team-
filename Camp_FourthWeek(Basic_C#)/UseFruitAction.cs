namespace Camp_FourthWeek_Basic_C__;

public class UseFruitAction:ActionBase
{
    private float healAmount = 30f;
    public UseFruitAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }
    public override string Name =>"열매 사용";

    public override void OnExcute()
    {
        var player = GameManager.Instance.PlayerInfo;
        var monster = player.Monster;
        var hpStat = monster.Stats[StatType.CurHp];
        var maxHp = monster.Stats[StatType.MaxHp].FinalValue;
        hpStat.ModifyBaseValue(healAmount, 0, maxHp);

        UseItemAction.fruitCount--;
        PrevAction?.Execute();
    }
}