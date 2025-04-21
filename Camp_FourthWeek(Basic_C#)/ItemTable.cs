namespace Camp_FourthWeek_Basic_C__;

public static class ItemTable
{
    public static readonly Dictionary<int, Item> ItemDic = new Dictionary<int, Item>()
    {
        {
            1,
            new Item(1, "수련자 갑옷", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 10) },
                "수련에 도움을 주는 갑옷입니다.", 100)
        },
        {
            2,
            new Item(2, "무쇠 갑옷", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 15) },
                "무쇠로 만들어져 튼튼한 갑옷입니다.", 500)
        },
        {
            3,
            new Item(3, "스파르타의 갑옷", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 20) },
                "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 1000)
        },

        {
            4,
            new Item(4, "낡은 검", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 10) },
                "쉽게 볼 수 있는 낡은 검 입니다.", 100)
        },
        {
            5,
            new Item(5, "청동 도끼", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 15) },
                "어디선가 사용됐던거 같은 도끼입니다.", 500)
        },
        {
            6,
            new Item(6, "스파르타의 창", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 20) },
                "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1000)
        },

        {
            7,
            new Item(7, "수련자 투구", ItemType.Helmet,
                new List<Stat> { new Stat(StatType.Defense, 1), new Stat(StatType.MaxHp, 10) }, "수련에 도움을 주는 갑옷입니다.",
                100)
        },
        {
            8,
            new Item(8, "무쇠 투구", ItemType.Helmet,
                new List<Stat> { new Stat(StatType.Defense, 3), new Stat(StatType.MaxHp, 50) },
                "무쇠로 만들어져 튼튼한 투구입니다.", 500)
        },
        {
            9,
            new Item(9, "스파르타 투구", ItemType.Helmet,
                new List<Stat> { new Stat(StatType.Defense, 5), new Stat(StatType.MaxHp, 100) },
                "스파르타의 전사들이 사용했다는 전설의 투구입니다.", 1000)
        },

        {
            10,
            new Item(10, "수련자 장갑", ItemType.Groves,
                new List<Stat> { new Stat(StatType.Defense, 1), new Stat(StatType.CriticalChance, 5) },
                "수련에 도움을 주는 장갑입니다.", 100)
        },
        {
            11,
            new Item(11, "무쇠 장갑", ItemType.Groves,
                new List<Stat> { new Stat(StatType.Defense, 3), new Stat(StatType.CriticalChance, 10) },
                "무쇠로 만들어져 튼튼한 장갑입니다.", 500)
        },
        {
            12,
            new Item(12, "스파르타 장갑", ItemType.Groves,
                new List<Stat> { new Stat(StatType.Defense, 5), new Stat(StatType.CriticalChance, 20) },
                "스파르타의 전사들이 사용했다는 전설의 장갑입니다.", 1000)
        },

        {
            13,
            new Item(13, "수련자 장화", ItemType.Shoes,
                new List<Stat> { new Stat(StatType.Defense, 1), new Stat(StatType.MaxMp, 10) }, "수련에 도움을 주는 장화입니다.",
                100)
        },
        {
            14,
            new Item(14, "무쇠 신발", ItemType.Shoes,
                new List<Stat> { new Stat(StatType.Defense, 3), new Stat(StatType.MaxMp, 20) },
                "무쇠로 만들어져 튼튼한 장화입니다.", 500)
        },
        {
            15,
            new Item(15, "스파르타 신발", ItemType.Shoes,
                new List<Stat> { new Stat(StatType.Defense, 5), new Stat(StatType.MaxMp, 30) },
                "스파르타의 전사들이 사용했다는 전설의 장화입니다.", 1000)
        },
    };

    public static Item? GetItemById(int _id)
    {
        if (ItemDic.TryGetValue(_id, out var item))
        {
            return item;
        }

        Console.WriteLine("원하는 아이템이 테이블에 등록되지 않았음");
        return null;
    }
}