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
        SelectAndRunAction(SubActionMap);
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
        int index = 1;
        foreach (var quest in QuestManager.Instance.CurrentAcceptQuestList)
        {
            SubActionMap[index++] = new DisplayQuestInfoAction(quest, this);
        }

        SelectAndRunAction(SubActionMap);
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
        int index = 1;
        foreach (Quest quest in QuestTable.QuestDic.Values)
        {
            bool canAccept = quest.PrerequisiteQuest.All(pre => QuestManager.Instance.ClearQuestList.Contains(pre))
                             && !QuestManager.Instance.ClearQuestList.Contains(quest.Key)
                             && !QuestManager.Instance.CurrentAcceptQuestList.Exists(x => x.Key == quest.Key);
            if (canAccept)
            {
                SubActionMap[index++] = new DisplayQuestInfoAction(quest, this);
            }
        }

        SelectAndRunAction(SubActionMap);
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
        Console.WriteLine(requiredQuest.QuestName);

        Console.WriteLine();
        Console.WriteLine(requiredQuest.QuestDescription);
        Console.WriteLine();

        foreach (QuestCondition condition in requiredQuest.Conditions)
        {
            if (condition.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine
                ($"{string.Format(condition.GetDescription())} ({condition.CurrentCount}/{condition.RequiredCount})");

            Console.ResetColor();
        }

        if (requiredQuest.IsCompleted())
        {
            SubActionMap[1] = new ClaimQuestRewardAction(requiredQuest, PrevAction);
        }
        else
        {
            if (QuestManager.Instance.CurrentAcceptQuestList.Contains(requiredQuest))
            {
                SubActionMap[1] = new AbandonQuestAction(requiredQuest, PrevAction);
            }
            else
            {
                SubActionMap[1] = new AcceptQuestAction(requiredQuest, PrevAction);
            }
        }

        SelectAndRunAction(SubActionMap);
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
    }

    public override void OnExcute()
    {
        QuestManager.Instance.AcceptQuest(requiredQuest);

        PrevAction?.SetFeedBackMessage($"{requiredQuest.QuestName} 퀘스트를 수락하셨습니다.");
        PrevAction?.Execute();
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
    }

    public override void OnExcute()
    {
        QuestManager.Instance.QuestClear(requiredQuest);
        Console.WriteLine(requiredQuest.QuestCompleteDescription);

        SelectAndRunAction(SubActionMap);
    }
}

//보류
public class AbandonQuestAction : ActionBase
{
    public override string Name => "포기하기";
    readonly Quest requiredQuest;

    public AbandonQuestAction(Quest _requiredQuest, IAction _preAction)
    {
        PrevAction = _preAction;
        requiredQuest = _requiredQuest;
    }

    public override void OnExcute()
    {
        QuestManager.Instance.AbandonQuest(requiredQuest);

        PrevAction?.SetFeedBackMessage($"{requiredQuest.QuestName} 퀘스트를 포기하셨습니다.");
        PrevAction?.Execute();
    }
}