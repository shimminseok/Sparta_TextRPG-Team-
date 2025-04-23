using Camp_FourthWeek_Basic_C__;

namespace Camp_FourthWeek_Basic_C__;

public static class SkillTable
{
    public static readonly Dictionary<int, Skill> SkillDataDic = new Dictionary<int, Skill>()
    {
        {
            1, new Skill(1, "전광석화", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 2) },
                { StatType.CurMp, new Stat(StatType.CurMp, 10) },
            },SkillAttackType.Select,1)
        },
        {
            2, new Skill(2, "번개", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 1.5f) },
                { StatType.CurMp, new Stat(StatType.CurMp, 15) },
            },SkillAttackType.Random,2)
        }
    };
}