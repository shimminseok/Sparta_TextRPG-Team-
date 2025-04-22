using System;
using System.Collections.Generic;

namespace Camp_FourthWeek_Basic_C__;

public static class QuestTable
{
    public static readonly Dictionary<int, Quest> QuestDic = new Dictionary<int, Quest>()
    {
        {
            1, new Quest(1, "장비 구매 퀘스트", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 1, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 4, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 7, 0, 1)
                },
                new List<int>() { 10, 13 },
                1500)
        },
        {
            2, new Quest(2, "장비 장착 퀘스트", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>() { 1 },
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, "{0} 장착하기", 1, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 장착하기", 3, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 장착하기", 5, 0, 1)
                },
                new List<int>() { 7, 9 },
                1500)
        },
        {
            3, new Quest(3, "장비 장착 퀘스트", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>() { 2 },
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Monster, "{0} 장착하기", 1, 0, 5),
                    new QuestCondition(QuestTargetType.Monster, "{0} 장착하기", 2, 0, 3),
                },
                new List<int>() { 7, 9 },
                1500)
        },

        {
            10, new Quest(10, "장비 구매 퀘스트 중복 테스트", QuestType.Main,
                "장비 구매 퀘스트 입니다.",
                "장비 구매를 완료했구나 축하",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 1, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 4, 0, 1),
                    new QuestCondition(QuestTargetType.Item, "{0} 구매하기", 7, 0, 1)
                },
                new List<int>() { 10, 13 },
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