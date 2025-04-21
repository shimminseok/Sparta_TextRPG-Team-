namespace Camp_FourthWeek_Basic_C__;

public class EnterResetAction : ActionBase
{
    public EnterResetAction(IAction _action)
    {
        PrevAction = _action;
    }

    public override string Name => "리셋!!!";

    public override void OnExcute()
    {
        Console.WriteLine("저장된 데이터를 삭제하시겠습니까?");
        Console.WriteLine("1. 예 \t 2.아니오");

        if (int.TryParse(Console.ReadLine(), out var input))
        {
            if (input == 1)
                GameManager.Instance.DeleteGameData();
            else if (input == 2) PrevAction?.Execute();
        }
    }
}