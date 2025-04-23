namespace Camp_FourthWeek_Basic_C__;

public static class ExpTable
{
    public static Dictionary<int, int> ExpDataDic { get; private set; } = new Dictionary<int, int>()
    {
        {
            1, 0
        },
        {
            2, 10
        },
        {
            3, 20
        },
        {
            4, 30
        },
        {
            5, 40
        },
        {
            6, 50
        },
        {
            7, 60
        },
        {
            8, 70
        },
        {
            9, 80
        },
        {
            10, 90
        },
        {
            11, 100
        },
        {
            12, 110
        },
        {
            13, 120
        },
        {
            14, 130
        },
        {
            15, 140
        },
        {
            16, 150
        },
        {
            17, 160
        },
        {
            18, 170
        },
        {
            19, 190
        },
        {
            20, 200
        }
    };

    public static int GetExpByLevel(int _level)
    {
        if (ExpDataDic.TryGetValue(_level, out int exp))
        {
            return exp;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Exp not found");
        Console.ResetColor();
        return 0;
    }
}