namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;
using System.Text;

public class DungeonAction : ActionBase
{
    private readonly Dungeon? dungeon;

    public DungeonAction(int _dunNum, IAction _prevAction)
    {
        dungeon = DungeonTable.GetDungeonById(_dunNum);
        PrevAction = _prevAction;
    }

    public override string Name => SetName();

    public override void OnExcute()
    {
        //던전 도전 조건 판단
        var dungeonStat = dungeon.RecommendedStat;
        var playerStat = PlayerInfo.Stats[dungeonStat.Type].FinalValue;

        if (35 - playerStat - dungeonStat.FinalValue > PlayerInfo.Stats[StatType.CurHp].FinalValue)
        {
            PrevAction!.SetFeedBackMessage("체력이 부족합니다.");
            PrevAction?.Execute();
            return;
        }

        var rans = new Random();
        float damage = rans.Next(20, 36);
        damage -= playerStat - dungeonStat.FinalValue;


        string message;
        var sb = new StringBuilder();

        //권장 방어력이 높으면
        if (dungeonStat.FinalValue > playerStat)
        {
            //던전을 실패할 수있음 40프로 확률
            var percent = rans.NextSingle();
            if (percent <= 0.4f)
                message = dungeon.UnClearDungeon(damage);
            else
                message = dungeon.ClearDungeon(damage);
        }
        else
        {
            message = dungeon.ClearDungeon(damage);
        }


        PrevAction!.SetFeedBackMessage(message);
        PrevAction!.Execute();
    }

    private string SetName()
    {
        var sb = new StringBuilder();
        sb.Append($"{PadRightWithKorean($"{dungeon.DungeonName}", 12)} ");

        sb.Append(PadRightWithKorean($"| {dungeon.RecommendedStat.GetStatName()}", 10));
        sb.Append(PadRightWithKorean($"+{dungeon.RecommendedStat.FinalValue} 이상", 10));

        sb.Append(" |");
        return sb.ToString();
    }
}