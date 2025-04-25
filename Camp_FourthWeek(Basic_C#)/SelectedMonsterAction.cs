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
        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Intro_SetStarting, new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,10) },
                      {1, new Tuple<int, int>(20,10) },
                      {2, new Tuple<int, int>(70,10) },
                      {3, new Tuple<int, int>(120,10) },

                  }));
    }
}