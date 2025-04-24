namespace Camp_FourthWeek_Basic_C__;

public class RecoverAction : ActionBase
{
    public RecoverAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "회복";

    public override void OnExcute()
    {
        var message = string.Empty;
        if (PlayerInfo.Gold < 500)
        {
            message = "골드가 부족합니다.";
        }
        else
        {
            var curHP = PlayerInfo.Monster.Stats[StatType.CurHp];
            var before = curHP.FinalValue;
            curHP.ModifyBaseValue(PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue, 0,
                PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue);
            Console.WriteLine("회복중..");
            Thread.Sleep(3000);
            message = $"체력이 회복되었습니다 HP {before} -> {curHP.FinalValue}";
        }

        PrevAction!.SetFeedBackMessage(message);
        PrevAction?.Execute();
    }
}