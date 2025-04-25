namespace Camp_FourthWeek_Basic_C__;

public class EnterStageAction : ActionBase
{
    public EnterStageAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "지역 이동";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        int stageCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (var i = 0; i <= StageManager.Instance.ClearStage; i++)
        {
            SubActionMap.Add(i + 1, new StageAction(i + 1, this));
            lineDic.Add(stageCount++, $"{i + 1} : {new StageAction(i + 1, this).Name}");
        }

        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Location, null, (10, lineDic)));
    }
}