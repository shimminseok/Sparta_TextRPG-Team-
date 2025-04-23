namespace Camp_FourthWeek_Basic_C__;

public class StageAction : ActionBase
{
    private readonly Stage? stage;

    public StageAction(int _dunNum, IAction _prevAction)
    {
        stage = StageTable.GetDungeonById(_dunNum);
        PrevAction = _prevAction;
    }

    public override string Name => stage.StageName;

    public override void OnExcute()
    {
        //전투 시작
        //내가 이기면
        StageManager.Instance.ClearCurrentStage(stage);

        //내가 지면
        StageManager.Instance.UnClearStage(stage);

        PrevAction!.Execute();
    }
}