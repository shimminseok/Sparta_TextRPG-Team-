using System.Text;

namespace Camp_FourthWeek_Basic_C__;

#region[Enum]

public enum MonsterType
{
    None,
    Pikachu, //피카츄
    Charmander, //파이리
    Squirtle, //꼬부기
    Bulbasaur, // 이상해씨
    Pidgey, //구구
    Dratini, //미뇽

    Raichu, //라이츄
    Charmeleon, //리자드
    Wartortle, //어니부기
    Ivysaur, //이상해풀
    Pidgeotto, //피죤
    Dragonair, //신뇽

    Charizard, //리자몽
    Blastoise, //거북왕
    Venusaur, //이상해꽃
    Pidgeot, //피죤투
    Dragonite, //망나뇽

    Snorlax, //잠만보
    Pachirisu, //파치리스

    Articuno, //프리져
    Zapdos, //썬더
    Moltres, //파이어
    Stakataka //차곡차곡
}

public enum StatType
{
    Attack,
    Defense,
    MaxHp,
    CurHp,
    MaxMp,
    CurMp,
    CriticalChance,
    CriticlaDamage,
    EvadeChance
}

public enum ItemType
{
    Weapon,
    Helmet,
    Armor,
    Groves,
    Shoes
}

public enum MainManu
{
    Character = 1,
    Inventory,
    Shop,
    Dungeon,
    Quest,
    Collection,
    Rest,
    Battle,
    Reset
}

public enum SkillAttackType
{
    All,
    Random,
    Select
}

#endregion[Enum]

public interface IAction
{
    string Name { get; }

    void Execute();
    void SetFeedBackMessage(string _message);
}

public class PlayerInfo
{
    public int Gold = 1500;

    public PlayerInfo(MonsterType _monster, string _name)
    {
        Monster = MonsterTable.MonsterDataDic[_monster];
        Monsters.Add(Monster);
        Stats = Monster.Stats.ToDictionary();
        Name = _name;
        Skills = Monster.Skills;
    }

    public void ChangeMonsterStat(Monster _monster)
    {
        Monster = _monster;
        Stats.Clear();
        foreach (var kv in _monster.Stats)
        {
            Stats[kv.Key] = new Stat(kv.Key, kv.Value.BaseValue);
        }

        Skills = _monster.Skills;
    }

    public string Name { get; private set; }
    public Monster Monster { get; set; }
    public List<Monster> Monsters { get; } = new();
    public Dictionary<StatType, Stat> Stats { get; }
    public List<int> Skills { get; private set; }
}

public class Item(int _key, string _name, ItemType _type, List<Stat> _stats, string _description, int _cost)
{
    public readonly int Cost = _cost;
    public readonly string Description = _description;
    public readonly ItemType ItemType = _type;
    public readonly string Name = _name;
    public readonly List<Stat> Stats = _stats;
    public int Key { get; private set; } = _key;

    public bool IsEquippedBy(Monster m) => m.ItemId == Key;
    public bool IsEquipment => EquipmentManager.IsEquipped(this);
}

public class Stat
{
    public float BaseValue;
    public float BuffValue = 0;
    public float EquipmentValue;
    public StatType Type;

    public Stat()
    {
    }

    public Stat(Stat stat)
    {
        Type = stat.Type;
        BaseValue = stat.BaseValue;
        BuffValue = stat.BuffValue;
        EquipmentValue = stat.EquipmentValue;
    }

    public Stat(StatType _type, float _value)
    {
        Type = _type;
        BaseValue = _value;
    }

    public float FinalValue => BaseValue + EquipmentValue + BuffValue;

    public void ModifyBaseValue(float _value, float _min = 0, float _max = int.MaxValue)
    {
        BaseValue += _value;
        BaseValue = Math.Clamp(BaseValue, _min, _max);
    }

    public void ModifyEquipmentValue(float _value)
    {
        EquipmentValue += _value;
        EquipmentValue = Math.Clamp(EquipmentValue, 0, int.MaxValue);
    }

    public void ModifyAllValue(float _value, float _min = 0, float _max = int.MaxValue)
    {
        if (_value <= 0)
            _value = 1;

        var remainingDam = _value;
        if (remainingDam > 0)
        {
            var damToEquip = Math.Min(remainingDam, EquipmentValue);
            ModifyEquipmentValue(-damToEquip);
            remainingDam -= damToEquip;
        }

        if (remainingDam > 0) ModifyBaseValue(-remainingDam, 0, FinalValue);
    }

    public string GetStatName()
    {
        switch (Type)
        {
            case StatType.Attack:
                return "공격력";
            case StatType.Defense:
                return "방어력";
            case StatType.MaxHp:
                return "최대 HP";
            case StatType.CurHp:
                return "HP";
            case StatType.MaxMp:
                return "최대 MP";
            case StatType.CurMp:
                return "MP";
            case StatType.CriticalChance:
                return "치명타확률";
            case StatType.CriticlaDamage:
                return "치명타피해";
            case StatType.EvadeChance:
                return "회피확률";
            default: return string.Empty;
        }
    }
}

public class Stage
{
    //권장 방어력
    private readonly PlayerInfo playerInfo = GameManager.Instance.PlayerInfo;

