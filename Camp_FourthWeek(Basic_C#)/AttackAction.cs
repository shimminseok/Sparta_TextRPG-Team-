using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackAction : ActionBase
    {
        private Monster monster;
        public override string Name => $"Lv. {monster.Lv} {monster.Name} 공격";

        public override void OnExcute()
        {
            float originalDamage = PlayerInfo.Monster.Stats[StatType.Attack].FinalValue;
            float minDamage = originalDamage * 0.9f; //공격력의 최소값
            float maxDamage = originalDamage * 1.1f; //공격력의 최대값

            Random random = new Random();
            float randomNum = (float)random.NextDouble() * (maxDamage - minDamage); //최소값과 최대값 사이에서 랜덤 값 구하기
            float roundedValue = (float)Math.Round(randomNum, 0); //소수점 1자리에서 반올림

            float damage = maxDamage - roundedValue; //공격력에서 10% 오차가 생긴 데미지

            float monsterOriginHp = monster.Stats[StatType.CurHp].FinalValue;

            //메세지 수정 -> 
            Console.WriteLine("Battle");
            Console.WriteLine($"{PlayerInfo.Monster.Name}의 공격!");
            Console.WriteLine($"Lv.{monster.Lv} {monster.Name}을(를) 맞췄습니다. [데미지 : {damage}]\n");
            Console.WriteLine($"Lv.{monster.Lv} {monster.Name}");
            
            //여기서 공격
            monster.Stats[StatType.CurHp].ModifyAllValue(damage);

            //FinalValue <= 0인 경우 Dead로 바꿔주기
            if (monster.Stats[StatType.CurHp].FinalValue <= 0)
                EnterBattleAction.MonsterStateDic[monster] = MonsterState.Dead;

            // 몬스터 체력이 모두 0인지 확인
            bool isAllMonstersDead = EnterBattleAction.MonsterSelectList.All(m => m.Stats[StatType.CurHp].FinalValue <= 0);

            Console.WriteLine($"HP {monsterOriginHp} -> " +
                $"{(EnterBattleAction.MonsterStateDic[monster] == MonsterState.Dead ? "Dead" : $"{monster.Stats[StatType.CurHp].FinalValue}")}");

            //enemy턴 =====================================================================================================여기서 dead시 공격 못하게
            Console.WriteLine("1. 다음");
            Console.Write(">> ");

            InputNumber();
            Console.Clear();
            bool isPlayerDead = false;
            float playerCurHpStatus = 0; //플레이어의 최종 HP를 ResultAction에서 사용하기 위해
            List<Monster> aliveMonsterList = new List<Monster>();

            foreach (Monster mon in EnterBattleAction.MonsterSelectList)
            {
                if (EnterBattleAction.MonsterStateDic[mon] == MonsterState.Normal)
                {
                    aliveMonsterList.Add(mon);
                }
            }
            foreach (var monster in aliveMonsterList)
            {
                float playerOriginHp = PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue;
                PlayerInfo.Monster.Stats[StatType.CurHp].ModifyAllValue(monster.Stats[StatType.Attack].FinalValue);
                string playerDead = PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue <= 0 ? "Dead" : PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue.ToString();
                
                Console.WriteLine($"Lv{monster.Lv}  {monster.Name}의 공격!");
                Console.WriteLine($"{PlayerInfo.Monster.Name}을(를) 맞췄습니다. [데미지 : {monster.Stats[StatType.Attack].FinalValue}]");

                Console.WriteLine($"Lv.{PlayerInfo.Monster.Lv}  {PlayerInfo.Monster.Name}");
                Console.WriteLine($"{playerOriginHp} -> {playerDead}\n");

                if (playerDead == "Dead") //플레이어가 죽으면 출력 종료
                {
                    //Dead일 때 break;
                    isPlayerDead = (playerDead == "Dead");
                    break;
                }
            }

            if (!isPlayerDead) //플레이어 HP가 0보다 큰 경우(ResultAction에서 승리 시 플레이어의 HP출력하기 위해)
            {
                playerCurHpStatus = PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue;
            }

            Console.WriteLine("1. 다음");
            InputNumber();
            //결과 실행
            //성공 실패
            if (isAllMonstersDead) //몬스터가 다 죽었을 때
            {
                SubActionMap[1] = new ResultAction(true, this, playerCurHpStatus);
                SubActionMap[1].Execute();
            }
            if(isPlayerDead)  //내가 죽었을 때
            {
                SubActionMap[1] = new ResultAction(false, this, playerCurHpStatus);
                SubActionMap[1].Execute();
            }
            
            //1. 모든 몬스터의 HP가 0일때,
            //승패판정이 나야할 때,
            //true, false 값 isWin에 넣고
            //SubActionMap[1] = new ResultAction(true, this);
            //

            //2. 모든 몬스터가 HP가 0이 안되면
            //prevActin 실행 //지금 이 코드

            if (PrevAction != null)
            {
                PrevAction.Execute();
            }
        }

        public void InputNumber()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                {
                    if (number == 1)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public AttackAction(Monster _monster, IAction _prevAction)
        {
            PrevAction = _prevAction;
            monster = _monster;
        }
    }
}
