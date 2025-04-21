namespace Camp_FourthWeek_Basic_C__;

public static class QuestTable
{
    public readonly static Dictionary<int, Quest> DungeonDic = new Dictionary<int, Quest>()
    {
        {
            1, new Quest("장비 구매", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 구매하기"), 1, 1),
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 구매하기"), 3, 1),
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 구매하기"), 5, 1)
                },
                new List<int>() { 7, 9 },
                1500)
        },
        {
            1, new Quest("장비 장착", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 장착하기"), 1, 1),
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 장착하기"), 3, 1),
                    new QuestCondition(QuestTargetType.Item, string.Format("{0} 장착하기"), 5, 1)
                },
                new List<int>() { 7, 9 },
                1500)
        },
    };
}