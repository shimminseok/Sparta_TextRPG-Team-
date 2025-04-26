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
            AttackActionBase.battleLogDic.Clear();
            AttackActionBase.uiPivotDic.Clear();

            AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };

            // 플레이어 정보 출력
            AttackActionBase.BattlePlayerInfo();

            var aliveMonsterList = AttackActionBase.battleMonsters
                .Where(mon => AttackActionBase.monsterStates[mon] == MonsterState.Normal)
                .ToList();

            int pivotCount = AttackActionBase.uiPivotDic.Count;
            int playerActionLine = 19;

            AttackActionBase.battleLogDic[18] = "[ 포획 대상 지정 ]";

            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
                AttackActionBase.battleLogDic[playerActionLine++] = $"- {i + 1} : {aliveMonsterList[i].Name}";

                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(aliveMonsterList[i], PrevAction));
            }

            for (int i = playerActionLine; i < 22; i++)
            {
                AttackActionBase.battleLogDic[i] = "";
            }

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                    AttackActionBase.uiPivotDic,
                    (18, AttackActionBase.battleLogDic),
                    AttackActionBase.monsterIconList));
        }
    }
}