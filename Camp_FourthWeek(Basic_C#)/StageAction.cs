namespace Camp_FourthWeek_Basic_C__;

public class StageAction : ActionBase
{
    public override string Name => stage.StageName;
    private readonly Stage? stage;

    public StageAction(int _key, IAction _prevAction)
    {
        PrevAction = _prevAction;
        stage = StageTable.GetDungeonById(_key);
    }


    public override void OnExcute()
    {
        //전투 시작
        TestBattleAction battle = new TestBattleAction(stage.Key, this);
        battle.OnExcute();
        SelectAndRunAction(SubActionMap);
        // PrevAction!.Execute();
    }
}

public class TestBattleAction : ActionBase
{
    public override string Name => "전투 시작";
    private Stage stage;
    private Random random = new Random();

    public TestBattleAction(int _stageNum, IAction _prevAction)
    {
        stage = StageTable.GetDungeonById(_stageNum);
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        //몬스터를 뿌려주고
        int monsterCount = random.Next(1, 5);
        for (int i = 0; i < monsterCount; i++)
        {
            int monsterIndex = random.Next(0, stage.SpawnedMonsters.Length);
            MonsterType monsterKey = stage.SpawnedMonsters[monsterIndex];
            Monster monster = MonsterTable.GetMonsterByType(monsterKey);
            Console.WriteLine($"{i + 1}. {monster.Name}");

            CollectionManager.Instnace.OnDiscovered(monsterKey);
        }
    }
}