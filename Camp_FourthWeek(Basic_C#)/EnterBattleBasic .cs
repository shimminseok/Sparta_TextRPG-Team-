using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnterBattleBasic : ActionBase
    {
        public EnterBattleBasic(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override string Name => "전투 시작";

        public override void OnExcute()
        {
            RandomMonster();

            Console.WriteLine("[내정보]");


            LinePrint();
           
            if (!inputCheck)
            {
                Console.WriteLine("잘못된 입력입니다.");
                LinePrint();
            }
            else if(inputCheck)
            {
                if(input == 1)
                {
                    Console.WriteLine("공격");
                }
                else if(input == 2)
                {
                    Console.WriteLine("도망치기");
                }
                else { Console.WriteLine("1과 2외의 잘못된 입력입니다."); LinePrint(); }
            }

            //몬스터 1~4마리 랜덤 등장
            //몬스터 정보 - LV 이름 HP ATK

            //1. Battle!! -> 출력
            // 몬스터 정보 출력

            //2. [내정보] -> 출력
            //Ex) Lv.1 Chad (전사)
            //Ex) HP 100/100

            //3. 1. 공격
            // 원하시는 행동을 입력해주세요. >>
        }

        public void RandomMonster()
        {
            Console.WriteLine("Battle!!");

            List<Monster> MonstersAllList = MonsterTable.MonsterDataDic.Values.ToList(); //몬스터 테이블의 모든 몬스터 정보
            List<Monster> MonsterSelectList = new List<Monster>(); //랜덤으로 선택된 몬스터를 넣을 공간

            Random rand = new Random(); //랜덤값을 쓰겠습니다.
            int randomNum = rand.Next(1, 5); //1부터 4까지의 랜덤값을 randomNum에 넣음 -> 랜덤한 수를 출력


            for (int i = 0; i < randomNum; i++) //-> 랜덤한 몬스터 출력
            {
                int randomMonsterNum = rand.Next(0, MonstersAllList.Count); //0부터 전체 몬스터의 수만큼 랜덤한 수를 randomMonsterNum에 넣음
                MonsterSelectList.Add(MonstersAllList[randomMonsterNum]); //MonsterAllList의 정보를 랜덤한 수(randomMonsterNum)로 골라 MonsterSelectList에 넣음.
            }

            foreach (Monster monster in MonsterSelectList) //-> 최종적으로 화면에 출력하기 위해
            {
                float maxHP = monster.Stats[StatType.MaxHp].FinalValue; //maxHP를 가져옴

                Console.WriteLine($"Lv. {monster.Lv} {monster.Name} HP {maxHP}"); //화면에 출력
            }
        }

        public void LinePrint()
        {
            Console.WriteLine($"\n1. 공격");
            Console.WriteLine("2. 도망치기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            string PlayerInputNumber = Console.ReadLine();
            inputCheck = int.TryParse(PlayerInputNumber, out input);
        }

        private int input = -1;
        private bool inputCheck = true;

    }
}
