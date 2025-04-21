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
            Console.WriteLine("모험가님의 이름을 설정해주세요.");
            nickName = Console.ReadLine();
        } while (string.IsNullOrEmpty(nickName));

        Console.WriteLine($"{nickName}님 안녕하세요.");
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new SelectedJobAction(nickName) }
        };
        SelectAndRunAction(SubActionMap);
    }
}