using System.Collections.Generic;
using System.Net.Mime;

namespace Camp_FourthWeek_Basic_C__;

//퀘스트 타입
public enum QuestType
{
    Main,
    Side,
    Repeat,
}

public enum QuestTargetType
{
    Monster,
    Item
}

public enum QuestConditionType
{
    Equip, // 장착하기
    Buy, // 구매하기
    Catch, // 포획
    Use, // 사용하기
    Kill,
}

//퀘스트 진행 사항
public enum QuestState
{
    NotStarted,
    InProgress,
    Completable,
    Completed,
}

public class Quest
{
    public int Key { get; private set; }
    public string QuestName { get; private set; }
    public string QuestDescription { get; private set; } //퀘스트 수락 대사
    public string QuestCompleteDescription { get; private set; } //퀘스트 완료 대사

    public QuestType QuestType { get; private set; }
    public List<int> PrerequisiteQuest; //선행 퀘스트
    public List<QuestCondition> Conditions; //퀘스트 조건 목록
    public QuestState State { get; set; } = QuestState.NotStarted;


    public List<int> RewardItemsList { get; private set; }
    public int QuestRewardGold { get; private set; }

    /// <summary>
    /// 퀘스트 정보 Class
    /// </summary>
    /// <param name="_questName">퀘스트 이름</param>
    /// <param name="_questType">퀘스트 타입</param>
    /// <param name="_questDescription">퀘스트 수락 Desc</param>
    /// <param name="_prerequisQuest"></param>
    /// <param name="_targetCount"></param>
    /// <param name="_conditions"></param>
    /// <param name="_rewardItemsList"></param>
    /// <param name="_questRewardGold"></param>
    public Quest(int _key, string _questName, QuestType _questType, string _questDescription,
        string _questCompleteDescription
        , List<int> _prerequisQuest, List<QuestCondition> _conditions,
        List<int> _rewardItemsList, int _questRewardGold)
    {
        Key = _key;
        QuestName = _questName;
        QuestType = _questType;
        QuestDescription = _questDescription;
        QuestCompleteDescription = _questCompleteDescription;
        PrerequisiteQuest = _prerequisQuest;
        Conditions = _conditions;
        RewardItemsList = _rewardItemsList;
        QuestRewardGold = _questRewardGold;
    }

    public bool IsCompleted()
    {
        return Conditions.TrueForAll(x => x.IsCompleted);
    }

    public Quest()
    {
    }

    public Quest DeepCopy()
    {
        return new Quest
        (Key, QuestName, QuestType, QuestDescription, QuestCompleteDescription,
            new List<int>(PrerequisiteQuest),
            Conditions.Select(cond =>
                new QuestCondition(cond.TargetType, cond.ConditionType, cond.TargetID, cond.CurrentCount,
                    cond.RequiredCount)
            ).ToList(),
            new List<int>(RewardItemsList), QuestRewardGold);
    }
}

public class QuestCondition
{
    public QuestTargetType TargetType;
    public QuestConditionType ConditionType;
    public int TargetID;
    public int CurrentCount = 0;
    public int RequiredCount;
    public bool IsCompleted => CurrentCount >= RequiredCount;

    public QuestCondition(QuestTargetType _targetType, QuestConditionType _conditionType, int _targetID,
        int _currentCount,
        int _requiredCount)
    {
        TargetType = _targetType;
        ConditionType = _conditionType;
        TargetID = _targetID;
        CurrentCount = _currentCount;
        RequiredCount = _requiredCount;
    }

    public string GetDescription()
    {
        switch (ConditionType)
        {
            case QuestConditionType.Equip:
            {
                string itemName = ItemTable.GetItemById(TargetID).Name;
                return $"{itemName} 장착하기";
            }
            case QuestConditionType.Buy:
            {
                string itemName = ItemTable.GetItemById(TargetID).Name;
                return $"{itemName} 구매하기";
            }
            case QuestConditionType.Catch:
            {
                string monsterName = MonsterTable.GetMonsterByType((MonsterType)TargetID).Name;
                return $"{monsterName} {RequiredCount}마리 포획하기";
            }
            case QuestConditionType.Use:
            {
                string itemName = ItemTable.GetItemById(TargetID).Name;
                return $"{itemName} {RequiredCount}개 사용하기";
            }
            case QuestConditionType.Kill:
            {
                string monsterName = MonsterTable.GetMonsterByType((MonsterType)TargetID).Name;
                return $"{monsterName} {RequiredCount}마리 처치하기";
            }
            default:
                return $"Unknown Quest Condition: {ConditionType}";
        }
    }
}