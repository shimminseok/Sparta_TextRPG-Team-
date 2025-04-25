using System.Security.Cryptography.X509Certificates;
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

    Raichu = 11, //라이츄
    Charmeleon, //리자드
    Wartortle, //어니부기
    Ivysaur, //이상해풀
    Pidgeotto, //피죤
    Dragonair, //신뇽

    Charizard = 31, //리자몽
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
    Armor,
}

public enum MainManu
{
    Character = 1,
    Inventory,
    Shop,
    Stage,
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

    public PlayerInfo(Monster _monster, string _name)
    {
        Monster = _monster;
        InventoryManager.Instance.AddMonsterToBox(Monster);
        Name = _name;
        Skills = Monster.Skills;
    }

    public void ChangeMonsterStat(Monster _monster)
    {
        Monster = _monster;
        Skills = _monster.Skills;
    }

    public string Name { get; private set; }

    public Monster Monster { get; set; }

    // public Dictionary<StatType, Stat> Stats { get; }
    public List<int> Skills { get; private set; }
}

public class Item
{
    public int Key { get; }
    public int Cost { get; }
    public string Description { get; }
    public ItemType ItemType { get; }
    public string Name { get; }
    public List<Stat> Stats { get; }
    public int UniqueNumber { get; private set; }

    //현재 몬스터가 장착한 아이템인지
    public bool IsEquippedBy(Monster m) => m.Item?.UniqueNumber == UniqueNumber;

    //몬스터 박스에 있는 몬스터들이 장착한 아이템인지
    public bool IsEquipment => EquipmentManager.IsEquipped(UniqueNumber);

    public Item(int _key, string _name, ItemType _type, List<Stat> _stats, string _description, int _cost)
    {
        Key = _key;
        Name = _name;
        ItemType = _type;
        Stats = _stats;
        Description = _description;
        Cost = _cost;
    }

    public Item()
    {
    }

    public Item Copy()
    {
        return new Item(Key, Name, ItemType, new List<Stat>(Stats.Select(x => x.Copy(x))),
            Description,
            Cost);
    }

    public void SetUniqueNumber()
    {
        Random random = new Random();
        UniqueNumber = random.Next(0, int.MaxValue);
    }

    public void SetUniqueNumber(int _uniqueNumber)
    {
        UniqueNumber = _uniqueNumber;
    }
}

public class Stat
{
    public float BaseValue { get; private set; }
    public float BuffValue { get; private set; } = 0;
    public float LevelValue { get; private set; } = 0;
    public float EquipmentValue { get; private set; }
    public StatType Type { get; }

    public Stat()
    {
    }

    public Stat(Stat stat)
    {
        Type = stat.Type;
        BaseValue = stat.BaseValue;
        BuffValue = stat.BuffValue;
        LevelValue = stat.LevelValue;
        EquipmentValue = stat.EquipmentValue;
    }

    public Stat(SaveStat _saveStat)
    {
        Type = _saveStat.Type;
        BaseValue = _saveStat.BaseValue;
        BuffValue = _saveStat.BuffValue;
        LevelValue = _saveStat.LevelValue;
        EquipmentValue = _saveStat.EquipmentValue;
    }

    public Stat(StatType _type, float _value)
    {
        Type = _type;
        BaseValue = _value;
    }

    public float FinalValue => BaseValue + EquipmentValue + BuffValue + LevelValue;

    public void ModifyBaseValue(float _value, float _min = 0, float _max = int.MaxValue)
    {
        BaseValue += _value;
        BaseValue = Math.Clamp(BaseValue, _min, _max);
    }

