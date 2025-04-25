using System.Diagnostics;

namespace Camp_FourthWeek_Basic_C__
{
    public enum MonsterState
    {
        Normal, Dead, Catched
    }
    public class EnterBattleAction : ActionBase
    {
        static  int addId = 199;
        static  int addMonsterId = 99;
        public override string Name => "전투 시작";
        public static Stage CurrentStage { get; private set; }
        public static List<Monster> MonsterSelectList = new List<Monster>(); //랜덤으로 선택된 몬스터를 넣을 공간
        public static Dictionary<Monster,MonsterState> MonsterStateDic = new Dictionary<Monster, MonsterState>(); //랜덤으로 선택된 몬스터를 넣을 공간
        static  Dictionary<int, string> lineDic = new Dictionary<int, string>(); // UI 추가를 위한 딕셔너리
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static  List<int> monsterUIList= new List<int>();
        private List<Monster> monstersAllList = new();

        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6) , new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };


        public EnterBattleAction(Stage _currentStage, IAction _prevAction)
        {
            PrevAction = _prevAction;
            CurrentStage = _currentStage;
            SubActionMap = new Dictionary<int, IAction>
            {
                { 1, new AttackSelectAction(this) },
                { 3, new SkillSelectAction(this) },
                { 4, new CatchSelectAction(this) }
            };

            monstersAllList = CurrentStage.SpawnedMonsters
                .Select(_type => MonsterTable.MonsterDataDic[_type])
                .ToList();


            RandomMonster();
        }


        public override void OnExcute()
        {
            ClearDic();
            bool isValidInput = false; //while문을 돌리기 위해
            BattlePlayerInfo(); //플레이어 정보 출력
            DisplayMonsterList();

         
 


            SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Battle, pivotDict, (18, lineDic), monsterUIList));
        }

        public void RandomMonster()
        {
            MonsterSelectList.Clear();
            Console.WriteLine("\n");

            Random rand = new Random();
            int randomNum = rand.Next(1, 4);

            for (int i = 0; i < randomNum; i++) //-> 랜덤한 몬스터 출력
            {
                int randomMonsterNum = rand.Next(0, monstersAllList.Count); //0부터 전체 몬스터의 수만큼 랜덤한 수를 randomMonsterNum에 넣음
                //랜덤한 몬스터 1개를 선택한다. 새로운 몬스터를 만들어서 그 몬스터에 정보를 넘겨준다.
                Monster randomMonster = monstersAllList[randomMonsterNum];
                MonsterSelectList.Add(randomMonster.Copy()); //MonsterAllList의 Copy()에서 정보를 랜덤한 수(randomMonsterNum)로 골라 MonsterSelectList에 넣음.
                //-> 동일한 정보를 가진 몬스터의 피를 일괄로 깎지 않기 위해!
                MonsterStateDic[MonsterSelectList[i]] = MonsterState.Normal;
                
                CollectionManager.Instnace.OnDiscovered(randomMonster.Type);
            }
        }

        public static void DisplayMonsterList() // 몬스터 hp <= 0이면 dead 표시, 색 변경
        {
            int monsterCount = 2;
            int uiCount = 6;
            foreach (Monster monster in MonsterSelectList) //-> 최종적으로 화면에 출력하기 위해
            {
                float maxHP = monster.Stats[StatType.MaxHp].FinalValue; //maxHP를 가져옴
                float curHP = monster.Stats[StatType.CurHp].FinalValue;

                if (MonsterStateDic[monster] == MonsterState.Catched)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Lv. {monster.Lv} {monster.Name} Catched!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (MonsterStateDic[monster] == MonsterState.Dead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Lv. {monster.Lv} {monster.Name} Dead");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    // 여기가 살아있는 포켓몬
                    Console.WriteLine($"Lv. {monster.Lv} {monster.Name} HP {curHP}/{maxHP}");
                    int index = Array.IndexOf(Enum.GetValues(typeof(MonsterType)), monster.Type) + addMonsterId;
                    monsterUIList.Add(index);
                    pivotDict.Add(monsterCount,pivotArr[monsterCount - 2]);
                }
                monsterCount++;
            }
        }
        public static List<Monster> GetAliveMonsters()
        {
            var list = new List<Monster>();
            foreach (Monster mon in MonsterSelectList)
            {
                if (MonsterStateDic[mon] == MonsterState.Normal)
                {
                    list.Add(mon);
                }
            }
            return list;
        }

        public void BattlePlayerInfo()
        {
            Console.WriteLine("\n[내정보]");
            string name = PlayerInfo.Monster.Name;
            int level = PlayerInfo.Monster.Lv;
            float maxHP = PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue;
            float curHP = PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue;

            float maxMP = PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue;
            float curMP = PlayerInfo.Monster.Stats[StatType.CurMp].FinalValue;

            Console.WriteLine($"Lv.{level}  {name}");
            Console.WriteLine($"HP {curHP}/{maxHP}");
            Console.WriteLine($"MP {curMP}/{maxMP}");

            int index = Array.IndexOf(Enum.GetValues(typeof(MonsterType)), PlayerInfo.Monster.Type) + addId;
             monsterUIList.Add(index);

            lineDic = new Dictionary<int, string>
                  {   
                      // 플레이어 정보
                      {0, $"{level}"},
                      {1, $"{name}"},
                      {2, $"HP : {curHP}/{maxHP}"},
                      {3, StringUtil.GetBar(curHP,maxHP)},
                      {4, $"PP : {curMP}/{maxMP}"},
                      {5, StringUtil.GetBar(curMP,maxMP) },
                  };
        }
        public void ClearDic()
        {
            lineDic = new Dictionary<int, string>();
            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            monsterUIList = new List<int>();

        }
    }
}