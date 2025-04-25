using static System.Net.Mime.MediaTypeNames;

namespace Camp_FourthWeek_Basic_C__
{
    public class CatchAction : ActionBase
    {
        private Monster monster;
        private int num = 25;
        private UIName uiname = UIName.Battle_AttackEnemy;

        public CatchAction(Monster _monster, IAction _prevAction)
        {
            PrevAction = _prevAction;
            monster = _monster;
        }

        public override string Name => $"Lv.{monster.Lv} {monster.Name} 포획";

        public override void OnExcute()
        {
            float basicCatchChance = 0.3f;
            float maximumCatchChance = 0.9f;
            Random rand = new Random(DateTime.Now.Millisecond);
            float hpRatio = monster.Stats[StatType.CurHp].FinalValue / monster.Stats[StatType.MaxHp].FinalValue;
            float catchChance = maximumCatchChance - (maximumCatchChance - basicCatchChance) * hpRatio;

            if (rand.NextDouble() < catchChance)
            {
                QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Monster, QuestConditionType.Catch),
                    (int)monster.Type);
                InventoryManager.Instance.AddMonsterToBox(monster);
                EnterBattleAction.MonsterStateDic[monster] = MonsterState.Catched;
            }

            var nextAction = CheckBattleEnd();
            SubActionMap[1] = nextAction;

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiname, EnterBattleAction.pivotDict, (num, EnterBattleAction.lineDic),
                    EnterBattleAction.monsterUIList));
        }

        private IAction CheckBattleEnd()
        {
            bool isAllMonstersDead = !EnterBattleAction.GetAliveMonsters().Any();

            if (isAllMonstersDead)
            {
                uiname = UIName.Battle_Result;
                EnterBattleAction.pivotDict = null;
                num = 20;
                EnterBattleAction.monsterUIList = null;
                EnterBattleAction.lineDic = null;
                return new ResultAction(true, new MainMenuAction());
            }

            return new EnemyAttackAction(PrevAction);
        }

        public void InputNumber()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                {
                    if (number == 1)
                        break;
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
                else
                    Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}