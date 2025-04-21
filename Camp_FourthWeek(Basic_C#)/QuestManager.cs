namespace Camp_FourthWeek_Basic_C__;

public class QuestManager
{
    private static QuestManager instance;
    public List<Quest> quests = new List<Quest>();

    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestManager();
            }

            return instance;
        }
    }

    // public void
}