namespace Camp_FourthWeek_Basic_C__;

public class SelectedMonsterAction : ActionBase
{
    private MonsterType[] startPoketmon = [MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur];

    public SelectedMonsterAction(string _name)
    {
        for (int i = 0; i < startPoketmon.Length; i++)
        {
            var key = startPoketmon[i];
            Monster monster = MonsterTable.GetMonsterByType(key);
            SubActionMap.Add(i + 1, new SelectMonsterAction(monster, _name));
        }
    }

    public override string Name => "포켓몬 선택 선택";

    public override void OnExcute()
    {
        Console.WriteLine("플레이 하실 직업을 선택해주세요.");
        Console.WriteLine();
        foreach (var monster in startPoketmon)
            Console.Write($"\t{MonsterTable.GetMonsterByType(monster).Name}");
        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }
}