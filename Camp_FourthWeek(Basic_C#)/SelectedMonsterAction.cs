namespace Camp_FourthWeek_Basic_C__;

public class SelectedMonsterAction : ActionBase
{
    public SelectedMonsterAction(string _name)
    {
        foreach (var monster in MonsterTable.MonsterDataDic.Values)
            SubActionMap.Add((int)monster.Type, new SelectMonsterAction(monster, _name));
    }

    public override string Name => "직업 선택";

    public override void OnExcute()
    {
        Console.WriteLine("플레이 하실 직업을 선택해주세요.");
        Console.WriteLine();
        foreach (var monster in MonsterTable.MonsterDataDic.Values) 
            Console.Write($"\t{monster.Name}");
        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }
}