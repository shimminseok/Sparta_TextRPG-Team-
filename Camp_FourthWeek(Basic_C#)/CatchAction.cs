using static System.Net.Mime.MediaTypeNames;

namespace Camp_FourthWeek_Basic_C__
{
    public class CatchAction : AttackActionBase
    {
        private Monster targetMonster;
        private int num = 25;
        private UIName uiName = UIName.Battle_AttackEnemy;

        public CatchAction(Monster _targetMonster, IAction _prevAction)
        {
            PrevAction = _prevAction;
            targetMonster = _targetMonster;
        }

        public override string Name => $"Lv.{targetMonster.Lv} {targetMonster.Name} 포획";

        public override void OnExcute()
        {
            SubActionMap.Clear();
            battleLogDic.Clear();
            uiPivotDic.Clear();
            monsterIconList.Clear();

            BattlePlayerInfo();

            TryCatchMonster();

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiName, uiPivotDic, (num, battleLogDic), monsterIconList));
        }

        private void TryCatchMonster()
        {
            float basicCatchChance = 0.3f;
            float maximumCatchChance = 0.9f;
            Random rand = new Random(DateTime.Now.Millisecond);

            float hpRatio = targetMonster.Stats[StatType.CurHp].FinalValue /
                            targetMonster.Stats[StatType.MaxHp].FinalValue;
            float catchChance = maximumCatchChance - (maximumCatchChance - basicCatchChance) * hpRatio;

            int line = 24;

            if (rand.NextDouble() < catchChance)
            {
                QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Monster, QuestConditionType.Catch),
                    (int)targetMonster.Type);

                InventoryManager.Instance.AddMonsterToBox(targetMonster);
                monsterStates[targetMonster] = MonsterState.Catched;

                battleLogDic[line++] = $"Lv.{targetMonster.Lv} {targetMonster.Name}을(를) 포획했습니다!";
            }
            else
            {
                battleLogDic[line++] = $"Lv.{targetMonster.Lv} {targetMonster.Name} 포획 실패...";
            }

            for (int i = line; i < 40; i++)
            {
                battleLogDic[i] = "";
            }

            uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) } // 내 포켓몬
            };
            int pivotCount = 2;
            for (int i = 0; i < pivotArr.Length && i < battleMonsters.Count; i++)
            {
                uiPivotDic.Add(pivotCount++, pivotArr[i]);
            }

            monsterIconList.Add(28);

            SetNextAction();
        }

        private void SetNextAction()
        {
            bool isAllEnemiesDead = !battleMonsters.Any(m => monsterStates[m] == MonsterState.Normal);

            if (isAllEnemiesDead)
            {
                uiName = UIName.Battle_Result;
                num = 20;
                uiPivotDic.Clear();
                monsterIconList.Clear();
                battleLogDic.Clear();
                NextAction = new ResultAction(true, new MainMenuAction());
            }
            else
            {
                SubActionMap[1] = new EnemyAttackAction(PrevAction);
            }
        }
    }
}