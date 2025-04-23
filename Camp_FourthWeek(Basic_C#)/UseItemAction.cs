namespace Camp_FourthWeek_Basic_C__;

public class UseItemAction : ActionBase
{
    public override string Name => "열매 관리";
    public static int fruitCount = 3;

    public UseItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        var player = GameManager.Instance.PlayerInfo;
        var monster = player.Monster;
        var curHp = monster.Stats[StatType.CurHp].FinalValue;
        Console.WriteLine($"이곳에서 열매를 사용하여 포켓몬의 체력을 30회복시킬 수 있습니다. (열매 수: {fruitCount})");
        Console.WriteLine();
        Console.WriteLine($"현재체력: {curHp}");
        Console.WriteLine();
        SubActionMap.Clear();
        if (fruitCount > 0)
        {
            SubActionMap[1] = new UseFruitAction(this);
        }
        else
        {
            Console.WriteLine("열매가 없습니다.");
        }

        SelectAndRunAction(SubActionMap);
    }
}