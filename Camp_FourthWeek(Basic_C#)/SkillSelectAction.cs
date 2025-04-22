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
            if (PlayerInfo != null)
            {
                ShowSkillInfo();
            }

            SelectAndRunAction(SubActionMap);
            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }
        }
        private void ShowSkillInfo()
        {
            for (var i = 0; i < skills.Count; i++)
            {
                var item = skills[i];
                var sb = skills[i].Name + " " + skills[i].Stats[StatType.Attack].GetStatName() + " " + skills[i].Stats[StatType.Attack].FinalValue;

                Console.WriteLine(sb.ToString());
                Console.ResetColor();
            }
        }
    }
}
