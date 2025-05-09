namespace Camp_FourthWeek_Basic_C__;

public class EnterRestAction : ActionBase
{
    public EnterRestAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new RecoverAction(this) }
        };
    }

    public override string Name => "포켓몬 센터";

    public override void OnExcute()
    {
        Console.WriteLine($"500G를 내면 장착한 포켓몬의 체력을 회복할 수 있습니다. (보유 골드 : {PlayerInfo.Gold})");

        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(6, $"보유 골드 : {PlayerInfo.Gold} G");

        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Center, null, (1, lineDic)));
    }
}

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
            var curMP = PlayerInfo.Monster.Stats[StatType.CurMp];
            var before = curHP.FinalValue;
            int recoverGold = 500; //회복비용
            curHP.ModifyBaseValue(PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue, 0,
                PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue);
            curMP.ModifyBaseValue(PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue, 0,
                PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue);
            PlayerInfo.Gold -= recoverGold;
            Console.WriteLine("회복중..");
            Thread.Sleep(2000);
            message = $"체력이 회복되었습니다 HP {before} -> {curHP.FinalValue}";
        }

        NextAction = PrevAction;
    }
}