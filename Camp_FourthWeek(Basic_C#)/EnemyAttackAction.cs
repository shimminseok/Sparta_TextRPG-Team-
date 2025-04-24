using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnemyAttackAction : ActionBase
    {
        public override string Name => "적 턴";
        public EnemyAttackAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }
        public override void OnExcute()
        {
            var player = PlayerInfo.Monster;

            var aliveMonsters = EnterBattleAction.MonsterSelectList
                .Where(m => EnterBattleAction.MonsterStateDic[m] == MonsterState.Normal)
                .ToList();
            bool isPlayerDead = false;
            foreach (var monster in aliveMonsters)
            {
                var (isEvade, isCritical) = AttackAction.CalculateBattleChances(monster, player);

                float beforeHp = player.Stats[StatType.CurHp].FinalValue;

                float damage = AttackAction.GetCalculatedDamage(monster.Stats[StatType.Attack].FinalValue);
                if (isCritical)
                    damage *= monster.Stats[StatType.CriticlaDamage].FinalValue;

                if (!isEvade)
                    player.Stats[StatType.CurHp].ModifyAllValue(damage);

                float afterHp = player.Stats[StatType.CurHp].FinalValue;
                isPlayerDead = afterHp <= 0;

                Console.WriteLine($"Lv{monster.Lv} {monster.Name}의 공격!");
                if (isEvade)
                {
                    Console.WriteLine("그러나 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        Console.WriteLine($"급소에 맞았다!");
                    }
                    Console.WriteLine($"{player.Name}을(를) 맞췄습니다 [데미지 : {damage}]");
                    Console.WriteLine($"Lv.{player.Lv} {player.Name}");
                    Console.WriteLine($"{beforeHp} -> {(isPlayerDead ? "Dead" : afterHp.ToString())}\n");
                }
                if (isPlayerDead)
                    break;
            }
            AttackAction.InputNumber();
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
