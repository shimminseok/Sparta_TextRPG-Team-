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

    public PlayerInfo PlayerInfo { get; private set; } = new(MonsterType.Pikachu, "Test");

    public void Init(MonsterType _monster, string _name)
    {
        PlayerInfo = new PlayerInfo(_monster, _name);
        if (loadData == null)
            return;
        LoadInventoryAndEquipment();
        LoadDungeonClearProgress();
        LoadQuestProgress();
        LoadPlayerGold();
    }

    public void SaveGame()
    {
        var inventory = InventoryManager.Instance.Inventory.Select(x => x.Key).ToList();
        var equipmentItem = EquipmentManager.EquipmentItems.Values.Select(x => x.Key).ToList();

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
            Monster = PlayerInfo.Monster.Type,
            Gold = PlayerInfo.Gold,
            Inventory = GetInventoryItemKeys(),
            EquipmentItem = GetEquippedItemKeys(),
            DungeonClearCount = LevelManager.ClearDungeonCount,
            Quests = GetCurrentQuestData(),
            ClearQuests = QuestManager.Instance.ClearQuestList.ToList()
        };
        var sJson = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        string encrypted = AESUtil.Encrypt(sJson);

        Console.WriteLine(encrypted.Length);

        File.WriteAllText(path, encrypted);
    }

    public void LoadGame()
    {
        if (!File.Exists(path))
        {
            var start = new CreateCharacterAction();
            start.Execute();
            return;
        }

        try
        {
            var encrypted = File.ReadAllText(path);
            string decrypted = AESUtil.Decrypt(encrypted);
            var data = JsonConvert.DeserializeObject<SaveData>(decrypted);
            if (data != null)
            {
                loadData = new SaveData(data);
                Init(loadData.Monster, loadData.Name);
                var mainAction = new MainMenuAction();
                mainAction.Execute();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("⚠ 저장 파일이 손상되었거나 복호화에 실패했습니다.");
            File.Delete(path); // 손상된 파일 삭제 (선택)
            new CreateCharacterAction().Execute();
        }
    }

    #region [LoadGame]

    private void LoadInventoryAndEquipment()
    {
        foreach (var itemId in loadData.Inventory)
        {
            var item = ItemTable.GetItemById(itemId);
            InventoryManager.Instance.AddItem(item);

            if (loadData.EquipmentItem.Contains(item.Key))
            {
                EquipmentManager.EquipmentItem(item);
            }
        }
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

    #endregion

    #region [SaveGame]

    private List<int> GetInventoryItemKeys()
    {
        return InventoryManager.Instance.Inventory
            .Select(item => item.Key)
            .ToList();
    }

    private List<int> GetEquippedItemKeys()
    {
        return EquipmentManager.EquipmentItems.Values
            .Select(item => item.Key)
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