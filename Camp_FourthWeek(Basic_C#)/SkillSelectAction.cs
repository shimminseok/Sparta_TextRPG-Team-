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
            skills = SkillTable.SkillDataDic.Values
            .Where(item => PlayerInfo.Skills.Contains(item.Id))
            .ToList();
            var message = string.Empty;
            for (int i = 0; i < skills.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new AttackSelectAction(this, skills[i]));
            }

            SelectAndRunAction(SubActionMap);
            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }
        }
    }
}
