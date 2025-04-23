using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public static class StageTable
{
    public static Dictionary<int, Dungeon_1> StageDic { get; } = new Dictionary<int, Dungeon_1>()
    {
        {
            1,
            new Dungeon_1("1 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1000)
        },
        {
            2,
            new Dungeon_1("2 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1700)
        },
        {
            3,
            new Dungeon_1("3 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini
                ],
                2500)
        },
        {
            4,
            new Dungeon_1("4 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                    MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto
                ],
                1000)
        },
        {
            5,
            new Dungeon_1("5 스테이지",
            [
                MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
            ], 1700)
        },
        {
            6,
            new Dungeon_1("6 스테이지",
            [
                MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
                MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur, MonsterType.Pidgeot,
            ], 2500)
        },
        {
            7,
            new Dungeon_1("7 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1000)
        },
        {
            8,
            new Dungeon_1("8 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1700)
        },
        {
            9,
            new Dungeon_1("9 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 2500)
        },
        {
            10,
            new Dungeon_1("10 스테이지",
            [
                MonsterType.Articuno, MonsterType.Zapdos, MonsterType.Moltres, MonsterType.Stakataka
            ], 2500)
        },
    };

    public static Dungeon? GetDungeonById(int _id)
    {
        // if (StageDic.TryGetValue(_id, out var dungeon))
        {
            return null;
        }
        //
        // Console.WriteLine("던전이 테이블에 등록되지 않았음");
        // return null;
    }
}

public class Dungeon_1
{
    //권장 방어력
    private readonly PlayerInfo playerInfo = GameManager.Instance.PlayerInfo;

    public Dungeon_1(string _dungeonName, MonsterType[] _monsters, int _rewardGold)
    {
        DungeonName = _dungeonName;
        SpawnedMonsters = _monsters;
        RewardGold = _rewardGold;
    }

    public string DungeonName { get; }
    public int RewardGold { get; private set; }
    public MonsterType[] SpawnedMonsters { get; }

    public string ClearDungeon(float dam)
    {
        LevelManager.AddClearCount();
        var rand = new Random();
        var stat = playerInfo.Stats[StatType.Attack].FinalValue;
        var curHP = playerInfo.Stats[StatType.CurHp];
        RewardGold += rand.Next((int)stat, (int)(stat * 2 + 1));

        var originHP = curHP.FinalValue;
        curHP.ModifyAllValue(dam);

        var sb = new StringBuilder();
        sb.AppendLine("던전 클리어");
        sb.AppendLine("축하 합니다!!");
        sb.AppendLine($"{DungeonName}을 클리어 하였습니다.");

        sb.AppendLine("[탐험 결과]");
        sb.AppendLine($"체력 {originHP} -> {curHP.FinalValue}");
        sb.AppendLine($"Gold {playerInfo.Gold} -> {playerInfo.Gold + RewardGold}");

        playerInfo.Gold += RewardGold;

        return sb.ToString();
    }

    public string UnClearDungeon(float _dam)
    {
        var rand = new Random();

        var damage = (int)(_dam / 2);
        var curHP = playerInfo.Stats[StatType.CurHp];
        var originHP = curHP.FinalValue;

        curHP.ModifyAllValue(damage);
        var sb = new StringBuilder();
        sb.AppendLine("던전 공략 실패");
        sb.AppendLine($"{DungeonName} 공략에 실패 하였습니다.");

        sb.AppendLine("[탐험 결과]");
        sb.AppendLine($"체력 {originHP} -> {curHP.FinalValue}");

        return sb.ToString();
    }
}