using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnterBattleAction : ActionBase
    {
        private Monster monster;
        public static List<Monster> MonsterSelectList = new List<Monster>(); //랜덤으로 선택된 몬스터를 넣을 공간
        //private bool isMonsterListInitialized = false;

        public EnterBattleAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
            SubActionMap = new Dictionary<int, IAction>
            {
                { 1, new AttackSelectAction(this) }
            };
        }

        public override string Name => "전투 시작";

        public override void OnExcute()
        {
            bool isValidInput = false; //while문을 돌리기 위해
            Console.Clear();
            if(!(MonsterSelectList.Count > 0)) //(!isMonsterListInitialized)// //메인에 돌아갔다 와도 교체 되지 않음. //클리어(몬스터가 모두 dead)이후 교체도 해야함.
            {
                RandomMonster(); //몬스터 리스트 출력
                //isMonsterListInitialized = true;
            }

            DisplayMonsterList();

            BattlePlayerInfo(); //플레이어 정보 출력

            SelectAndRunAction(SubActionMap); 
        }

        public void RandomMonster()
        {
            MonsterSelectList.Clear();
            Console.WriteLine("\n");

            List<Monster> MonstersAllList = MonsterTable.MonsterDataDic.Values.ToList(); //몬스터 테이블의 모든 몬스터 정보

            Random rand = new Random(); //랜덤값을 쓰겠습니다.
            int randomNum = rand.Next(1, 5); //1부터 4까지의 랜덤값을 randomNum에 넣음 -> 랜덤한 수를 출력
         
            for (int i = 0; i < randomNum; i++) //-> 랜덤한 몬스터 출력
            {
                int randomMonsterNum = rand.Next(0, MonstersAllList.Count); //0부터 전체 몬스터의 수만큼 랜덤한 수를 randomMonsterNum에 넣음
                //랜덤한 몬스터 1개를 선택한다. 새로운 몬스터를 만들어서 그 몬스터에 정보를 넘겨준다.
                //Monster newMonster = MonstersAllList[randomMonsterNum].copy();
                MonsterSelectList.Add(MonstersAllList[randomMonsterNum].Copy()); //MonsterAllList의 Copy()에서 정보를 랜덤한 수(randomMonsterNum)로 골라 MonsterSelectList에 넣음.
                                                                                 //-> 동일한 정보를 가진 몬스터의 피를 일괄로 깎지 않기 위해!
            } 
        }

        public static void DisplayMonsterList() // 몬스터 hp <= 0이면 dead 표시, 색 변경
        {
            foreach (Monster monster in MonsterSelectList) //-> 최종적으로 화면에 출력하기 위해
            {
                float maxHP = monster.Stats[StatType.MaxHp].FinalValue; //maxHP를 가져옴
                float curHP = monster.Stats[StatType.CurHp].FinalValue;

                string monsterDead = monster.Stats[StatType.CurHp].FinalValue <= 0 ? "Dead" : monster.Stats[StatType.CurHp].FinalValue.ToString();
                Console.WriteLine($"Lv. {monster.Lv} {monster.Name} HP {monsterDead}"); //화면에 출력
            }
        }

        public void BattlePlayerInfo()
        {
            Console.WriteLine("\n[내정보]");
            string name = PlayerInfo.Name;
            int level = PlayerInfo.Monster.Lv;
            float maxHP = PlayerInfo.Stats[StatType.MaxHp].BaseValue;
            float curHP = PlayerInfo.Stats[StatType.CurHp].BaseValue;

            float maxMP = PlayerInfo.Stats[StatType.MaxMp].BaseValue;
            float curMP = PlayerInfo.Stats[StatType.CurMp].BaseValue;

            Console.WriteLine($"Lv.{level}  {name}");
            Console.WriteLine($"HP {curHP}/{maxHP}");
            Console.WriteLine($"MP {curMP}/{maxMP}");
        }
    }
}
