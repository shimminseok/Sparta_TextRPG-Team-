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
            SubActionMap.Clear();
            EnterBattleAction.DisplayMonsterList();

            List<Monster> aliveMonsterList = EnterBattleAction.GetAliveMonsters();

            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(aliveMonsterList[i], PrevAction));
            }


            SelectAndRunAction(SubActionMap);
        }
    }
}
