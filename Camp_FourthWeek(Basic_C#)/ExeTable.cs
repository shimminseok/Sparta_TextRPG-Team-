namespace Camp_FourthWeek_Basic_C__;

public static class ExpTable
{
    public static Dictionary<int, int> ExpDataDic { get; private set; } = new Dictionary<int, int>()
    {
        { 1, 0 },
        { 2, 10 },
        { 3, 35 },
        { 4, 65 },
        { 5, 100 },
        { 6, 130 },
        { 7, 160 },
        { 8, 190 },
        { 9, 220 },
        { 10, 250 },
        { 11, 280 },
        { 12, 310 },
        { 13, 340 },
        { 14, 370 },
        { 15, 400 },
        { 16, 430 },
        { 17, 460 },
        { 18, 490 },
        { 19, 520 },
        { 20, 550 },
        { 21, 580 },
        { 22, 610 },
        { 23, 640 },
        { 24, 670 },
        { 25, 700 },
        { 26, 730 },
        { 27, 760 },
        { 28, 790 },
        { 29, 820 },
        { 30, 850 },
        { 31, 880 },
        { 32, 910 },
        { 33, 940 },
        { 34, 970 },
        { 35, 1000 },
        { 36, 1040 },
        { 37, 1080 },
        { 38, 1120 },
        { 39, 1160 },
        { 40, 1200 },
        { 41, 1240 },
        { 42, 1280 },
        { 43, 1320 },
        { 44, 1360 },
        { 45, 1400 },
        { 46, 1445 },
        { 47, 1490 },
        { 48, 1535 },
        { 49, 1580 },
        { 50, 1625 }
    };

    public static int GetExpByLevel(int _level)
    {
        if (ExpDataDic.TryGetValue(_level, out int exp))
        {
            return exp;
        }

        return -1;
    }
}