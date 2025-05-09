using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public class EnterCollectionAction : PagedListActionBase
{
    public override string Name => "포켓몬 도감";


    public EnterCollectionAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        MaxPage = (int)Math.Ceiling(MonsterTable.Instance.MonsterDataDic.Count / (float)VIEW_COUNT);
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EnterCollectionAction(PrevAction, newPage);
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        var keys = MonsterTable.Instance.MonsterDataDic.Keys.ToList();
        int start = (Page * VIEW_COUNT);
        int end = Math.Min(start + VIEW_COUNT, keys.Count);

        int viewIndex = start + 1;
        for (int i = start; i < end; i++)
        {
            var monster = keys[i];
            bool isDiscovered = CollectionManager.Instnace.IsDiscovered(monster);
            bool isCaptured = CollectionManager.Instnace.IsCaptured(monster);

            string name = (isCaptured || isDiscovered)
                ? MonsterTable.Instance.MonsterDataDic[monster].Name
                : "???";

            var line = new StringBuilder($"{viewIndex++}. {name}");

            if (isCaptured)
            {
                line.Append(" \t[포획 완료]");
                Console.ForegroundColor = ConsoleColor.Green;
            }

            output.Add(line.ToString());
            Console.ResetColor();
        }

        return output;
    }

    public override void OnExcute()
    {
        var lines = GetPageContent();
        base.OnExcute();

        int LineCount = 5;

        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount++, lines[i]);
        }

        for (int i = LineCount; i < 8; i++)
        {
            lineDic.Add(LineCount++, "");
        }

        lineDic[8] = Page > 0 ? "-1. 이전 페이지" : "";
        lineDic[9] = Page < MaxPage - 1 ? "-2. 다음 페이지" : "";

        SelectAndRunAction(SubActionMap, isViewSubMap,
            () => UiManager.UIUpdater(UIName.Collection, null, (5, lineDic)));
    }
}