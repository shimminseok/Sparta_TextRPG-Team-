namespace Camp_FourthWeek_Basic_C__;

public static class ItemTable
{
    public static readonly Dictionary<int, Item> ItemDic = new Dictionary<int, Item>()
    {
        {
            1,
            new Item(1, "목탄", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 5) },
                "다 타버린 나무조각.", 100)
        },
        {
            2,
            new Item(2, "휘어진 스푼", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 15) },
                "힘인지 초능력인지 휘어진 스푼이다.", 500)
        },
        {
            3,
            new Item(3, "끈기갈고리손톱", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 20) },
                "어떤 몬스터의 손톱이다. 더러운 먼지가 묻어있다.", 1000)
        },
        {
            4,
            new Item(4, "예리한 부리", ItemType.Weapon,
                new List<Stat> { new Stat(StatType.Attack, 20), new Stat(StatType.Defense, 10) },
                "어떤 몬스터의 부리이다. 잘 손질되어있다.", 1300)
        },
        {
            5,
            new Item(5, "경험 부적", ItemType.Weapon,
                new List<Stat> { new Stat(StatType.Attack, 10), new Stat(StatType.Defense, 10) },
                "전투의 달인이 지니고 있던 부적이다.", 1500)
        },
        {
            6,
            new Item(6, "달인의 띠", ItemType.Weapon,
                new List<Stat> { new Stat(StatType.Attack, 20), new Stat(StatType.Defense, 20) },
                "전투의 달인이 지니고 있던 허리띠이다.", 2000)
        },
        {
            7,
            new Item(7, "용의 이빨", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 40) },
                "용의 사체에서 얻은 이빨이다. 매우 단단하다.", 3300)
        },
        {
            8,
            new Item(8, "파괴의 유전자", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 100), new Stat(StatType.MaxHp, -40) },
                "파괴충동을 가진 유전자이다. 심으면 어떻게 될지..", 4200)
        },
        {
            9,
            new Item(9, "먹다남은 음식", ItemType.Weapon, new List<Stat> { new Stat(StatType.Attack, 80) },
                "누군가 먹다남은 음식이다 음식에 극독이 묻어있다.", 5500)
        },
        {
            10,
            new Item(10, "실프 스카프", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 5), new Stat(StatType.CriticalChance, 2) },
                "순백의 스카프. 방어력은 거의 없는거같다.", 100)
        },
        {
            11,
            new Item(11, "방호 패드", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 10) },
                "공격하는 상대가 아파할 정도의 방호복이다.", 500)
        },
        {         12,
            new Item(12, "특성 가드", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 15) },
                    "빛이 다 바래진 귀여운 방패이다.", 1000)
        },
        {
            13,
            new Item(13, "녹지않는 얼음", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 20) },
                    "어느 추운지방의 만년빙이다.", 1500)
        },
        {
            14,
            new Item(14, "저주 부적", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 40), new Stat(StatType.MaxHp, -15) },
                "누군가의 염원이 담긴 불길한 부적이다.", 2500)
        },
        {
            15,
            new Item(15, "검은 안경", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 20), new Stat(StatType.MaxHp, 100) },
                "앞이 컴컴한 안경이다. 신비한 기운이 감싸고있다.", 2700)
        },
        {
            16,
            new Item(16, "요정의 깃털", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 25), new Stat(StatType.CriticalChance, 20) },
                "고대 요정의 깃털이다.", 3000)
        },
        {
           17,
            new Item(17, "구애 머리띠", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 50) },
                "어느 모태 솔로의 머리띠. 그 무엇보다도 단단하다...", 4000)
        },  
        {
            18,
            new Item(18, "생명의 구슬", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 30), new Stat(StatType.MaxHp, 150)  },
                "생명력이 가득 차있는 구슬이다.", 3500)
        },      
        {
            19,
            new Item(19, "은밀 망토", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, -20), new Stat(StatType.CriticalChance, 60) },
                "겉으로 보기에는 변함이 없을 것이다", 5000)
        }, 
        {
            20,
            new Item(20, "변함 없는돌", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 50), new Stat(StatType.MaxHp, 100) },
                "겉으로 보기에는 변함이없다. 내용만 다를뿐", 6500)
        },
        {
            21,
            new Item(21, "학습 장치", ItemType.Armor, new List<Stat> { new Stat(StatType.Defense, 80), new Stat(StatType.Attack, 120)},
                "낡아버린 기계장치이다. 머리에 쓰면 강해 질거같다.", 10000)
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