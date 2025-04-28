namespace Camp_FourthWeek_Basic_C__;

public class MonsterTable
{
    private static readonly MonsterTable instance = new MonsterTable();
    public static MonsterTable Instance => instance;

    public readonly Dictionary<MonsterType, Monster> MonsterDataDic = new Dictionary<MonsterType, Monster>()
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
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Raichu)
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
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Charmeleon)
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
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Wartortle)
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
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Ivysaur)
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
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Pidgeotto)
        },
        {
            MonsterType.Dratini, new Monster(MonsterType.Dratini, "미뇽", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 30) }
            }, [1, 2], MonsterType.Dragonair)
        },
        {
            MonsterType.Raichu, new Monster(MonsterType.Raichu, "라이츄", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Charmeleon, new Monster(MonsterType.Charmeleon, "리자드", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Charizard)
        },
        {
            MonsterType.Wartortle, new Monster(MonsterType.Wartortle, "어니부기", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Blastoise)
        },
        {
            MonsterType.Ivysaur, new Monster(MonsterType.Ivysaur, "이상해풀", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Venusaur)
        },
        {
            MonsterType.Pidgeotto, new Monster(MonsterType.Pidgeotto, "피죤", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Pidgeot)
        },
        {
            MonsterType.Dragonair, new Monster(MonsterType.Dragonair, "신룡", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2], MonsterType.Dragonite)
        },
        {
            MonsterType.Charizard, new Monster(MonsterType.Charizard, "리자몽", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Blastoise, new Monster(MonsterType.Blastoise, "거북왕", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Venusaur, new Monster(MonsterType.Venusaur, "이상해꽃", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Pidgeot, new Monster(MonsterType.Pidgeot, "피죤투", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Dragonite, new Monster(MonsterType.Dragonite, "망나뇽", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Snorlax, new Monster(MonsterType.Snorlax, "잠만보", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 10) }
            }, [1, 2])
        },
        {
            MonsterType.Pachirisu, new Monster(MonsterType.Pachirisu, "파치리스", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 1000) },
                { StatType.CurHp, new Stat(StatType.CurHp, 1000) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 1000) },
                { StatType.CurMp, new Stat(StatType.CurMp, 1000) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 90) }
            }, [1, 2])
        },
        {
            MonsterType.Articuno, new Monster(MonsterType.Articuno, "프리져", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 90) }
            }, [1, 2])
        },
        {
            MonsterType.Zapdos, new Monster(MonsterType.Zapdos, "썬더", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 90) }
            }, [1, 2])
        },
        {
            MonsterType.Moltres, new Monster(MonsterType.Moltres, "파이어", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 100) },
                { StatType.CurHp, new Stat(StatType.CurHp, 100) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 100) },
                { StatType.CurMp, new Stat(StatType.CurMp, 100) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 50) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 1.5f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 90) }
            }, [1, 2])
        },
        {
            MonsterType.Stakataka, new Monster(MonsterType.Stakataka, "차곡차곡", new Dictionary<StatType, Stat>()
            {
                { StatType.Attack, new Stat(StatType.Attack, 10) },
                { StatType.Defense, new Stat(StatType.Defense, 5) },
                { StatType.MaxHp, new Stat(StatType.MaxHp, 2000) },
                { StatType.CurHp, new Stat(StatType.CurHp, 2000) },
                { StatType.MaxMp, new Stat(StatType.MaxMp, 2000) },
                { StatType.CurMp, new Stat(StatType.CurMp, 2000) },
                { StatType.CriticalChance, new Stat(StatType.CriticalChance, 90) },
                { StatType.CriticlaDamage, new Stat(StatType.CriticlaDamage, 3f) },
                { StatType.EvadeChance, new Stat(StatType.EvadeChance, 60) }
            }, [1, 2])
        }
    };

    public Monster GetMonsterByType(MonsterType _monsterType)
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