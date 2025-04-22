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

            monster.Stats[StatType.CurHp].ModifyAllValue(attackPower);
            message = $"{monster.Name}의 체력이 {attackPower} 만큼 달았다!";
            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }
        }
    }
}
