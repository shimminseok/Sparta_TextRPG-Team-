namespace Camp_FourthWeek_Basic_C__;

using System.Text;

public class StageManager
{
    private static readonly StageManager instance = new StageManager();
    public static StageManager Instance => instance;

    public readonly PlayerInfo playerInfo = GameManager.Instance.PlayerInfo;
    public int ClearStage = 0;
    public Stage CurrentStage;

    public void ClearCurrentStage()
    {
        if (ClearStage < CurrentStage.Key)
            ClearStage = CurrentStage.Key;

        playerInfo.Gold += CurrentStage.RewardGold;
    }

    public string UnClearStage(Stage _stage)
    {
        var sb = new StringBuilder();
        sb.AppendLine("던전 공략 실패");
        sb.AppendLine($"{_stage.StageName} 공략에 실패 하였습니다.");

        return sb.ToString();
    }
}