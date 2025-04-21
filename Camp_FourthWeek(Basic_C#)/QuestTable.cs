using System;
using System.Collections.Generic;

namespace Camp_FourthWeek_Basic_C__;

public static class QuestTable
{
    public static readonly Dictionary<int, Quest> QuestDic = new Dictionary<int, Quest>()
    {
        {
            1, new Quest(1, "장비 구매", QuestType.Main,
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
            2, new Quest(2, "장비 장착", QuestType.Main,
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

    public static Quest GetQuestInfo(int _key)
    {
        if (QuestDic.TryGetValue(_key, out Quest quest))
        {
            return quest;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Quest not found");
        Console.ResetColor();
        return null;
    }
}