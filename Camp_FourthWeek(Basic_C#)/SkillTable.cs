namespace Camp_FourthWeek_Basic_C__;

public static class SkillTable
{
    public static readonly Dictionary<int , Skill> SkillDataDic = new Dictionary<int, Skill>()
    {
        {
            1, new Skill(1, "몸통박치기", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 30) },
            })
        },
        {
            2, new Skill(2, "전광석화", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 50) },
            })
        }
    };
}