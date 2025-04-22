namespace Camp_FourthWeek_Basic_C__
{

    public class AttackSelectAction : ActionBase
    {
        private Skill skill;
        public AttackSelectAction(IAction _prevAction, Skill? _skill = null)
        {
            PrevAction = _prevAction;
            skill = _skill;
        }

        public override string Name => $"{(PrevAction is SkillSelectAction ? skill.Name :  "공격하기")}";

        public override void OnExcute()
        {
            BattleAction.ShowMonsterList(true);
            for(int i= 0; i<BattleAction.monsters.Length; i++)
            {
                if(!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new AttackAction(BattleAction.monsters[i], skill is null ? PlayerInfo.Monster.Stats[StatType.Attack].FinalValue : skill.Stats[StatType.Attack].FinalValue,PrevAction));
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}
