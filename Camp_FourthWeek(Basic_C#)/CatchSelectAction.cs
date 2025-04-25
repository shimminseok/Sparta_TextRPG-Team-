namespace Camp_FourthWeek_Basic_C__
{
    internal class CatchSelectAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };
        public CatchSelectAction(IAction _prevAction, Dictionary<int, string>? _lineDic = null
            , Dictionary<int, Tuple<int, int>?> _pivotDict = null, List<int> _monsterList = null)
        {
            PrevAction = _prevAction;
            lineDic = _lineDic;
            pivotDict = _pivotDict;
            monsterUIList = _monsterList;
        }

        public override string Name => "포획하기";

        public override void OnExcute()
        {           //  EnterBattleAction.DisplayMonsterList();
            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            int pivotCount = 2;
            int playerAction = 19;
            lineDic[18] ="[ 포획 대상 지정 ]";
            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();

            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                pivotDict.Add(pivotCount++, pivotArr[i]);
                lineDic.Add(playerAction++, $"- {i + 1} : {aliveMonsterList[i].Name}");
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(aliveMonsterList[i], PrevAction));
            }


            for (int i = playerAction; i < 22; i++)
            {
                lineDic.Add(i, "");
            }


            SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Battle_AttackSelect, pivotDict, (18, lineDic), monsterUIList));
        }
    }
}
