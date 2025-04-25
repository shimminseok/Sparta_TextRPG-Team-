namespace Camp_FourthWeek_Basic_C__
{
    internal class CatchSelectAction : ActionBase
    {
        public CatchSelectAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override string Name => "포획하기";

        public override void OnExcute()
        {
            //  EnterBattleAction.DisplayMonsterList();
            EnterBattleAction.pivotDict = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };
            int pivotCount = 2;
            int playerAction = 19;
            EnterBattleAction.lineDic[18] = "[ 포획 대상 지정 ]";
            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();

            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                EnterBattleAction.pivotDict.Add(pivotCount++, EnterBattleAction.pivotArr[i]);
                EnterBattleAction.lineDic.Add(playerAction++, $"- {i + 1} : {aliveMonsterList[i].Name}");
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(aliveMonsterList[i], PrevAction));
            }


            for (int i = playerAction; i < 22; i++)
            {
                EnterBattleAction.lineDic.Add(i, "");
            }


            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect, EnterBattleAction.pivotDict,
                    (18, EnterBattleAction.lineDic), EnterBattleAction.monsterUIList));
        }
    }
}