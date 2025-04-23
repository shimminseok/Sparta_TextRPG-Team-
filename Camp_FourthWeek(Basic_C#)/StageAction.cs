namespace Camp_FourthWeek_Basic_C__;

public class StageAction : ActionBase
{
    public override string Name => stage.StageName;
    private readonly Stage? stage;

    public StageAction(int _dunNum, IAction _prevAction)
    {
        stage = StageTable.GetDungeonById(_dunNum);
        for (int i = 0; i < stage.SpawnedMonsters.Length; i++)
        {
            Monster monster = MonsterTable.GetMonsterByType(stage.SpawnedMonsters[i]);
            Console.Write($"\t{monster.Name}");
        }

        PrevAction = _prevAction;
        SubActionMap[1] = new EnterBattleAction(stage, this);
    }


    public override void OnExcute()
    {
        SelectAndRunAction(SubActionMap);
    }
}