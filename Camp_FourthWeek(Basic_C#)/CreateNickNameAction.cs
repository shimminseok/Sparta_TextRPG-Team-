namespace Camp_FourthWeek_Basic_C__;

public class CreateNickNameAction : ActionBase
{
    private string? nickName = string.Empty;
    public override string Name => "닉네임 설정";
    private MonsterType[] startPoketmon = [MonsterType.Charmander, MonsterType.Squirtle, MonsterType.Bulbasaur];

    public override void OnExcute()
    {
        SubActionMap.Clear();
        do
        {
            nickName = UiManager.UIUpdater(UIName.Intro_TextBox);
        } while (string.IsNullOrEmpty(nickName));

        for (int i = 0; i < startPoketmon.Length; i++)
        {
            var key = startPoketmon[i];
            Monster monster = MonsterTable.GetMonsterByType(key);
            SubActionMap.Add(i + 1, new SelectMonsterAction(monster, nickName));
        }

        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Intro_SetStarting,
            new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 10) },
                { 1, new Tuple<int, int>(20, 10) },
                { 2, new Tuple<int, int>(70, 10) },
                { 3, new Tuple<int, int>(120, 10) },
            }));
        // SubActionMap = new Dictionary<int, IAction>
        // {
        //     { 1, new SelectedMonsterAction(nickName) }
        // };
        // SelectAndRunAction(SubActionMap, true, null, true);
    }
}