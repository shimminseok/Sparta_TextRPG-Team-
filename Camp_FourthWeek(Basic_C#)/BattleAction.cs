namespace Camp_FourthWeek_Basic_C__
{
    public class BattleAction : ActionBase
    {
        public static Monster[]? monsters;
        public BattleAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
            SubActionMap = new Dictionary<int, IAction>
            {
                { 1, new AttackSelectAction(this) },
                { 2, new SkillSelectAction(this) }
            };
        }
        public override string Name => "배틀";

        public override void OnExcute()
        {
            Random rand = new Random();
            if (monsters == null)
            {
                monsters = new Monster[rand.Next(0, 4) + 1];
                for (int i = 0; i < monsters.Length; i++)
                {
                    monsters[i] = new Monster(MonsterTable.MonsterDataDic[(MonsterType)rand.Next(1, MonsterTable.MonsterDataDic.Count + 1)]);
                }
            }
            Console.WriteLine("Battle!!!");
            Console.WriteLine();
            ShowMonsterList();
            Console.WriteLine($"[내 정보]");
            Console.WriteLine($"Lv.{LevelManager.CurrentLevel} ");
            Console.WriteLine($"이 름 : {PlayerInfo.Monster.Name}");
            Console.WriteLine($"H P: {PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}/{PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue}");
            Console.WriteLine($"M P: {PlayerInfo.Monster.Stats[StatType.CurMp].FinalValue}/{PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue}");
            Console.WriteLine("");

            SelectAndRunAction(SubActionMap);
        }
        public static void ShowMonsterList(bool isShowList = false)
        {
            for (int i = 0; i < monsters.Length; i++)
            {
                Monster? monster = monsters[i];
                Console.WriteLine($"{(isShowList ? i : "")} Lv.{monster.Lv}  {monster.Name}  HP {monster.Stats[StatType.CurHp].FinalValue}/{monster.Stats[StatType.MaxHp].FinalValue}");
            }

            Console.WriteLine();
        }
    }
}
