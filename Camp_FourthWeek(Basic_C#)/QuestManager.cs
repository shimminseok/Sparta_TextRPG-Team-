using System.Collections.Generic;
using System.Linq.Expressions;

namespace Camp_FourthWeek_Basic_C__;

public class QuestManager
{
    private static readonly QuestManager instance = new QuestManager();

    //퀘스트를 관리하는 Dic(다른 퀘스트에 같은 TargetID가 있을 수 있기 때문
    public Dictionary<QuestTargetType, List<QuestCondition>> Quests { get; private set; } =
        new Dictionary<QuestTargetType, List<QuestCondition>>();

    //현재 내가 수락한 실질적인 Quest List
    public List<Quest> CurrentAcceptQuestList { get; private set; } = new List<Quest>();
    public List<int> ClearQuestList { get; private set; } = new List<int>();
    public static QuestManager Instance => instance;


    public void AcceptQuest(Quest _quest)
    {
        if (CurrentAcceptQuestList.Exists(quest => quest.Key == _quest.Key))
            return;


        Quest quest = _quest.DeepCopy();
        for (int i = 0; i < quest.Conditions.Count; i++)
        {
            var condition = quest.Conditions[i];
            if (!Quests.ContainsKey(condition.TargetType))
            {
                Quests[condition.TargetType] = new List<QuestCondition>();
            }

            Quests[condition.TargetType].Add(condition);
        }

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

        RemoveQuest(_quest);
        ClearQuestList.Add(_quest.Key);
    }

    void RemoveQuest(Quest _quest)
    {
        CurrentAcceptQuestList.Remove(_quest);
        foreach (var condition in _quest.Conditions)
        {
            if (Quests.TryGetValue(condition.TargetType, out List<QuestCondition> conditions))
            {
                conditions.Remove(condition);
            }
        }
    }

    public void UpdateCurrentCount(QuestTargetType _type, int _targetID)
    {
        if (Quests.TryGetValue(_type, out List<QuestCondition> conditions))
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
}