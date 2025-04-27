using System;
using System.Collections.Generic;

namespace Camp_FourthWeek_Basic_C__;

public static class QuestTable
{
    public static readonly Dictionary<int, Quest> QuestDic = new Dictionary<int, Quest>()
    {
        {
            1, new Quest(1, "도구 구매 퀘스트", QuestType.Main,
                "도구 구매 퀘스트 입니다.",
                "도구 구매를 완료했구나 축하",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, QuestConditionType.Buy, 1, 0, 1),
                    new QuestCondition(QuestTargetType.Item, QuestConditionType.Buy, 4, 0, 1),
                },
                new List<int>() { 10, 13 }, new List<MonsterType>(),
                1500)
        },
        {
            2, new Quest(2, "도구 장착 퀘스트", QuestType.Main,
                "도구 장착 퀘스트 입니다.",
                "도구 장착을 완료했구나 축하",
                new List<int>() { 1 },
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Item, QuestConditionType.Equip, 1, 0, 1),
                },
                new List<int>() { 7, 9 }, new List<MonsterType>(),
                1500)
        },
        {
            3, new Quest(3, "구구가 좋아!", QuestType.Main,
                " 미안한데... 내가 구구가 꼭 가지고 싶거든?\n근데 내가 능력이 안되서 잡을 수가 없어!!\n구구 한마리를 잡아와 줄수 있겠어?",
                "와 너는 진짜 뛰어난 포켓몬 트레이너구나?!\n정말 고마워!!",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Monster, QuestConditionType.Catch, (int)MonsterType.Pidgey, 0,
                        1),
                },
                new List<int>() { 7, 9 }, new List<MonsterType>() { MonsterType.Pikachu },
                1500)
        },
        {
            4, new Quest(4, "구구가 싫어!", QuestType.Main,
                " 구구가 실허 다 죽었으면 좋겠어 우다다다다",
                "와 너는 진짜 잔인한 놈이구나?!\n정말 고마워!!",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Monster, QuestConditionType.Kill, (int)MonsterType.Pidgey, 0,
                        5),
                },
                new List<int>(), new List<MonsterType>() { MonsterType.Dratini },
                1500)
        },
        {
            5, new Quest(5, "구구랑 피카츄가 좋아!", QuestType.Main,
                " 미안한데... 내가 구구랑 피카츄가 꼭 가지고 싶거든?\n근데 내가 능력이 안되서 잡을 수가 없어!!\n구구와 피카츄 한마리를 잡아와 줄수 있겠어?",
                "와 너는 진짜 뛰어난 포켓몬 트레이너구나?!\n정말 고마워!!",
                new List<int>(),
                new List<QuestCondition>()
                {
                    new QuestCondition(QuestTargetType.Monster, QuestConditionType.Catch, (int)MonsterType.Pidgey, 0,
                        1),
                    new QuestCondition(QuestTargetType.Monster, QuestConditionType.Catch, (int)MonsterType.Pikachu, 0,
                        1),
                },
                new List<int>(), new List<MonsterType>() { MonsterType.Raichu },
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