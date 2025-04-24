namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackSelectAction : ActionBase
    {
        private Skill? skill;
        public override string Name =>
            skill is null ? "공격하기" : $"{skill.Name} - MP {skill.Stats[StatType.CurMp].FinalValue}\n{skill.Description}";

        public override void OnExcute()
        {
            SubActionMap.Clear();
            EnterBattleAction.DisplayMonsterList();
            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();
            if(skill is not null)
            {
                if(skill.SkillAttackType== SkillAttackType.Random)
                {
                    int count = skill.TargetCount;
                    Random rand = new Random(DateTime.Now.Millisecond);
                    var randomTargets = aliveMonsterList.OrderBy(_ => rand.Next()).Take(count).ToList();
                    new AttackAction(randomTargets, skill, PrevAction).Execute();
                }
                if (skill.SkillAttackType == SkillAttackType.All)
                {
                    new AttackAction(aliveMonsterList, skill, PrevAction).Execute();
                }
            }
            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                {
                    SubActionMap.Add(i + 1, new AttackAction(aliveMonsterList[i], skill, PrevAction));
                }
            }
            int maxKey = SubActionMap.Keys.Max(); //포켓몬 몇마리인지(최대) 받아서 ResultAction.cs에서 출력!
            ResultAction.SetmaxKey(maxKey);

            SelectAndRunAction(SubActionMap);
        }

        public AttackSelectAction(IAction _prevAction, Skill? _skill = null)
        {
            PrevAction = _prevAction;
            skill = _skill;
        }
    }
}
