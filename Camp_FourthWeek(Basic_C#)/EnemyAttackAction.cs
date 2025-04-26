using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnemyAttackAction : AttackActionBase
    {
        public override string Name => "적 턴";
        private UIName uiname = UIName.Battle_AttackEnemy;
        private int num = 25;

        public EnemyAttackAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override void OnExcute()
        {
            SubActionMap.Clear();
            battleLogDic.Clear();
            uiPivotDic.Clear();
            monsterIconList.Clear();

            BattlePlayerInfo();
            var player = PlayerInfo.Monster;

            var aliveMonsters = battleMonsters.Where(m => monsterStates[m] == MonsterState.Normal).ToList();

            int attackLine = 24;
            bool isPlayerDead = false;

            foreach (var monster in aliveMonsters)
            {
                battleLogDic[attackLine++] = $"Lv.{monster.Lv} {monster.Name}의 공격!";

                var (isEvade, isCritical) = CalculateBattleChances(monster, player);

                float baseDamage = monster.Stats[StatType.Attack].FinalValue;
                float damage = GetCalculatedDamage(baseDamage);

                if (!isEvade)
                    ApplyDamage(player, damage, isCritical);

                float afterHp = player.Stats[StatType.CurHp].FinalValue;
                isPlayerDead = afterHp <= 0;

                if (isEvade)
                {
                    battleLogDic[attackLine++] = $"그러나 맞지 않았다.";
                }
                else
                {
                    if (isCritical)
                        battleLogDic[attackLine++] = $"급소에 맞았다!";

                    battleLogDic[attackLine++] = $"{player.Name}을(를) 맞췄습니다. [데미지 : {damage}]";
                    battleLogDic[attackLine++] = $"{afterHp} {(isPlayerDead ? "Dead" : "")}";
                }

                battleLogDic[attackLine++] = "";

                if (isPlayerDead)
                    break;
            }


            for (int i = attackLine; i < 40; i++)
            {
                battleLogDic[i] = "";
            }

            uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) } // 플레이어 포켓몬
            };
            int pivotCount = 2;
            for (int i = 0; i < pivotArr.Length && i < battleMonsters.Count; i++)
            {
                uiPivotDic.Add(pivotCount++, pivotArr[i]);
            }

            monsterIconList.Add(28); // 하단 커서

            CheckBattleEnd(isPlayerDead);

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiname, uiPivotDic, (num, battleLogDic), monsterIconList));
        }

        private void CheckBattleEnd(bool isPlayerDead)
        {
            if (isPlayerDead)
            {
                uiname = UIName.Battle_Result;
                num = 20;
                uiPivotDic.Clear();
                monsterIconList.Clear();
                battleMonsters.Clear();
                NextAction = new ResultAction(false, new MainMenuAction());
                return;
            }

            SubActionMap[1] = PrevAction;
        }
    }
}