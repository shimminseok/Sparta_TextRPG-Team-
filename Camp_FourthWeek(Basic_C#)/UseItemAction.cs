namespace Camp_FourthWeek_Basic_C__;

public class UseItemAction : ActionBase
{
    public override string Name => "열매 관리";
    private readonly int fruitCount = InventoryManager.Instance.FruitCount;

    public UseItemAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        var player = GameManager.Instance.PlayerInfo;
        var monster = player.Monster;
        var curHp = monster.Stats[StatType.CurHp].FinalValue;
        SubActionMap.Clear();
        if (fruitCount > 0)
        {
            SubActionMap[1] = new UseFruitAction(this);
        }
        else
        {
            Console.WriteLine("열매가 없습니다.");
        }

        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        lineDic.Add(5, $"이곳에서 열매를 사용하여 포켓몬의 체력을 {InventoryManager.HEAL_AMOUNT}회복시킬 수 있습니다. (열매 수: {fruitCount})");
        lineDic.Add(6, $"현재체력: {curHp}");


        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Fruit, null, (5, lineDic)));
    }
}