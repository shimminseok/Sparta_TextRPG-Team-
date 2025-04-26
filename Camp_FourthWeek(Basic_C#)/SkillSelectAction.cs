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
            SubActionMap.Clear();
            AttackActionBase.battleLogDic.Clear();
            AttackActionBase.uiPivotDic.Clear();
            AttackActionBase.monsterIconList.Clear();

            AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };

            // 플레이어 정보 출력
            AttackActionBase.BattlePlayerInfo();

            skills = SkillTable.SkillDataDic.Values
                .Where(skill => GameManager.Instance.PlayerInfo.Skills.Contains(skill.Id))
                .ToList();

            int skillInfoLine = 24;

            AttackActionBase.battleLogDic[18] = "";
            int playerActionLog = AttackActionBase.battleLogDic.Last().Key + 1;

            foreach (var skill in skills)
            {
                AttackActionBase.battleLogDic[skillInfoLine++] = $"- {skill.Id} : {skill.Name}";
                AttackActionBase.battleLogDic[skillInfoLine++] = skill.Description;
                AttackActionBase.battleLogDic[skillInfoLine++] = "";
            }

            for (int i = skillInfoLine; i < 40; i++)
            {
                AttackActionBase.battleLogDic[i] = "";
            }

            for (int i = 0; i < skills.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new AttackSelectAction(PrevAction, skills[i]));
            }

            for (int i = playerActionLog; i < 22; i++)
            {
                AttackActionBase.battleLogDic.Add(i, "");
            }

            AttackActionBase.monsterIconList.Add(28); // 하단 커서 추가

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                    AttackActionBase.uiPivotDic,
                    (18, AttackActionBase.battleLogDic),
                    AttackActionBase.monsterIconList));
        }
    }
}