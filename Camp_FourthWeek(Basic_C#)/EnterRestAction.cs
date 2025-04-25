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