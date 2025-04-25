namespace Camp_FourthWeek_Basic_C__;

public abstract partial class ActionBase : IAction
{
    private string feedBackMessage = string.Empty;
    protected IAction? PrevAction;
    protected Dictionary<int, IAction> SubActionMap = new();
    protected PlayerInfo PlayerInfo { get; } = GameManager.Instance.PlayerInfo;
    public abstract string Name { get; }

    public IAction? NextAction { get; protected set; } = null;

    public void Execute()
    {
        // 추후 아래의 콘솔은 지워주어야 합니다.
        Console.Clear();
        Console.WriteLine($"[{Name}]");
        OnExcute();
    }

    public void SetFeedBackMessage(string _message)
    {
        feedBackMessage = _message;
    }

    public abstract void OnExcute();

    public void SelectAndRunAction(Dictionary<int, IAction> _actionMap, bool _isView = true,
        Func<string>? inputProvider = null)
    {
        Console.WriteLine();
        if (_isView)
        {
            foreach (var action in _actionMap)
            {
                if (action.Key < 0)
                    continue;
                Console.WriteLine($"{action.Key}. {action.Value.Name}");
            }
        }

        while (true)
        {
            if (int.TryParse(inputProvider?.Invoke() ?? Console.ReadLine(), out var id))
            {
                if (UiManager.isLoop)
                    UiManager.isLoop = false;

                if (id == 0)
                {
                    if (PrevAction == null && this is MainMenuAction)
                    {
                        // GameManager.Instance.SaveGame();
                        NextAction = null;
                    }
                    else
                    {
                        NextAction = PrevAction;
                    }

                    break;
                }

                if (_actionMap.ContainsKey(id))
                {
                    NextAction = _actionMap[id];
                    break;
                }
                //개발자의 이스터에그
                else if (id == 527)
                {
                    var miniGame = new MiniGame();
                    miniGame.StartGame();
                    break;
                }
            }
        }


        feedBackMessage = string.Empty;
    }
}