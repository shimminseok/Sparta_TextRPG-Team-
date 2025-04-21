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

//퀘스트 진행 사항
public enum QuestState
{
    NotStarted,
    InProgress,
    Completed,
    Failed
}

public class Quest
{
    public int Key { get; private set; }
    public string QuestName { get; private set; }
    public string QuestDescription { get; private set; } //퀘스트 수락 대사
    public string QuestCompleteDescription { get; private set; } //퀘스트 완료 대사

    public QuestType QuestType { get; private set; }
    public List<int> PrerequisiteQuest; //선행 퀘스트
    public int CurrentCount; //퀘스트의 현재 카운트
    public List<QuestCondition> Conditions; //퀘스트 조건 목록
    public QuestState State { get; private set; }


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


    public void CheckQuestComplete()
    {
    }

    public Quest()
    {
    }
}

public class QuestCondition
{
    public QuestTargetType TargetType;
    public string QuestConditionTxt;
    public int TargetID;
    public int RequiredCount;

    public QuestCondition(QuestTargetType _targetType, string _questConditionTxt, int _targetID, int _requiredCount)
    {
        TargetType = _targetType;
        QuestConditionTxt = _questConditionTxt;
        TargetID = _targetID;
        RequiredCount = _requiredCount;
    }
}