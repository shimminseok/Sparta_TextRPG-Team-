namespace Camp_FourthWeek_Basic_C__;

public static class MonsterTable
{
    public static readonly Dictionary<MonsterType, Monster> MonsterDataDic = new Dictionary<MonsterType, Monster>()
    {
        {
            MonsterType.Pikachu, new Monster(MonsterType.Pikachu, "피카츄", new Dictionary<StatType, Stat>()
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
            }, [1, 2])
        },
        {
            MonsterType.Charmander, new Monster(MonsterType.Charmander, "파이리", new Dictionary<StatType, Stat>()
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
            }, [1, 2])
        },
        {
            MonsterType.Squirtle, new Monster(MonsterType.Squirtle, "꼬부기", new Dictionary<StatType, Stat>()
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
            }, [1, 2])
        },
        {
            MonsterType.Bulbasaur, new Monster(MonsterType.Bulbasaur, "이상해씨", new Dictionary<StatType, Stat>()
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
            }, [1, 2])
        },
        {
            MonsterType.Pidgey, new Monster(MonsterType.Pidgey, "구구", new Dictionary<StatType, Stat>()
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
            }, [1, 2])
        },
        {
            MonsterType.Stakataka, new Monster(MonsterType.Stakataka, "차곡차곡", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 50) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 40) }
            }, [1, 2])
        }
    };

    public static Monster GetMonsterByType(MonsterType _monsterType)
    {
        if (MonsterDataDic.TryGetValue(_monsterType, out Monster monster))
        {
            return monster;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Monster not found");
        Console.ResetColor();
        return null;
    }
}