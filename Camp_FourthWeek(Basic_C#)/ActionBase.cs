namespace Camp_FourthWeek_Basic_C__;

public abstract partial class ActionBase : IAction
{
    private string feedBackMessage = string.Empty;
    protected IAction? PrevAction;
    protected Dictionary<int, IAction> SubActionMap = new();
    protected PlayerInfo PlayerInfo { get; } = GameManager.Instance.PlayerInfo;
    public abstract string Name { get; }

    public void Execute()
    {
        Console.Clear();
        Console.WriteLine($"[{Name}]");
        OnExcute();
    }

    public void SetFeedBackMessage(string _message)
    {
        feedBackMessage = _message;
    }

    public abstract void OnExcute();

    public void SelectAndRunAction(Dictionary<int, IAction> _actionMap)
    {
        Console.WriteLine();
        foreach (var action in _actionMap) Console.WriteLine($"{action.Key}. {action.Value.Name}");
        Console.WriteLine();
        Console.WriteLine($"0. {(PrevAction == null ? "종료하기" : $"{PrevAction.Name}로 되돌아가기")}");
        Console.WriteLine();
        Console.WriteLine(feedBackMessage);
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        while (true)
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                if (id == 0)
                {
                    if (PrevAction == null && this is MainMenuAction)
                        GameManager.Instance.SaveGame();
                    else
                        PrevAction?.Execute();
                    break;
                }

                if (_actionMap.ContainsKey(id))
                {
                    _actionMap[id].Execute();
                    break;
                }
                //개발자의 이스터에그
                else if (id == 527)
                {
                    var miniGame = new MiniGame();
                    miniGame.StartGame();
                    break;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }


        feedBackMessage = string.Empty;
    }
}