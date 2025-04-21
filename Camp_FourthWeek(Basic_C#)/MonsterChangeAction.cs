namespace Camp_FourthWeek_Basic_C__;


public class MonsterChangeAction : ActionBase
{
    public override string Name => "포켓몬 박스";
    public MonsterChangeAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        Console.WriteLine("포켓몬을 관리 할 수 있습니다.");
        Console.WriteLine("\n[포켓몬 목록]");


        SelectAndRunAction(SubActionMap);
    }
}
