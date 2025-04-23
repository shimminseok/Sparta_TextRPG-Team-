namespace Camp_FourthWeek_Basic_C__;

public class UseItemAction : ActionBase
{
    
    public override string Name => "열매사용";

    public UseItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    } 
    public override void OnExcute()
    {       
        Console.WriteLine("이곳에서 열매를 사용하여 포켓몬의 체력을 30회복시킬 수 있습니다.");
        SelectAndRunAction(SubActionMap);
    }
}