using System.Linq;

namespace Camp_FourthWeek_Basic_C__;

public class EnterQuestAction : ActionBase
{
    public override string Name => "퀘스트";

    public EnterQuestAction(IAction _preAction)
    {
        PrevAction = _preAction;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        //퀘스트 Dic에서 현재 내가 수락할 수 있는 퀘스트만 뿌려줘야함...
        //어떻게..?
        // 1. 퀘스트 Dic에서 모든 퀘스트를 들고옴
        // 2. 퀘스트 Dic에서 이전 퀘스트 클리어 조건을 확인 후 뿌려줌

        int index = 1;
        foreach (Quest quest in Camp_FourthWeek_Basic_C__.QuestTable.QuestDic.Values)
        {
            bool canAccept = quest.PrerequisiteQuest.All(pre => QuestManager.Instance.ClearQuestList.Contains(pre));
            if (canAccept)
            {
                SubActionMap[index++] = new AcceptQuestAction(quest, this);
            }
        }
    }
}

public class AcceptQuestAction : ActionBase
{
    public override string Name => "수락하기";
    public Quest RequiredQuest { get; private set; } = new Quest();

    public AcceptQuestAction(Quest _quest, IAction _action)
    {
        RequiredQuest = _quest;
        PrevAction = _action;
    }

    public override void OnExcute()
    {
    }
}