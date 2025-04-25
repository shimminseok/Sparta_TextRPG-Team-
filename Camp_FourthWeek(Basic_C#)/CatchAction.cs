using static System.Net.Mime.MediaTypeNames;

namespace Camp_FourthWeek_Basic_C__
{
    public class CatchAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };
        private Monster monster;

        public CatchAction(Monster _monster, IAction _prevAction, Dictionary<int, string>? _lineDic = null
            , Dictionary<int, Tuple<int, int>?> _pivotDict = null, List<int> _monsterList = null)
        {
            PrevAction = _prevAction;
            monster = _monster;
            lineDic = _lineDic;
            pivotDict = _pivotDict;
            monsterUIList = _monsterList;
        }

        public override string Name => $"Lv.{monster.Lv} {monster.Name} 포획";

        public override void OnExcute()
        {


            float basicCatchChance = 0.3f;
            float maximumCatchChance = 0.9f;
            Random rand = new Random(DateTime.Now.Millisecond);
            float hpRatio = monster.Stats[StatType.CurHp].FinalValue / monster.Stats[StatType.MaxHp].FinalValue;
            float catchChance = maximumCatchChance - (maximumCatchChance - basicCatchChance) * hpRatio;

            if(rand.NextDouble()<catchChance)
            {
                QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Monster, QuestConditionType.Catch),
                    (int)monster.Type);
                InventoryManager.Instance.AddMonsterToBox(monster);
                EnterBattleAction.MonsterStateDic[monster] = MonsterState.Catched;
                Console.WriteLine("포획 성공!");
            }
            else
            {
                Console.WriteLine("포획 실패............................");
            }
            InputNumber();
            CheckBattleEnd();
            new EnemyAttackAction(PrevAction,monsterUIList,lineDic).Execute();
        }
        private void CheckBattleEnd()
        {
            bool isAllMonstersDead = !EnterBattleAction.GetAliveMonsters().Any();

            if (isAllMonstersDead)
            {
                SubActionMap[1] = new ResultAction(true, new MainMenuAction());
                SubActionMap[1].Execute();
            }
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