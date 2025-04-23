namespace Camp_FourthWeek_Basic_C__;

public class EnterStageAction : ActionBase
{
    public EnterStageAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        //TO DO
        SubActionMap[1] = new SelectStageAction(this);
    }

    public override string Name => "지역 이동";

    public override void OnExcute()
    {
        //TO DO
        //Clear한 스테이지를 저장한 후 그 스테이지에 맞게 뿌려줌
        SelectAndRunAction(SubActionMap);
    }

    public class SelectStageAction : ActionBase
    {
        public override string Name => "전투 시작";

        public SelectStageAction(IAction _prAction)
        {
            PrevAction = _prAction;
        }

        public override void OnExcute()
        {
            SubActionMap.Clear();

            //TO DO
            // 현재 클리어한 스테이지를 저장 후 해당 스테이지 만큼 돌려줌
            for (var i = 0; i <= StageManager.Instance.ClearStage; i++)
            {
                SubActionMap.Add(i + 1, new StageAction(i + 1, this));
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}