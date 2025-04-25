namespace Camp_FourthWeek_Basic_C__;

public class CreateNickNameAction : ActionBase
{
    private string? nickName = string.Empty;
    public override string Name => "닉네임 설정";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        do
        {
            nickName = UiManager.UIUpdater(UIName.Intro_TextBox);
        } while (string.IsNullOrEmpty(nickName));

        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new SelectedMonsterAction(nickName) }
        };
        SelectAndRunAction(SubActionMap, true, null, true);
    }
}