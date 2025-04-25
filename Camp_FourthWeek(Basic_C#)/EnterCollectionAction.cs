using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public class EnterCollectionAction : PagedListActionBase
{
    public override string Name => "포켓몬 도감";


    public EnterCollectionAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
        MaxPage = (int)Math.Ceiling(MonsterTable.MonsterDataDic.Count / (float)VIEW_COUNT);
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EnterCollectionAction(PrevAction, newPage);
    }

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        var keys = MonsterTable.MonsterDataDic.Keys.ToList();
        int start = (Page * VIEW_COUNT);
        int end = Math.Min(start + VIEW_COUNT, keys.Count);

        int viewIndex = start + 1;
        for (int i = start; i < end; i++)
        {
            var monster = keys[i];
            bool isDiscovered = CollectionManager.Instnace.IsDiscovered(monster);
            bool isCaptured = CollectionManager.Instnace.IsCaptured(monster);

            string name = (isCaptured || isDiscovered)
                ? MonsterTable.MonsterDataDic[monster].Name
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
    {        var lines = GetPageContent();
        base.OnExcute();

        int LineCount = 5;

        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount + i, lines[i]);
        }
        if (Page > 0)
            lineDic.Add(8, "-1. 이전 페이지");
        if (Page < MaxPage - 1)
            lineDic.Add(9, "-2. 다음 페이지");
        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Collection, null, (5, lineDic)));

    }

    /*public override void OnExcute()
    {
         Console.WriteLine("테스트로 해당 번호 입력시 조우, 한번 더 입력하면 포획입니다.");
         var keys = MonsterTable.MonsterDataDic.Keys.ToList();
         int start = (page * MONSTER_VIEW_COUNT);
         int end = Math.Min(start + MONSTER_VIEW_COUNT, keys.Count);

         var pagedMonsters = keys.Skip(start).Take(MONSTER_VIEW_COUNT).ToList();
         int viewIndex = start + 1;
         foreach (MonsterType monster in pagedMonsters)
         {
             bool isDiscovered = CollectionManager.Instnace.IsDiscovered(monster);
             bool isCaptured = CollectionManager.Instnace.IsCaptured(monster);
             string monsterName = (isCaptured || isDiscovered)
                 ? MonsterTable.MonsterDataDic[monster].Name
                 : "???";

             StringBuilder sb = new StringBuilder(monsterName);

             if (isCaptured)
             {
                 sb.Append("\t [포획 완료]");
                 Console.ForegroundColor = ConsoleColor.Green;
             }

             Console.WriteLine($"{viewIndex++}. {sb.ToString()}");
             Console.ResetColor();
         }

         if (page > 0)
             SubActionMap[1] = new CollectionPrevPageAction(PrevAction, page - 1);
         if (end < keys.Count)
             SubActionMap[2] = new CollectionNextPageAction(PrevAction, page + 1);
        IAction action = new EnterCollectionAction(null, page);
        action.Execute();
        SelectAndRunAction(SubActionMap);
        // SelectAndRunAction(SubActionMap);
    }*/
}