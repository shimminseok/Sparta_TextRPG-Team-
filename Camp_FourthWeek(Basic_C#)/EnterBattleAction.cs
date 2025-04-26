using System.Diagnostics;

namespace Camp_FourthWeek_Basic_C__
{
    public enum MonsterState
    {
        Normal,
        Dead,
        Catched
    }

    public class EnterBattleAction : ActionBase
    {
        public override string Name => $"{currentStage.StageName}";
        private Stage currentStage;


        public EnterBattleAction(Stage _currentStage, IAction _prevAction)
        {
            PrevAction = _prevAction;
            currentStage = _currentStage;
            AttackActionBase.InitializeBattle(currentStage);
            SubActionMap = new Dictionary<int, IAction>
            {
                { 1, new AttackSelectAction(this, null) },
                { 2, new SkillSelectAction(this) },
                { 3, new CatchSelectAction(this) }
            };
            StageManager.Instance.CurrentStage = _currentStage;
        }


        public override void OnExcute()
        {
            AttackActionBase.battleLogDic.Clear();
            AttackActionBase.uiPivotDic.Clear();
            AttackActionBase.monsterIconList.Clear();

            AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };

            AttackActionBase.BattlePlayerInfo();
            AttackActionBase.DisplayMonsterList();

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(
                    UIName.Battle,
                    AttackActionBase.uiPivotDic,
                    (18, AttackActionBase.battleLogDic),
                    AttackActionBase.monsterIconList));
        }
    }
}