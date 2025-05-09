using System;
using System.Linq;

namespace Camp_FourthWeek_Basic_C__;

public class EnterQuestAction : ActionBase
{
    public override string Name => "퀘스트";

    public EnterQuestAction(IAction _preAction)
    {
        PrevAction = _preAction;
        SubActionMap = new Dictionary<int, IAction>()
        {
            {
                1, new ActiveQuestAction(this)
            },
            {
                2, new AcceptableQuestAction(this)
            }
        };
    }

    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Quest_Main));
    }
}

public class ActiveQuestAction : ActionBase
{
    public override string Name => "진행중 퀘스트 목록";

    public ActiveQuestAction(IAction _preAction)
    {
        PrevAction = _preAction;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        SubActionMap[0] = PrevAction;
        int index = 1;
        int LineCount = 4;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        foreach (var quest in QuestManager.Instance.CurrentAcceptQuestList)
        {
            SubActionMap[index++] = new DisplayQuestInfoAction(quest, this);
            lineDic.Add(LineCount + index - 1, $"{index - 1}. {quest.QuestName}");
        }

        for (int i = LineCount + index; i < 14; i++)
        {
            lineDic.Add(i, "");
        }

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Quest_List_Working, null, (10, lineDic)));
    }
}

public class AcceptableQuestAction : ActionBase
{
    public override string Name => "수락 가능한 퀘스트 목록";

    public AcceptableQuestAction(IAction _preAction)
    {
        PrevAction = _preAction;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        SubActionMap[0] = PrevAction;
        int index = 1;
        int LineCount = 4;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        foreach (Quest quest in QuestTable.Instance.QuestDic.Values)
        {
            bool canAccept = quest.PrerequisiteQuest.All(pre => QuestManager.Instance.ClearQuestList.Contains(pre))
                             && !QuestManager.Instance.ClearQuestList.Contains(quest.Key)
                             && !QuestManager.Instance.CurrentAcceptQuestList.Exists(x => x.Key == quest.Key);

            if (canAccept)
            {
                SubActionMap[index++] = new DisplayQuestInfoAction(quest, this);
                lineDic.Add(LineCount + index - 1, $"{index - 1}. {quest.QuestName}");
            }
        }

        for (int i = LineCount + index; i < 14; i++)
        {
            lineDic.Add(i, "");
        }


        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Quest_List_Acceptyet, null, (10, lineDic)));
    }
}

public class DisplayQuestInfoAction : ActionBase
{
    public override string Name => $"{requiredQuest.QuestName}";
    readonly Quest requiredQuest;

    public DisplayQuestInfoAction(Quest _quest, IAction _action)
    {
        requiredQuest = _quest;
        PrevAction = _action;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(4, requiredQuest.QuestName);
        int descriptionCount = 5;

        string[] lines = requiredQuest.QuestDescription.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            lineDic.Add(descriptionCount++, line);
        }

        for (int i = descriptionCount; i < 16; i++)
        {
            lineDic.Add(i, "");
        }

        int conditionCount = 16;
        foreach (QuestCondition condition in requiredQuest.Conditions)
        {
            if (condition.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Green;

            lineDic.Add(conditionCount++,
                $"{string.Format(condition.GetDescription())} ({condition.CurrentCount}/{condition.RequiredCount})");

            Console.ResetColor();
        }

        for (int i = conditionCount; i < 25; i++)
        {
            lineDic.Add(i, "");
        }


        if (requiredQuest.IsCompleted())
        {
            SubActionMap[1] = new ClaimQuestRewardAction(requiredQuest, PrevAction);
            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Quest_Detail_Clear, null, (10, lineDic)));
        }
        else
        {
            if (QuestManager.Instance.CurrentAcceptQuestList.Contains(requiredQuest))
            {
                SubActionMap[1] = new AbandonQuestAction(requiredQuest, PrevAction);
                SelectAndRunAction(SubActionMap, false,
                    () => UiManager.UIUpdater(UIName.Quest_Detail_Working, null, (10, lineDic)));
            }
            else
            {
                SubActionMap[1] = new AcceptQuestAction(requiredQuest, PrevAction);
                SelectAndRunAction(SubActionMap, false,
                    () => UiManager.UIUpdater(UIName.Quest_Detail_Acceptyet, null, (10, lineDic)));
            }
        }
    }
}

public class AcceptQuestAction : ActionBase
{
    public override string Name => "수락하기";
    readonly Quest requiredQuest = new Quest();

    public AcceptQuestAction(Quest _requiredQuest, IAction _preAction)
    {
        PrevAction = _preAction;
        requiredQuest = _requiredQuest;
        NextAction = _preAction;
    }

    public override void OnExcute()
    {
        QuestManager.Instance.AcceptQuest(requiredQuest);
    }
}

public class ClaimQuestRewardAction : ActionBase
{
    public override string Name => "보상 받기";
    readonly Quest requiredQuest = new Quest();

    public ClaimQuestRewardAction(Quest _requiredQuest, IAction _preAction)
    {
        PrevAction = _preAction;
        requiredQuest = _requiredQuest;
        SubActionMap[0] = _preAction;
    }

    public override void OnExcute()
    {
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(4, requiredQuest.QuestName);
        int descriptionCount = 5;

        string[] lines =
            requiredQuest.QuestCompleteDescription.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            lineDic.Add(descriptionCount++, line);
        }

        for (int i = descriptionCount; i < 17; i++)
        {
            lineDic.Add(i, "");
        }

        int conditionCount = 17;
        foreach (var monster in requiredQuest.RewardMonsters)
        {
            Monster reward = MonsterTable.Instance.GetMonsterByType(monster);
            lineDic.Add(conditionCount++, reward.Name);
        }

        foreach (int item in requiredQuest.RewardItemsList)
        {
            Item reward = ItemTable.Instance.GetItemById(item);
            lineDic.Add(conditionCount++, reward.Name);
        }

        lineDic.Add(conditionCount++, $"{requiredQuest.QuestRewardGold}G");

        for (int i = conditionCount; i < 25; i++)
        {
            lineDic[i] = "";
        }

        QuestManager.Instance.QuestClear(requiredQuest);
        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Quest_Clear, null, (10, lineDic)));
    }
}

public class AbandonQuestAction : ActionBase
{
    public override string Name => "포기하기";
    readonly Quest requiredQuest;

    public AbandonQuestAction(Quest _requiredQuest, IAction _preAction)
    {
        PrevAction = _preAction;
        requiredQuest = _requiredQuest;
        NextAction = _preAction;
    }

    public override void OnExcute()
    {
        QuestManager.Instance.AbandonQuest(requiredQuest);
    }
}