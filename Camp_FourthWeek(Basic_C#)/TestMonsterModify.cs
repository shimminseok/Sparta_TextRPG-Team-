namespace Camp_FourthWeek_Basic_C__;

public class TestMonsterModify : ActionBase
{
    public TestMonsterModify(IAction _preAction)
    {
        PrevAction = _preAction;
        NextAction = PrevAction;
    }

    public override string Name => "테스트 포켓몬 추가";

    public override void OnExcute()
    {
        var player = GameManager.Instance.PlayerInfo;

        // 예시 몬스터 타입
        var types = Enum.GetValues(typeof(MonsterType))
            .Cast<MonsterType>()
            .Where(t => t != MonsterType.None)
            .ToList();

        var rand = new Random();
        var newType = types[rand.Next(types.Count)];

        InventoryManager.Instance.AddMonsterToBox(MonsterTable.GetMonsterByType(MonsterType.Pikachu).Copy());
    }
}