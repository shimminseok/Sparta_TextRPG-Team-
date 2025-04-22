using System.Text;

namespace Camp_FourthWeek_Basic_C__;

public class EnterCollectionAction : ActionBase
{
    public const int MONSTER_VIEW_COUNT = 3;
    public override string Name => "포켓몬 도감";
    private int page = 0;

    public EnterCollectionAction(IAction _prevAction, int _page = 0)
    {
        PrevAction = _prevAction;
        page = _page;
    }

    public override void OnExcute()
    {
        Console.WriteLine("테스트로 해당 번호 입력시 조우, 한번 더 입력하면 포획입니다.");
        var keys = MonsterTable.MonsterDataDic.Keys.ToList();
        int start = (page * MONSTER_VIEW_COUNT);
        int end = Math.Min(start + MONSTER_VIEW_COUNT, keys.Count);

        var pagedMonsters = keys.Skip(start).Take(MONSTER_VIEW_COUNT).ToList();
        int viewIndex = start + 1;
        foreach (var monster in pagedMonsters)
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

        SelectAndRunAction(SubActionMap);
    }

    public class CollectionNextPageAction : ActionBase
    {
        public override string Name => "다음 페이지";
        private int page = 0;

        public CollectionNextPageAction(IAction _prevAction, int _page)
        {
            PrevAction = _prevAction;
            page = _page;
        }

        public override void OnExcute()
        {
            var nextPage = new EnterCollectionAction(PrevAction, page);
            nextPage.Execute();
        }
    }

    public class CollectionPrevPageAction : ActionBase
    {
        public override string Name => "이전 페이지";
        private int page = 0;

        public CollectionPrevPageAction(IAction _prevAction, int _page)
        {
            PrevAction = _prevAction;
            page = _page;
        }

        public override void OnExcute()
        {
            var prevPage = new EnterCollectionAction(PrevAction, page);
            prevPage.Execute();
        }
    }
}