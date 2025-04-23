namespace Camp_FourthWeek_Basic_C__;

public class DungeonAction : ActionBase
{
    private readonly Dungeon? dungeon;

    public DungeonAction(int _dunNum, IAction _prevAction)
    {
        dungeon = StageTable.GetDungeonById(_dunNum);
        PrevAction = _prevAction;
    }

    public override string Name => dungeon.DungeonName;

    public override void OnExcute()
    {
        //전투 시작
        Console.WriteLine("전투를 시작해요!!");
        PrevAction!.Execute();
    }
}