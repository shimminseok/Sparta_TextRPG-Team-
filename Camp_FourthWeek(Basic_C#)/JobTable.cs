namespace Camp_FourthWeek_Basic_C__;

public static class JobTable
{
    public static readonly Dictionary<JobType, Job> JobDataDic = new Dictionary<JobType, Job>()
    {
        {
            JobType.Warrior, new Job(JobType.Warrior, "전사", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 40) }
            }, [1,2])
        },
        {
            JobType.Assassin, new Job(JobType.Assassin, "암살자", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 15) },
                { StatType.Defense, new Stat(StatType.Defense, 3) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 80) },
                { StatType.CurHp, new Stat(StatType.CurHp, 80) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 80) },
                { StatType.CurMp, new Stat(StatType.CurMp, 80) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 40) }
            },[1,2])
        }
    };
}