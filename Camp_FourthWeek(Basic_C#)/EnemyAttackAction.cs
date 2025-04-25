using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnemyAttackAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };
        public override string Name => "적 턴";
        public EnemyAttackAction(IAction _prevAction, List<int> _monsterUIList, Dictionary<int, string> _lineDic)
        {
            PrevAction = _prevAction;
            monsterUIList = _monsterUIList;
            lineDic = _lineDic;
        }
        public override void OnExcute()
        {
            if(lineDic != null)
            {
                for (int i = 24; i <= 40; i++)
                {
                    lineDic.Remove(i);
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
                lineDic.Add(attackCount++, $"Lv{monster.Lv} {monster.Name}의 공격!");


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
                    lineDic.Add(attackCount++, $"그러나 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        lineDic.Add(attackCount++, $"급소에 맞았다!");
                        lineDic.Add(attackCount++, $"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    }
                    else
                    {
                        lineDic.Add(attackCount++, $"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    }
                    //Console.WriteLine($"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    // Console.WriteLine($"Lv.{player.Lv} {player.Name}");
                    lineDic.Add(attackCount++, $"{beforeHp} -> {(isPlayerDead ? "Dead" : afterHp.ToString())}");

                    //  Console.WriteLine($"{beforeHp} -> {(isPlayerDead ? "Dead" : afterHp.ToString())}\n");
                }
                lineDic.Add(attackCount++, $"");
                if (isPlayerDead)
                    break;
                Console.WriteLine("\n");
            }
  

            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            int pivotCount = 2;
            for (int i = 0; i < monsterUIList.Count - 2&& i < pivotArr.Length; i++)
            {
                pivotDict.Add(pivotCount++, pivotArr[i]);
            }
            pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            monsterUIList.Add(28);

            while (true)
            {
                string input = UiManager.UIUpdater(UIName.Battle_AttackEnemy, pivotDict, (45, lineDic), monsterUIList);

                if (int.TryParse(input, out int number))
                {
                    if (number == 1)
                        break;
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
                else
                    Console.WriteLine("잘못된 입력입니다.");
            }
            CheckBattleEnd(isPlayerDead);

            PrevAction?.Execute();
        }
        private void CheckBattleEnd(bool isPlayerDead)
        {
            if (isPlayerDead)
            {
                SubActionMap[1] = new ResultAction(false, new MainMenuAction());
                SubActionMap[1].Execute();
            }
        }

    }
}
