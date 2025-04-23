using System.Collections.Generic;
using System.Linq.Expressions;

namespace Camp_FourthWeek_Basic_C__;

public class QuestManager
{
    private static readonly QuestManager instance = new QuestManager();
    public static QuestManager Instance => instance;

    //퀘스트를 관리하는 Dic(다른 퀘스트에 같은 TargetID가 있을 수 있기 때문
    public Dictionary<QuestTargetType, List<QuestCondition>> QuestConditionsMap { get; private set; } =
        new Dictionary<QuestTargetType, List<QuestCondition>>();

    //현재 내가 수락한 실질적인 Quest List
    public List<Quest> CurrentAcceptQuestList { get; private set; } = new List<Quest>();
    public List<int> ClearQuestList { get; set; } = new List<int>();


    public void AcceptQuest(Quest _quest)
    {
        if (CurrentAcceptQuestList.Exists(quest => quest.Key == _quest.Key))
            return;


        Quest quest = _quest.DeepCopy();
        for (int i = 0; i < quest.Conditions.Count; i++)
        {
            var condition = quest.Conditions[i];
            if (!QuestConditionsMap.ContainsKey(condition.TargetType))
            {
                QuestConditionsMap[condition.TargetType] = new List<QuestCondition>();
            }

            QuestConditionsMap[condition.TargetType].Add(condition);
        }

        quest.State = QuestState.InProgress;
        CurrentAcceptQuestList.Add(quest);
    }

    public void AbandonQuest(Quest _quest)
    {
        RemoveQuest(_quest);
    }

    public void QuestClear(Quest _quest)
    {
        for (int i = 0; i < _quest.RewardItemsList.Count; i++)
        {
            var item = ItemTable.GetItemById(_quest.RewardItemsList[i]);
            InventoryManager.Instance.AddItem(item);
        }

        if (_quest.QuestRewardGold > 0)
        {
            GameManager.Instance.PlayerInfo.Gold += _quest.QuestRewardGold;
        }

        _quest.State = QuestState.Completed;
        RemoveQuest(_quest);
        ClearQuestList.Add(_quest.Key);
    }

    void RemoveQuest(Quest _quest)
    {
        CurrentAcceptQuestList.Remove(_quest);
        foreach (var condition in _quest.Conditions)
        {
            if (QuestConditionsMap.TryGetValue(condition.TargetType, out List<QuestCondition> conditions))
            {
                conditions.Remove(condition);
            }
        }
    }

    public void UpdateCurrentCount(QuestTargetType _type, int _targetID)
    {
        if (QuestConditionsMap.TryGetValue(_type, out List<QuestCondition> conditions))
        {
            foreach (var condition in conditions)
            {
                if (condition.TargetID == _targetID)
                {
                    condition.CurrentCount++;
                }
            }
        }
    }

    public void LoadQuestData(SaveQeust _saveQuest)
    {
        Quest loadQuest = QuestTable.GetQuestInfo(_saveQuest.Key);
        if (loadQuest == null)
            return;

        for (int i = 0; i < loadQuest.Conditions.Count; i++)
        {
            var saveCondition = _saveQuest.QuestConditions.Find(condition => condition.Index == i);
            loadQuest.Conditions[i].CurrentCount = saveCondition.CurrentCount;
        }

        AcceptQuest(loadQuest);
    }
}

public class SaveQeust
{
    public int Key;
    public List<SaveCondition> QuestConditions;

    public SaveQeust(int _key, List<SaveCondition> _conditions)
    {
        Key = _key;
        QuestConditions = _conditions;
    }

    public SaveQeust()
    {
    }
}

public class SaveCondition
{
    public int Index;
    public int CurrentCount;

    public SaveCondition(int _index, int _currentCount)
    {
        Index = _index;
        CurrentCount = _currentCount;
    }

    public SaveCondition()
    {
    }
}