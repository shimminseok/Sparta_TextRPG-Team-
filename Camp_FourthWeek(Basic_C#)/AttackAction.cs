using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string monsterDead = monster.Stats[StatType.CurHp].FinalValue <= 0 ? "Dead" : monster.Stats[StatType.CurHp].FinalValue.ToString();

            Console.WriteLine($"{monsterOriginHp} -> {monsterDead}");

/*            if (PrevAction != null)
            {
                PrevAction.Execute();
            }*/

            SelectAndRunAction(SubActionMap);
        }

        public AttackAction(Monster _monster, IAction _prevAction)
        {
            PrevAction = _prevAction;
            monster = _monster;
        }
    }
}
