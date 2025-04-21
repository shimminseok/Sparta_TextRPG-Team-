namespace Camp_FourthWeek_Basic_C__;

public class EnterDungeonAction : ActionBase
{
    public EnterDungeonAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>();
        for (var i = 0; i < DungeonTable.DungeonDic.Count; i++) SubActionMap.Add(i + 1, new DungeonAction(i + 1, this));
    }

    public override string Name => "던전 입장";

    public override void OnExcute()
    {
        Console.WriteLine("");

        Console.WriteLine("[던전 목록]");
        Console.WriteLine($"현재 공격력 : {PlayerInfo.Stats[StatType.Attack].FinalValue}");
        Console.WriteLine($"현재 방어력 : {PlayerInfo.Stats[StatType.Defense].FinalValue}");

        SelectAndRunAction(SubActionMap);
    }
}