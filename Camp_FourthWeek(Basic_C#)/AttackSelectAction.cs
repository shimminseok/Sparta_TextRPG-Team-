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
            EnterBattleAction.pivotDict = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };


            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();
            EnterBattleAction.lineDic.Clear();
            BattlePlayerInfo();
            EnterBattleAction.DisplayMonsterList();
            int pivotCount = EnterBattleAction.pivotDict.Last().Key + 1;
            int playerAction = 19;
            if (skill is not null)
            {
                for (int i = 0; i < aliveMonsterList.Count; i++)
                {
                    EnterBattleAction.pivotDict.Add(pivotCount++, EnterBattleAction.pivotArr[i]);
                    EnterBattleAction.lineDic.Add(playerAction++, $"- {i + 1} : {aliveMonsterList[i].Name}");
                }

                EnterBattleAction.lineDic[17] = "[ 스킬 대상 지정 ]";
                if (skill.SkillAttackType == SkillAttackType.Select)
                {
                    for (int i = 0; i < aliveMonsterList.Count; i++)
                    {
                        SubActionMap.Add(i + 1,
                            new AttackAction(new List<Monster>() { aliveMonsterList[i] }, skill, PrevAction));
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
            else
            {
                for (int i = 0; i < aliveMonsterList.Count; i++)
                {
                    EnterBattleAction.pivotDict.Add(pivotCount++, EnterBattleAction.pivotArr[i]);
                    EnterBattleAction.lineDic.Add(playerAction++, $"- {i + 1} : {aliveMonsterList[i].Name}");

                    SubActionMap.Add(i + 1,
                        new AttackAction(new List<Monster>() { aliveMonsterList[i] }, skill, PrevAction));
                }

                for (int i = playerAction; i < 22; i++)
                {
                    EnterBattleAction.lineDic.Add(i, "");
                }
            }

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect, EnterBattleAction.pivotDict,
                    (18, EnterBattleAction.lineDic), EnterBattleAction.monsterUIList));
        }

        public void BattlePlayerInfo()
        {
            string name = PlayerInfo.Monster.Name;
            int level = PlayerInfo.Monster.Lv;
            float maxHP = PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue;
            float curHP = PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue;

            float maxMP = PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue;
            float curMP = PlayerInfo.Monster.Stats[StatType.CurMp].FinalValue;


            EnterBattleAction.lineDic = new Dictionary<int, string>
            {
                // 플레이어 정보
                { 0, $"{level}" },
                { 1, $"{name}" },
                { 2, $"HP : {curHP}/{maxHP}" },
                { 3, StringUtil.GetBar(curHP, maxHP) },
                { 4, $"PP : {curMP}/{maxMP}" },
                { 5, StringUtil.GetBar(curMP, maxMP) },
            };
        }
    }
}