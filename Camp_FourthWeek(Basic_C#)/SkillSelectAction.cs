namespace Camp_FourthWeek_Basic_C__
{
    public class SkillSelectAction : ActionBase
    {
        private List<Skill> skills = new();

        public SkillSelectAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override string Name => $"스킬 선택";

        public override void OnExcute()
        {
            EnterBattleAction.lineDic.Clear();
            EnterBattleAction.pivotDict = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };
            int pivotCount = 2;
            int playerAction = 19;

            int skillCount = 24;
            EnterBattleAction.lineDic.Add(18, "[스킬 정보]");

            skills = SkillTable.SkillDataDic.Values
                .Where(item => PlayerInfo.Skills.Contains(item.Id))
                .ToList();

            foreach (var value in skills)
            {
                EnterBattleAction.lineDic.Add(skillCount++, $"- {value.Id} : {value.Name}");
                EnterBattleAction.lineDic.Add(skillCount++, value.Description);
                EnterBattleAction.lineDic.Add(skillCount++, "");
            }

            for (int i = skillCount; i < 40; i++)
            {
                EnterBattleAction.lineDic.Add(i, "");
            }

            var message = string.Empty;
            for (int i = 0; i < skills.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1,
                        new AttackSelectAction(PrevAction, skills[i]));
            }

            for (int i = playerAction; i < 22; i++)
            {
                EnterBattleAction.lineDic.Add(i, "");
            }

            for (int i = 2; i < EnterBattleAction.monsterUIList.Count + 1; i++)
            {
                EnterBattleAction.pivotDict.Add(i, EnterBattleAction.pivotArr[i - 2]);
                pivotCount++;
            }

            EnterBattleAction.pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            EnterBattleAction.monsterUIList.Add(28);

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect, EnterBattleAction.pivotDict,
                    (25, EnterBattleAction.lineDic), EnterBattleAction.monsterUIList));
        }
    }
}