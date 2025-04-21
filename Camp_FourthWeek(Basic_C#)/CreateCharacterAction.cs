namespace Camp_FourthWeek_Basic_C__;

public class CreateCharacterAction : ActionBase
{
    public CreateCharacterAction()
    {
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new CreateNickNameAction() }
        };
    }

    public override string Name => "캐릭터 생성";

    public override void OnExcute()
    {
        Console.WriteLine("스파르타마을에 오신 모험가님을 환영합니다.");
        SelectAndRunAction(SubActionMap);
    }
}