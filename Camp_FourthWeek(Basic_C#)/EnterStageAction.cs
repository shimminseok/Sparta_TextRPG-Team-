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
        //체력이 0일떄 돌아가게끔 함
        if (PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue <= 0)
        {
            NextAction = PrevAction;
        }

        SubActionMap.Clear();
        int stageCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();

        for (var i = 0; i <= StageManager.Instance.ClearStage; i++)
        {
            //StageAction이 아닌 EnterBattleAction 이걸 추가
            var stage = StageTable.GetDungeonById(i + 1);

            var battleAction = new EnterBattleAction(stage, this);
            SubActionMap.Add(i + 1, battleAction);

            lineDic.Add(stageCount++, $"{i + 1} : {battleAction.Name}");
        }

        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Location, null, (10, lineDic)));
    }
}