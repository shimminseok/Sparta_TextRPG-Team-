using static System.Net.Mime.MediaTypeNames;

namespace Camp_FourthWeek_Basic_C__
{
    public class CatchAction : ActionBase
    {
        private Monster monster;

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
            SelectAndRunAction(SubActionMap);
        }
    }
}