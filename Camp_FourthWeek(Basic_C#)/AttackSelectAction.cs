using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackSelectAction : ActionBase
    {
        private Skill? skill;

        public override string Name =>
            skill is null ? "공격하기" : $"{skill.Name} - MP {skill.Stats[StatType.CurMp].FinalValue}\n{skill.Description}";

        public AttackSelectAction(IAction _prevAction, Skill? _skill)
        {
            PrevAction = _prevAction;
            skill = _skill;
        }

        public override void OnExcute()
        {
            SubActionMap.Clear();
            AttackActionBase.battleLogDic.Clear();
            AttackActionBase.uiPivotDic.Clear();

            AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };

            AttackActionBase.BattlePlayerInfo();
            AttackActionBase.DisplayMonsterList();

            var aliveMonsterList = AttackActionBase.battleMonsters
                .Where(m => AttackActionBase.monsterStates[m] == MonsterState.Normal)
                .ToList();
            int pivotCount = AttackActionBase.uiPivotDic.Last().Key + 1;
            int playerActionLine = AttackActionBase.battleLogDic.Last().Key + 2;
            if (skill is not null)
            {
                if (skill is not null)
                {
                    AttackActionBase.battleLogDic[18] = "[ 스킬 대상 선택 ]";
                    if (skill.SkillAttackType == SkillAttackType.Select)
                    {
                        for (int i = 0; i < aliveMonsterList.Count; i++)
                        {
                            AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
                            AttackActionBase.battleLogDic[playerActionLine++] =
                                $"- {i + 1} : {aliveMonsterList[i].Name}";

                            SubActionMap.Add(i + 1,
                                new AttackAction(new List<Monster> { aliveMonsterList[i] }, skill, PrevAction));
                        }
                    }
                    else if (skill.SkillAttackType == SkillAttackType.Random)
                    {
                        int count = skill.TargetCount;
                        Random rand = new Random(DateTime.Now.Millisecond);
                        var randomTargets = aliveMonsterList.OrderBy(_ => rand.Next()).Take(count).ToList();
                        NextAction = new AttackAction(randomTargets, skill, PrevAction);
                    }
                    else if (skill.SkillAttackType == SkillAttackType.All)
                    {
                        NextAction = new AttackAction(aliveMonsterList, skill, PrevAction);
                    }
                }
            }
            else
            {
                AttackActionBase.battleLogDic[18] = "[ 공격 대상 선택 ]";
                for (int i = 0; i < aliveMonsterList.Count; i++)
                {
                    AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
                    AttackActionBase.battleLogDic[playerActionLine++] = $"- {i + 1} : {aliveMonsterList[i].Name}";

                    SubActionMap.Add(i + 1,
                        new AttackAction(new List<Monster> { aliveMonsterList[i] }, skill, PrevAction));
                }

                for (int i = playerActionLine; i < 22; i++)
                {
                    AttackActionBase.battleLogDic[i] = "";
                }
            }

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                    AttackActionBase.uiPivotDic,
                    (18, AttackActionBase.battleLogDic),
                    AttackActionBase.monsterIconList));
        }
    }
}