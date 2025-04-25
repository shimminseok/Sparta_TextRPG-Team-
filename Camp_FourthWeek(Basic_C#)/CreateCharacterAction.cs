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
        SelectAndRunAction(SubActionMap);
    }
}