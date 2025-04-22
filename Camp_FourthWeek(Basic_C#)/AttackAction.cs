using static System.Net.Mime.MediaTypeNames;

namespace Camp_FourthWeek_Basic_C__
{

    public class AttackAction : ActionBase
    {
        private Monster monster;
        private float attackPower;
        public AttackAction(Monster _monster,float _attackPower,IAction _prevAction)
        {
            PrevAction = _prevAction;
            monster = _monster;
            attackPower = _attackPower;
        }

        public override string Name => $"Lv.{monster.Lv} {monster.Name} 공격";

        public override void OnExcute()
        {
            var message = string.Empty;
            bool isCritical = false;
            bool isEvade = false;
            Random rand = new Random();

            if (rand.NextDouble() < PlayerInfo.Stats[StatType.EvadeChance].FinalValue * 0.01f)
                isEvade = true;
            if (rand.NextDouble() < PlayerInfo.Stats[StatType.CriticalChance].FinalValue * 0.01f)
                isCritical = true;
            if(isEvade)
            {
                message = $"{monster.Name}이 피했다!";
            }
            else
            {
                float damage = isCritical ? attackPower * monster.Stats[StatType.CriticlaDamage].FinalValue : attackPower;
                monster.Stats[StatType.CurHp].ModifyAllValue(damage);
                message = $"{(isCritical ? "치명타!" : null)}{monster.Name}의 체력이 {damage} 만큼 달았다!";
            }
            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }
        }
    }
}