    public void SetLevelValue(float _value, float _min = 0, float _max = int.MaxValue)
    {
        LevelValue = _value;
        LevelValue = Math.Clamp(LevelValue, _min, _max);
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

    public Stat Copy(Stat _stat)
    {
        return new Stat(_stat);
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
    public Monster()
    {
    }

    public Monster(SaveMonsterData _saveData)
    {
        var monster = MonsterTable.GetMonsterByType(_saveData.Key);
        Name = monster.Name;
        Skills = monster.Skills;
        EvolveType = monster.EvolveType;
        Stats = new Dictionary<StatType, Stat>();
        foreach (var stat in monster.Stats)
        {
            Stats[stat.Key] = new Stat(stat.Value);
        }

        Type = _saveData.Key;
        Exp = _saveData.Exp;
        Item = InventoryManager.Instance.Inventory.FirstOrDefault(x => x.UniqueNumber == _saveData.EquipItemKey);
        UniqueNumber = _saveData.UniqueNumber;

        Stats[StatType.CurHp] = new Stat(_saveData.CurrentHP);
        Stats[StatType.CurMp] = new Stat(_saveData.CurrentMP);
    }

    public Monster(MonsterType _type, string _name, Dictionary<StatType, Stat> _stat, List<int> _skill,
        MonsterType? evolveType = MonsterType.None)
    {
        Type = _type;
        Name = _name;
        Stats = _stat;
        Skills = _skill;
        Exp = 0;
        EvolveType = evolveType;
    }

    public Monster(MonsterType _type)
    {
        var monster = MonsterTable.GetMonsterByType(_type).Copy();
        Type = monster.Type;
        Name = monster.Name;

        Stats = new Dictionary<StatType, Stat>();
        foreach (var stat in monster.Stats)
        {
            Stats[stat.Key] = new Stat(stat.Value);
        }

        Skills = new List<int>(monster.Skills);
        Item = monster.Item?.Copy();
        Lv = monster.Lv;
        Exp = monster.Exp;
    }

    public MonsterType Type { get; private set; }
    public string Name { get; private set; }
    public Dictionary<StatType, Stat> Stats { get; private set; }
    public int UniqueNumber { get; private set; }
    public List<int> Skills { get; private set; }
    public Item Item { get; private set; }
    public int Lv { get; private set; }
    public MonsterType? EvolveType { get; private set; } = MonsterType.None;
    private int exp;

    public int Exp
    {
        get => exp;
        set => AddExp(value - exp);
    }


    public Monster Copy()
    {
        //Dictionary 복제 -> Dictionary 복제하는데 Stat이 클래스라서 Stat을 복제하면서 해야함.
        Dictionary<StatType, Stat> newStat = new Dictionary<StatType, Stat>();


        foreach (var copyDict in Stats) //foreach문을 이용하여 딕셔너리의 모든 키-값에 접근할 수 있음. (foreach var '지역변수' in '딕셔너리 이름')
            // Stats : Dictionary<StatType, Stat>의 Stats이므로 copyDict도 <StatType, stat> 타입임
            // copyDict는 Stats 딕셔너리의 각 항목을 참조하는 변수 + foreach는 딕셔너리에서 항목을 자동으로 순회할 수 있기 때문에 Stats만 써도 딕셔너리의 Key와 value값을 가져올 수 있다.
        {
            Stat
                original = copyDict
                    .Value; //CopyDict는 foreach문으로 딕셔너리를 순회하는 변수이므로, copyDict.value(StatType의 정보)를 original에 저장하는 결과가 된다.
            Stat copyStat = new Stat(original); //Stat 객체 생성

            // copyStat.Type = original.Type; //복제!
            // copyStat.BaseValue = original.BaseValue;
            // copyStat.BuffValue = original.BuffValue;
            // copyStat.EquipmentValue = original.EquipmentValue;
            newStat[copyDict.Key] =
                copyStat; //newStat : Dictionary<StatType, Stat> 타입의 딕셔너리의 copyDict.Key라는 키에 copyStat 값을 주겠다.
            //즉 newStat의 key = copyDict.Key, newStat의 value = copyStat
        }

        //List 복제
        List<int> newSkill = new List<int>(Skills);

        Monster monster = new Monster(Type, Name, newStat, newSkill, EvolveType);

        return monster;
    }

    private void AddExp(int amount)
    {
        exp += amount;

        while (true)
        {
            int maxExp = ExpTable.GetExpByLevel(Lv + 1);
            if (maxExp < 0)
                break;

            if (exp >= maxExp)
            {
                LevelUp();
            }
            else
                break;
        }
    }

    private void LevelUp()
    {
        Lv++;

        if (Lv == ((int)Type / 10 + 1) * 10 && EvolveType != MonsterType.None)
        {
            Evolve(EvolveType.Value);
        }

        Stats[StatType.Attack].SetLevelValue(2f * (Lv - 1));
        Stats[StatType.MaxHp].SetLevelValue(10f * (Lv - 1));
        Stats[StatType.MaxMp].SetLevelValue(10f * (Lv - 1));

        Stats[StatType.CurHp].ModifyBaseValue(Stats[StatType.MaxHp].FinalValue, 0, Stats[StatType.MaxHp].FinalValue);
        Stats[StatType.CurMp].ModifyBaseValue(Stats[StatType.MaxMp].FinalValue, 0, Stats[StatType.MaxMp].FinalValue);
    }

    private void Evolve(MonsterType _monsterType)
    {
        var monster = MonsterTable.GetMonsterByType(_monsterType).Copy();

        Name = monster.Name;
        Type = monster.Type;
        Stats = monster.Stats;
        Skills = monster.Skills;
        EvolveType = monster.EvolveType;
    }

    public void SetUniqueNumber()
    {
        Random random = new Random();
        UniqueNumber = random.Next(0, int.MaxValue);
    }

    public void SetUniqueNumber(int _uniqueNumber) => UniqueNumber = _uniqueNumber;

    public void EquipItem(Item _item)
    {
        Item = _item;
    }

    public void UnEquipItem()
    {
        Item = null;
    }
}

public class Skill
{
    public Skill(int _id, string _name, Dictionary<StatType, Stat> _stat, SkillAttackType _skillAttackType,
        int _targetCount, Func<Skill, string> _descriptionFunc)
    {
        Id = _id;
        Name = _name;
        Stats = _stat;
        SkillAttackType = _skillAttackType;
        TargetCount = _targetCount;
        descriptionFunc = _descriptionFunc;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public Dictionary<StatType, Stat> Stats { get; private set; }
    public SkillAttackType SkillAttackType { get; private set; }
    public int TargetCount { get; private set; }
    private Func<Skill, string> descriptionFunc;
    public string Description => descriptionFunc?.Invoke(this);
}

public class SaveData
{
    public int ClearStage;
    public int Gold;

    // Item을 전부 변환 시킬 필요가 없음. int값만 가지고 와서 Table에서 가져오는 방식을 사용
    public string Name;
    public SaveMonsterData EquipMonster;
    public List<SaveItem> Inventory = new List<SaveItem>();
    public List<SaveMonsterData> MonsterBox = new List<SaveMonsterData>();

    //QuestData
    public List<SaveQeust> Quests = new List<SaveQeust>();
    public List<int> ClearQuests = new List<int>();

    //CollectionData
    public Dictionary<MonsterType, CollectionData> CollectionData = new Dictionary<MonsterType, CollectionData>();

    public SaveData(SaveData _data)
    {
        Name = _data.Name;
        EquipMonster = _data.EquipMonster;
        ClearStage = _data.ClearStage;
        Inventory = _data.Inventory;
        Gold = _data.Gold;
        Quests = _data.Quests;
        ClearQuests = _data.ClearQuests;
        CollectionData = _data.CollectionData;
        MonsterBox = _data.MonsterBox;
    }

    public SaveData()
    {
    }
}

public class SaveMonsterData
{
    public MonsterType Key;
    public int UniqueNumber;
    public int EquipItemKey;
    public int Exp;
    public SaveStat CurrentHP;
    public SaveStat CurrentMP;

    public SaveMonsterData(Monster _monster)
    {
        Key = _monster.Type;
        EquipItemKey = _monster.Item?.UniqueNumber ?? 0;
        UniqueNumber = _monster.UniqueNumber;
        Exp = _monster.Exp;
        CurrentHP = new SaveStat(_monster.Stats[StatType.CurHp]);
        CurrentMP = new SaveStat(_monster.Stats[StatType.CurHp]);
    }

    public SaveMonsterData()
    {
    }
}

public class SaveItem
{
    public int ItemKey;
    public int UniqueNumber;

    public SaveItem(Item _item)
    {
        ItemKey = _item.Key;
        UniqueNumber = _item.UniqueNumber;
    }

    public SaveItem()
    {
    }
}

public class SaveStat
{
    public StatType Type;
    public float BaseValue;
    public float BuffValue;
    public float LevelValue;
    public float EquipmentValue;

    public SaveStat(Stat _stat)
    {
        Type = _stat.Type;
        BaseValue = _stat.BaseValue;
        BuffValue = _stat.BuffValue;
        LevelValue = _stat.LevelValue;
        EquipmentValue = _stat.EquipmentValue;
    }

    public SaveStat()
    {
    }
}