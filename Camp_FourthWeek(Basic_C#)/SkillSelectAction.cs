namespace Camp_FourthWeek_Basic_C__
{

    public class SkillSelectAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };
        private List<Skill> skills = new();

        public SkillSelectAction(IAction _prevAction, List<int> _monsterUIList, Dictionary<int, string> _lineDic)
        {
            PrevAction = _prevAction;
            monsterUIList = _monsterUIList;
            lineDic = _lineDic;
        }

        public override string Name => $"스킬 선택";

        public override void OnExcute()
        {
            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            int pivotCount = 2;
            int playerAction = 19;

            int skillCount = 24;
            lineDic.Add(18, "[스킬 정보]");

            skills = SkillTable.SkillDataDic.Values
            .Where(item => PlayerInfo.Skills.Contains(item.Id))
            .ToList();

           foreach(var value in skills)
            {
                lineDic.Add(skillCount++,$"- {value.Id} : {value.Name}" );
                lineDic.Add(skillCount++, value.Description);
                lineDic.Add(skillCount++, "");
            }
           for(int i = skillCount; i< 40; i++)
            {
                lineDic.Add(i, "");
            }

            var message = string.Empty;
            for (int i = 0; i < skills.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new AttackSelectAction(PrevAction, skills[i],lineDic,pivotDict,monsterUIList));
            }

            for (int i = playerAction; i < 22; i++)
            {
                lineDic.Add(i, "");
            }
            for(int i = 2; i < monsterUIList.Count+1; i++)
            {
                pivotDict.Add(i, pivotArr[i - 2]);
                pivotCount++;
            }
            
            pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            monsterUIList.Add(28);

            SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Battle_AttackSelect, pivotDict, (25,lineDic), monsterUIList));

            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }
        }
    }
}
