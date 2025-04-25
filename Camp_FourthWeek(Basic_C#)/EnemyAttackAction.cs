using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnemyAttackAction : ActionBase
    {
        static Dictionary<int, Tuple<int, int>?> pivotDict;

        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[]
            { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };

        public override string Name => "적 턴";
        private UIName uiname = UIName.Battle_AttackEnemy;
        private int num = 25;

        public EnemyAttackAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override void OnExcute()
        {
            if (EnterBattleAction.lineDic != null)
            {
                for (int i = 24; i <= 40; i++)
                {
                    EnterBattleAction.lineDic.Remove(i);
                }
            }


            var player = PlayerInfo.Monster;

            var aliveMonsters = EnterBattleAction.MonsterSelectList
                .Where(m => EnterBattleAction.MonsterStateDic[m] == MonsterState.Normal)
                .ToList();
            bool isPlayerDead = false;
            int attackCount = 24;
            foreach (var monster in aliveMonsters)
            {
                EnterBattleAction.lineDic.Add(attackCount++, $"Lv{monster.Lv} {monster.Name}의 공격!");


                var (isEvade, isCritical) = AttackAction.CalculateBattleChances(monster, player);

                float beforeHp = player.Stats[StatType.CurHp].FinalValue;

                float damage = AttackAction.GetCalculatedDamage(monster.Stats[StatType.Attack].FinalValue);
                if (isCritical)
                    damage *= monster.Stats[StatType.CriticlaDamage].FinalValue;
                damage = (int)Math.Round(damage, 0);
                if (!isEvade)
                    player.Stats[StatType.CurHp].ModifyAllValue(damage);

                float afterHp = player.Stats[StatType.CurHp].FinalValue;
                isPlayerDead = afterHp <= 0;

                if (isEvade)
                {
                    EnterBattleAction.lineDic.Add(attackCount++, $"그러나 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        EnterBattleAction.lineDic.Add(attackCount++, $"급소에 맞았다!");
                        EnterBattleAction.lineDic.Add(attackCount++, $"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    }
                    else
                    {
                        EnterBattleAction.lineDic.Add(attackCount++, $"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    }

                    EnterBattleAction.lineDic.Add(attackCount++,
                        $"{beforeHp} -> {(isPlayerDead ? "Dead" : afterHp.ToString())}");
                }

                EnterBattleAction.lineDic.Add(attackCount++, $"");
                if (isPlayerDead)
                    break;
                Console.WriteLine("\n");
            }


            pivotDict = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };
            int pivotCount = 2;
            for (int i = 0; i < EnterBattleAction.monsterUIList.Count - 1 && i < pivotArr.Length; i++)
            {
                pivotDict.Add(pivotCount++, pivotArr[i]);
            }

            pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            EnterBattleAction.monsterUIList.Add(28);
            CheckBattleEnd(isPlayerDead);
            if (SubActionMap.Count == 0)
                return;
            attackCount = 24;
            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiname, EnterBattleAction.pivotDict, (num, EnterBattleAction.lineDic),
                    EnterBattleAction.monsterUIList));
        }

        private void CheckBattleEnd(bool isPlayerDead)
        {
            if (isPlayerDead)
            {
                uiname = UIName.Battle_Result;
                EnterBattleAction.pivotDict = null;
                num = 20;
                EnterBattleAction.monsterUIList = null;

                NextAction = new ResultAction(false, new MainMenuAction());
                return;
            }

            SubActionMap[1] = PrevAction;
        }
    }
}