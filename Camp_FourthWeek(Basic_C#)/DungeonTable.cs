namespace Camp_FourthWeek_Basic_C__;

public static class DungeonTable
{
    public static Dictionary<int, Dungeon> DungeonDic { get; private set; } = new Dictionary<int, Dungeon>()
    {
        { 1, new Dungeon("쉬운 던전", new Stat(StatType.Defense, 5), 1000) },
        { 2, new Dungeon("일반 던전", new Stat(StatType.Defense, 11), 1700) },
        { 3, new Dungeon("어려운 던전", new Stat(StatType.Defense, 17), 2500) },
    };

    public static Dungeon? GetDungeonById(int _id)
    {
        if (DungeonDic.TryGetValue(_id, out var dungeon))
        {
            return dungeon;
        }

        Console.WriteLine("던전이 테이블에 등록되지 않았음");
        return null;
    }
}