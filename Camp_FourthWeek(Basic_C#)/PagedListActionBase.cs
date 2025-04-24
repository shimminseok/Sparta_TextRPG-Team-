namespace Camp_FourthWeek_Basic_C__;

public abstract class PagedListActionBase : ActionBase
{
    protected const int VIEW_COUNT = 3;
    protected int Page;
    protected int lineCount = 5;
    protected int MaxPage;
    protected bool isViewSubMap = true;
    protected abstract List<string> GetPageContent();
    protected abstract PagedListActionBase CreateNew(int newPage);

    public PagedListActionBase(IAction _prevAction, int _page)
    {
        PrevAction = _prevAction;
        Page = _page;
    }


    public override void OnExcute()
    {
        // SubActionMap.Clear();

        var lines = GetPageContent();
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for(int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(lineCount + i, lines[i]);
        }

        Console.WriteLine($"[{Page + 1}/{MaxPage}] 페이지");
         Console.WriteLine();

       if (Page > 0)
            lineDic.Add(8,"-1. 이전 페이지");
        if (Page < MaxPage - 1)
            lineDic.Add(9,"-2. 다음 페이지");


       PageNavigationFactory.AddPageNavigation(SubActionMap, p => CreateNew(p), Page, MaxPage);

        SelectAndRunAction(SubActionMap, isViewSubMap,() => UiManager.UIUpdater(UIName.Collection, null, (5, lineDic)));
    }
}