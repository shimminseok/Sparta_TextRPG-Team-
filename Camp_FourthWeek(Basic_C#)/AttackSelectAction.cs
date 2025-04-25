using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackSelectAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };

        private Skill? skill;
        public override string Name =>
            skill is null ? "공격하기" : $"{skill.Name} - MP {skill.Stats[StatType.CurMp].FinalValue}\n{skill.Description}";

        public override void OnExcute()
        {
            SubActionMap.Clear();
            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            int pivotCount = 2;
            int playerAction = 19;


            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();
            if(skill is not null)
            {
                lineDic = new Dictionary<int, string>();
                lineDic.Add(17, "[ 스킬 대상 지정 ]");
                if (skill.SkillAttackType== SkillAttackType.Random)
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
                pivotDict.Add(pivotCount++, pivotArr[i]);
                lineDic.Add(playerAction++, $"- {i + 1} : {aliveMonsterList[i].Name}");
                if (!SubActionMap.ContainsKey(i + 1))
                {
                    SubActionMap.Add(i + 1, new AttackAction(aliveMonsterList[i], skill, PrevAction,monsterUIList,lineDic));
                }
            }
            for(int i = playerAction; i < 22; i++)
            {
                lineDic.Add(i, "");
            }


            SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Battle_AttackSelect, pivotDict, (18, lineDic), monsterUIList));
        }

        public AttackSelectAction(IAction _prevAction, Skill? _skill = null, Dictionary<int, string>? _lineDic = null
            , Dictionary<int, Tuple<int, int>?> _pivotDict = null, List<int> _monsterList = null)
        {
            PrevAction = _prevAction;
            skill = _skill;
            lineDic = _lineDic;
            monsterUIList = _monsterList;
        }
    }
}
