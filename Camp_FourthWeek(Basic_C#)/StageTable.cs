namespace Camp_FourthWeek_Basic_C__;

public class StageTable
{
    private static readonly StageTable instance = new StageTable();
    public static StageTable Instance => instance;

    public Dictionary<int, Stage> StageDic { get; } = new Dictionary<int, Stage>()
    {
        {
            1,
            new Stage(1, "1 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1000)
        },
        {
            2,
            new Stage(2, "2 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1700)
        },
        {
            3,
            new Stage(3, "3 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini
                ],
                2500)
        },
        {
            4,
            new Stage(4, "4 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                    MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto
                ],
                1000)
        },
        {
            5,
            new Stage(5, "5 스테이지",
            [
                MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
            ], 1700)
        },
        {
            6,
            new Stage(6, "6 스테이지",
            [
                MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
                MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur, MonsterType.Pidgeot,
            ], 2500)
        },
        {
            7,
            new Stage(7, "7 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1000)
        },
        {
            8,
            new Stage(8, "8 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1700)
        },
        {
            9,
            new Stage(9, "9 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 2500)
        },
        {
            10,
            new Stage(10, "10 스테이지",
            [
                MonsterType.Articuno, MonsterType.Zapdos, MonsterType.Moltres, MonsterType.Stakataka
            ], 2500)
        },
    };

    public Stage? GetDungeonById(int _id)
    {
        if (StageDic.TryGetValue(_id, out var dungeon))
        {
            return dungeon;
        }

        Console.WriteLine("던전이 테이블에 등록되지 않았음");
        return null;
    }
}