    public Stage(int _key, string _stageName, MonsterType[] _monsters, int _rewardGold)
    {
        Key = _key;
        StageName = _stageName;
        SpawnedMonsters = _monsters;
        RewardGold = _rewardGold;
    }

    public int Key { get; }
    public string StageName { get; }
    public int RewardGold { get; private set; }
    public MonsterType[] SpawnedMonsters { get; }
}

public class Monster
{
    public Monster(MonsterType _type, string _name, Dictionary<StatType, Stat> _stat, List<int> _skill)
    {
        Type = _type;
        Name = _name;
        Stats = _stat;
        Skills = _skill;
    }

    public Monster(Monster _monster)
    {
        Type = _monster.Type;
        Name = _monster.Name;

        Stats = new Dictionary<StatType, Stat>();
        foreach (var stat in _monster.Stats)
        {
            Stats[stat.Key] = new Stat(stat.Value);
        }

        Skills = new List<int>(_monster.Skills);
        ItemId = _monster.ItemId;
        Lv = _monster.Lv;
        Exp = _monster.Exp;
    }

    public MonsterType Type { get; private set; }
    public string Name { get; private set; }
    public Dictionary<StatType, Stat> Stats { get; }
    public List<int> Skills { get; private set; }
    public int ItemId { get; set; }
    public int Lv { get; private set; }
    public int Exp { get; private set; }

    public Monster Copy()
    {
        //Dictionary 복제 -> Dictionary 복제하는데 Stat이 클래스라서 Stat을 복제하면서 해야함.
        Dictionary<StatType, Stat> newStat = new Dictionary<StatType, Stat>();

        foreach (var copyDict in Stats)//foreach문을 이용하여 딕셔너리의 모든 키-값에 접근할 수 있음. (foreach var '지역변수' in '딕셔너리 이름')
                                       // Stats : Dictionary<StatType, Stat>의 Stats이므로 copyDict도 <StatType, stat> 타입임
                                       // copyDict는 Stats 딕셔너리의 각 항목을 참조하는 변수 + foreach는 딕셔너리에서 항목을 자동으로 순회할 수 있기 때문에 Stats만 써도 딕셔너리의 Key와 value값을 가져올 수 있다.
        {
            Stat original = copyDict.Value; //CopyDict는 foreach문으로 딕셔너리를 순회하는 변수이므로, copyDict.value(StatType의 정보)를 original에 저장하는 결과가 된다.
            Stat copyStat = new Stat(); //Stat 객체 생성

            copyStat.Type = original.Type; //복제!
            copyStat.BaseValue = original.BaseValue;
            copyStat.BuffValue = original.BuffValue;
            copyStat.EquipmentValue = original.EquipmentValue;
            newStat[copyDict.Key] = copyStat; //newStat : Dictionary<StatType, Stat> 타입의 딕셔너리의 copyDict.Key라는 키에 copyStat 값을 주겠다.
                                              //즉 newStat의 key = copyDict.Key, newStat의 value = copyStat
        }
        //List 복제
        List<int> newSkill = new List<int>(Skills);

        Monster monster = new Monster(Type, Name, newStat, newSkill);

        return monster;
    }

    public void AddExp(int _exp)
    {
        Exp += _exp;
        int maxExp = ExpTable.GetExpByLevel(Lv + 1);
        if (Exp > maxExp) //Todo : 추후 경험치 테이블에서 현재 레벨에 맞게 값을 가져와 적용
        {
            Exp -= maxExp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Lv++;
        //Todo : 진화를 여기서 하면 화면에 어떻게 뿌릴것인가?
    }
}

public class Skill
{
    public Skill(int _id, string _name, Dictionary<StatType, Stat> _stat, SkillAttackType _skillAttackType,
        int _targetCount)
    {
        Id = _id;
        Name = _name;
        Stats = _stat;
        SkillAttackType = _skillAttackType;
        TargetCount = _targetCount;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public Dictionary<StatType, Stat> Stats { get; private set; }
    public SkillAttackType SkillAttackType { get; private set; }
    public int TargetCount { get; private set; }
}

public class SaveData
{
    public int DungeonClearCount;
    public List<int> EquipmentItem;
    public int Gold;

    // Item을 전부 변환 시킬 필요가 없음. int값만 가지고 와서 Table에서 가져오는 방식을 사용
    public List<int> Inventory;
    public MonsterType Monster;

    public string Name;

    //QuestData
    public List<SaveQeust> Quests = new List<SaveQeust>();
    public List<int> ClearQuests = new List<int>();

    //CollectionData
    public Dictionary<MonsterType, CollectionData> CollectionData = new Dictionary<MonsterType, CollectionData>();

    public SaveData(SaveData _data)
    {
        Name = _data.Name;
        Monster = _data.Monster;
        DungeonClearCount = _data.DungeonClearCount;
        Inventory = _data.Inventory;
        EquipmentItem = _data.EquipmentItem;
        Gold = _data.Gold;
        Quests = _data.Quests;
        ClearQuests = _data.ClearQuests;
        CollectionData = _data.CollectionData;
    }

    public SaveData()
    {
    }
}