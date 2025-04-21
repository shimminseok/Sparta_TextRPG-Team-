namespace Camp_FourthWeek_Basic_C__;

public class EnterQuestAction : ActionBase
{
    public override string Name => "퀘스트";

    public override void OnExcute()
    {
        SubActionMap.Clear();
    }
}

public class AcceptQuestAction : ActionBase
{
    public override string Name => "수락하기";

    public override void OnExcute()
    {
    }
}