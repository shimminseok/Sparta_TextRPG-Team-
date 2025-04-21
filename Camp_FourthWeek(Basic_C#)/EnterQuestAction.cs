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
                1, new ActionveQuestAction(this)
            },
            {
                2, new AcceptableQuest(this)
            }
        };
    }

    public override void OnExcute()
    {
        // SubActionMap.Clear();
        //퀘스트 Dic에서 현재 내가 수락할 수 있는 퀘스트만 뿌려줘야함...
        //어떻게..?
        // 1. 퀘스트 Dic에서 모든 퀘스트를 들고옴
        // 2. 퀘스트 Dic에서 이전 퀘스트 클리어 조건을 확인 후 뿌려줌

        SelectAndRunAction(SubActionMap);
    }
}

public class ActionveQuestAction : ActionBase
{
    public override string Name => "진행중 퀘스트 목록";

    public ActionveQuestAction(IAction _preAction)
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

public class AcceptableQuest : ActionBase
{
    public override string Name => "수락 가능한 퀘스트 목록";

    public AcceptableQuest(IAction _preAction)
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
            string targetName = string.Empty;
            switch (condition.TargetType)
            {
                case QuestTargetType.Item:
                    targetName = ItemTable.GetItemById(condition.TargetID).Name;
                    break;
                case QuestTargetType.Monster:
                    // targetName = .GetItemById(condition.TargetID).Name;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (condition.IsCompleted)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine
                ($"{string.Format(condition.QuestConditionTxt, targetName)} ({condition.CurrentCount}/{condition.RequiredCount})");

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
        PrevAction?.SetFeedBackMessage("퀘스트 완료!!");
        PrevAction?.Execute();
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