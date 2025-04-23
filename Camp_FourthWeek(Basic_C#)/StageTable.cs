using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public static class StageTable
{
    public static Dictionary<int, Dungeon> StageDic { get; } = new Dictionary<int, Dungeon>()
    {
        {
            1,
            new Dungeon("1 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1000)
        },
        {
            2,
            new Dungeon("2 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey
                ],
                1700)
        },
        {
            3,
            new Dungeon("3 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini
                ],
                2500)
        },
        {
            4,
            new Dungeon("4 스테이지",
                [
                    MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                    MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                    MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto
                ],
                1000)
        },
        {
            5,
            new Dungeon("5 스테이지",
            [
                MonsterType.Pikachu, MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur,
                MonsterType.Pidgey, MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
            ], 1700)
        },
        {
            6,
            new Dungeon("6 스테이지",
            [
                MonsterType.Dratini, MonsterType.Raichu, MonsterType.Charmeleon,
                MonsterType.Wartortle, MonsterType.Ivysaur, MonsterType.Pidgeotto, MonsterType.Dragonair,
                MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur, MonsterType.Pidgeot,
            ], 2500)
        },
        {
            7,
            new Dungeon("7 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1000)
        },
        {
            8,
            new Dungeon("8 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 1700)
        },
        {
            9,
            new Dungeon("9 스테이지",
            [
                MonsterType.Dragonair, MonsterType.Charizard, MonsterType.Blastoise, MonsterType.Venusaur,
                MonsterType.Pidgeot, MonsterType.Snorlax, MonsterType.Pachirisu
            ], 2500)
        },
        {
            10,
            new Dungeon("10 스테이지",
            [
                MonsterType.Articuno, MonsterType.Zapdos, MonsterType.Moltres, MonsterType.Stakataka
            ], 2500)
        },
    };

    public static Dungeon? GetDungeonById(int _id)
    {
        if (StageDic.TryGetValue(_id, out var dungeon))
        {
            return null;
        }

        Console.WriteLine("던전이 테이블에 등록되지 않았음");
        return null;
    }
}