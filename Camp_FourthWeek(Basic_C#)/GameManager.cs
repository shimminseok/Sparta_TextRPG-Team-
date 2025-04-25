using Newtonsoft.Json;

namespace Camp_FourthWeek_Basic_C__;

public class GameManager
{
    private const string path = "saveData.json";
    private static GameManager instance;

    private SaveData loadData;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

    public PlayerInfo? PlayerInfo { get; private set; }

    public void Init(Monster _monster, string _name)
    {
        PlayerInfo = new PlayerInfo(_monster, _name);
        if (loadData == null)
            return;
        LoadInventoryAndEquipment();
        LoadMonterBox();
        LoadDungeonClearProgress();
        LoadQuestProgress();
        LoadCollectionData();
        LoadPlayerGold();
        LoadCurrentMonsterData();
        EquipmentManager.EquipmentItem(PlayerInfo.Monster.Item);
    }

    public void SaveGame()
    {
        var inventory = InventoryManager.Instance.Inventory.Select(x => x.Key).ToList();
        // var equipmentItem = EquipmentManager.EquipmentItems.Values.Select(x => x.Key).ToList();

        /*
         * SaveData Class를 Json으로 저장합니다.
         * Inventory에는 Item Class를 리스트로 관리하지만 SaveData는 List<int>로 아이템을 저장합니다.
         * 이유는 Item Class를 전부 직렬화 시킨다면 아이템이 더욱 많아질 경우 직렬화가 늦어지고 그에 따른 역직렬화도 늦어져서 로딩이 길어집니다.
         * 따라서 SaveData의 Inventory는 Item의 Key값만 가지고 있고 Load할때, 해당 키값을 가지고 다시 Inventory에 넣어줍니다.
         * EquipmentItem도 동일한 구조로 Item의 Key만 가지고 있습니다.
         */

        var saveData = new SaveData
        {
            Name = PlayerInfo.Name,
            EquipMonster = GetCurrentMonsterData(),
            Gold = PlayerInfo.Gold,
            Inventory = GetInventoryItemKeys(),
            MonsterBox = GetCurrentMonsterBoxData(),
            DungeonClearCount = LevelManager.ClearDungeonCount,
            Quests = GetCurrentQuestData(),
            ClearQuests = QuestManager.Instance.ClearQuestList.ToList(),
            CollectionData = GetCurrentCollectionData()
        };
        var sJson = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        // string encrypted = AESUtil.Encrypt(sJson);
        string encrypted = sJson;

        File.WriteAllText(path, encrypted);
    }

    public bool HasLoadData()
    {
        return File.Exists(path);
    }

    public IAction LoadGame()
    {
        if (!HasLoadData())
        {
            var start = new CreateNickNameAction();
            return start;
        }

        try
        {
            var encrypted = File.ReadAllText(path);
            string decrypted = encrypted;
            // string decrypted = AESUtil.Decrypt(encrypted);
            var data = JsonConvert.DeserializeObject<SaveData>(decrypted);
            if (data != null)
            {
                loadData = new SaveData(data);
                Init(new Monster(loadData.EquipMonster.Key), loadData.Name);
                var mainAction = new MainMenuAction();
                return mainAction;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("⚠ 저장 파일이 손상되었거나 복호화에 실패했습니다.");
            File.Delete(path);
            return new CreateNickNameAction();
        }

        return null;
    }

    #region [LoadGame]

    private void LoadInventoryAndEquipment()
    {
        List<Item> inventory = new List<Item>();
        foreach (var saveItem in loadData.Inventory)
        {
            var item = ItemTable.GetItemById(saveItem.ItemKey).Copy();
            item.SetUniqueNumber(saveItem.UniqueNumber);
            inventory.Add(item);
        }

        InventoryManager.Instance.Inventory = inventory.ToList();
    }

    void LoadMonterBox()
    {
        List<Monster> monsters = new List<Monster>();
        foreach (var saveMonster in loadData.MonsterBox)
        {
            var monster = MonsterTable.GetMonsterByType(saveMonster.Key).Copy();
            monster.SetUniqueNumber(saveMonster.UniqueNumber);
            monsters.Add(monster);
        }

        InventoryManager.Instance.MonsterBox = monsters;
    }

    private void LoadDungeonClearProgress()
    {
        for (int i = 0; i < loadData.DungeonClearCount; i++)
        {
            LevelManager.AddClearCount();
        }
    }

    private void LoadQuestProgress()
    {
        foreach (var questData in loadData.Quests)
        {
            QuestManager.Instance.LoadQuestData(questData);
        }

        QuestManager.Instance.ClearQuestList = loadData.ClearQuests;
    }

    private void LoadPlayerGold()
    {
        PlayerInfo.Gold = loadData.Gold;
    }

    private void LoadCollectionData()
    {
        CollectionManager.Instnace.GetLoadData(loadData.CollectionData);
    }

    private void LoadCurrentMonsterData()
    {
        var loadMonster = loadData.EquipMonster;
        PlayerInfo.Monster = new Monster(loadMonster);
    }

    #endregion

    #region [SaveGame]

    private List<SaveItem> GetInventoryItemKeys()
    {
        var saveItemList = new List<SaveItem>();
        for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            var saveItem = new SaveItem(InventoryManager.Instance.Inventory[i]);
            saveItemList.Add(saveItem);
        }

        return saveItemList.ToList();
    }

    private SaveMonsterData GetCurrentMonsterData()
    {
        return new SaveMonsterData(PlayerInfo.Monster);
    }

    private List<SaveMonsterData> GetCurrentMonsterBoxData()
    {
        return InventoryManager.Instance.MonsterBox
            .Select(monster => new SaveMonsterData(monster))
            .ToList();
    }

    private List<SaveQeust> GetCurrentQuestData()
    {
        return QuestManager.Instance.CurrentAcceptQuestList
            .Select(quest =>
                new SaveQeust
                (
                    quest.Key,
                    quest.Conditions
                        .Select((condition, index) => new SaveCondition(index, condition.CurrentCount))
                        .ToList()
                ))
            .ToList();
    }

    Dictionary<MonsterType, CollectionData> GetCurrentCollectionData()
    {
        return CollectionManager.Instnace.CollectionMonsterDataDic.ToDictionary();
    }

    #endregion


    public void DeleteGameData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("데이터가 삭제되었습니다. \n 잠시후 게임을 재시작 됩니다.");
            Thread.Sleep(1000);
        }
        else
        {
            Console.WriteLine("삭제할 데이터가 존재하지 않습니다.");
        }
    }
}