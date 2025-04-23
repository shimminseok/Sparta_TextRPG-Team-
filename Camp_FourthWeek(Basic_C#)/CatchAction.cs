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

            float hpRatio = monster.Stats[StatType.CurHp].FinalValue / monster.Stats[StatType.MaxHp].FinalValue;
            float catchChance = maximumCatchChance - (maximumCatchChance - basicCatchChance) * hpRatio;


            //포획 성공시
            QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Monster, QuestConditionType.Catch),
                (int)monster.Type);
            InventoryManager.Instance.AddMonsterToBox(monster);
            //

            SelectAndRunAction(SubActionMap);
        }
    }
}