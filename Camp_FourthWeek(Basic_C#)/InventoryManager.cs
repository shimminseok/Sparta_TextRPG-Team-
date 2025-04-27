using System.Dynamic;

namespace Camp_FourthWeek_Basic_C__;

public class InventoryManager()
{
    private static InventoryManager instance;

    public static InventoryManager Instance
    {
        get
        {
            if (instance == null) instance = new InventoryManager();
            return instance;
        }
    }

    public const float HEAL_AMOUNT = 30f;

    public List<Item> Inventory { get; set; } = new();
    public List<Monster> MonsterBox { get; set; } = new();

    public int FruitCount { get; private set; } = 3;

    public void AddItem(Item _item)
    {
        Random random = new Random();
        _item.SetUniqueNumber();
        Inventory.Add(_item);
    }

    public void RemoveItem(Item _item)
    {
        Monster monster = EquipmentManager.GetEquippedMonster(_item);
        if (monster != null)
            EquipmentManager.UnequipItem(monster);

        Inventory.Remove(_item);
    }

    public void AddMonsterToBox(Monster _monster)
    {
        _monster.SetUniqueNumber();
        MonsterBox.Add(_monster);
    }

    public void AddFruit()
    {
        FruitCount++;
    }

    public void UseFruit()
    {
        if (FruitCount > 0)
        {
            FruitCount--;
            Monster monster = GameManager.Instance.PlayerInfo.Monster;
            Stat hpStat = monster.Stats[StatType.CurHp];
            float maxHp = monster.Stats[StatType.MaxHp].FinalValue;
            hpStat.ModifyBaseValue(HEAL_AMOUNT, 0, maxHp);
        }
    }
}