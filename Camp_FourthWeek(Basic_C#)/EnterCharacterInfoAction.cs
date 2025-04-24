namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EnterCharacterInfoAction : ActionBase
{
    public EnterCharacterInfoAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "상태 보기";

    public override void OnExcute()
    {
        Console.WriteLine("캐릭터의 정보가 표시됩니다.");
        Console.WriteLine();

        Console.WriteLine($"{PadRightWithKorean("트레이너", 12)} : {PlayerInfo.Name}");      
        Console.WriteLine($"{PadRightWithKorean("Gold", 12)} : {PlayerInfo.Gold} G");
        Console.WriteLine();
        Console.WriteLine("[장착중인 포켓몬]");
        Console.WriteLine(PadRightWithKorean($"{PadRightWithKorean("포켓몬", 12)} : {PlayerInfo.Monster.Name}", 10));
        Console.WriteLine($"{PadRightWithKorean("Lv.", 12)} : {LevelManager.CurrentLevel}");
        Console.WriteLine();
        Console.WriteLine("[포켓몬 능력치]");
        foreach (var stat in PlayerInfo.Stats.Values)
            Console.WriteLine(
                $"{PadRightWithKorean(stat.GetStatName(), 12)} : {PadRightWithKorean(stat.FinalValue.ToString("N0"), 9)} +({stat.EquipmentValue})");


        SelectAndRunAction(SubActionMap);
    }
}