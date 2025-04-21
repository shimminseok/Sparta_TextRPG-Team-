using System.Collections.Generic;
using System.Linq.Expressions;

namespace Camp_FourthWeek_Basic_C__;

public class QuestManager
{
    private static readonly QuestManager instance = new QuestManager();

    //퀘스트를 관리하는 Dic
    public Dictionary<QuestTargetType, List<Quest>> Quests { get; private set; } =
        new Dictionary<QuestTargetType, List<Quest>>();

    //현재 내가 수락한 실질적인 Quest List
    public List<int> CurrentAcceptQuestList { get; private set; } = new List<int>();
    public List<int> ClearQuestList { get; private set; } = new List<int>();
    public static QuestManager Instance => instance;


    public void AcceptQuest(Quest _quest)
    {
        for (int i = 0; i < _quest.Conditions.Count; i++)
        {
            if (!Quests.ContainsKey(_quest.Conditions[i].TargetType))
            {
                Quests[_quest.Conditions[i].TargetType] = new List<Quest>();
            }

            Quests[_quest.Conditions[i].TargetType].Add(_quest);
        }
    }

    public void QuestClear(Quest _quest)
    {
        foreach (var condition in _quest.Conditions)
        {
            if (Quests.TryGetValue(condition.TargetType, out List<Quest> quests))
            {
                quests.RemoveAll(quest => quest.Key == _quest.Key);
            }
        }

        CurrentAcceptQuestList.Remove(_quest.Key);
        ClearQuestList.Add(_quest.Key);
    }

    public Quest? GetQuestInfo(int _key)
    {
        foreach (var quests in Quests.Values)
        {
            var targetQuest = quests.Find(quest => quest.Key == _key);
            return targetQuest;
        }

        return null;
    }
}