using System.Threading;
using static Camp_FourthWeek_Basic_C__.StringUtil;

namespace Camp_FourthWeek_Basic_C__
{
    public class BattleAction : ActionBase
    {
        private static Monster[]? monsters;
        public BattleAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
            SubActionMap = new Dictionary<int, IAction>
            {
                { 1, new AttackSelectAction(monsters,this) },
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
                    monsters[i] = MonsterTable.MonsterDataDic[(MonsterType)rand.Next(1, MonsterTable.MonsterDataDic.Count + 1)];
                }
            }
            Console.WriteLine("Battle!!!");
            Console.WriteLine();
            ShowMonsterList();
            Console.WriteLine($"[내 정보]");
            Console.WriteLine($"Lv.{LevelManager.CurrentLevel} {PlayerInfo.Name} ({PlayerInfo.Monster.Name})");
            Console.WriteLine("");

            SelectAndRunAction(SubActionMap);
        }
        public void ShowMonsterList()
        {
            foreach (var monster in monsters)
                Console.WriteLine($"Lv.{monster.Lv}  {monster.Name}  HP {monster.Stats[StatType.CurHp].FinalValue}");
            Console.WriteLine();
        }
    }
}
