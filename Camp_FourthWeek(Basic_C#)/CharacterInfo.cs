namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EnterCharacterInfoAction : ActionBase
{
    int addId = 99;

    Tuple<int, int> monsterPivot = new Tuple<int, int>(10, 23);

    public EnterCharacterInfoAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap[0] = PrevAction;
    }

    public override string Name => "상태 보기";

    public override void OnExcute()
    {
        int dicNum = 16;
        Dictionary<int, string> stringDic = new Dictionary<int, string>();
        int index = Array.IndexOf(Enum.GetValues(typeof(MonsterType)), PlayerInfo.Monster.Type) + addId;
        var pivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) },
            { 1, monsterPivot },
        };
        var myMonsterList = new List<int>() { index };

        stringDic.Add(dicNum++, PlayerInfo.Name);
        stringDic.Add(dicNum++, PlayerInfo.Gold.ToString());
        stringDic.Add(dicNum++, PlayerInfo.Monster.Name);
        stringDic.Add(dicNum++, PlayerInfo.Monster.Lv.ToString());

        foreach (var stat in PlayerInfo.Monster.Stats.Values)
        {
            if (stat.Type == StatType.MaxHp || stat.Type == StatType.MaxMp)
                continue;
            StringBuilder sb = new StringBuilder();
            sb.Append(stat.FinalValue);
            if (stat.Type == StatType.CurHp)
            {
                sb.Append($" / {PlayerInfo.Monster.Stats[StatType.MaxHp].FinalValue}");
            }
            else if (stat.Type == StatType.CurMp)
            {
                sb.Append($" / {PlayerInfo.Monster.Stats[StatType.MaxMp].FinalValue}");
            }

            stringDic.Add(dicNum++, sb.ToString());
        }

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Status, pivotDic, (10, stringDic), myMonsterList));
    }
}