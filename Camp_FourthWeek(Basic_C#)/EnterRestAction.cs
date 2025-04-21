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

    public override string Name => "휴식 하기";

    public override void OnExcute()
    {
        Console.WriteLine($"500G를 내면 체력을 회복할 수 있습니다. (보유 골드 : {PlayerInfo.Gold})");
        SelectAndRunAction(SubActionMap);
    }
}