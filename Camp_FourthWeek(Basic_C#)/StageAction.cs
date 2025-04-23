namespace Camp_FourthWeek_Basic_C__;

public class StageAction : ActionBase
{
    private readonly Stage? stage;

    public StageAction(int _dunNum, IAction _prevAction)
    {
        stage = StageTable.GetDungeonById(_dunNum);
        PrevAction = _prevAction;
    }

    public override string Name => stage.DungeonName;

    public override void OnExcute()
    {
        //전투 시작
        Console.WriteLine("전투를 시작해요!!");
        PrevAction!.Execute();
    }
}