namespace Camp_FourthWeek_Basic_C__;

public class CollectionManager
{
    private static readonly CollectionManager instance = new CollectionManager();
    public static CollectionManager Instnace => instance;

    public Dictionary<MonsterType, CollectionData> CollectionMonsterDataDic { get; private set; } =
        new Dictionary<MonsterType, CollectionData>();


    public void OnDiscovered(MonsterType _key)
    {
        if (!CollectionMonsterDataDic.ContainsKey(_key))
        {
            CollectionMonsterDataDic.Add(_key, new CollectionData() { Key = _key });
        }
    }

    public void OnCaptured(MonsterType _key)
    {
        if (!CollectionMonsterDataDic.TryGetValue(_key, out var monster))
        {
            OnDiscovered(_key);
            monster = CollectionMonsterDataDic[_key]; // 강제 참조로 재확인
        }

        monster.IsCaptured = true;
    }

    public bool IsDiscovered(MonsterType _key)
    {
        return CollectionMonsterDataDic.ContainsKey(_key);
    }

    public bool IsCaptured(MonsterType _key)
    {
        return CollectionMonsterDataDic.TryGetValue(_key, out var monster) && monster.IsCaptured;
    }

    public void GetLoadData(Dictionary<MonsterType, CollectionData> loadData)
    {
        CollectionMonsterDataDic = loadData.ToDictionary();
    }
}

public class CollectionData
{
    public MonsterType Key;
    public bool IsCaptured { get; set; }
}