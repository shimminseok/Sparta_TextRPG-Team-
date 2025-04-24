namespace Camp_FourthWeek_Basic_C__;

public abstract class PagedListActionBase : ActionBase
{
    protected const int VIEW_COUNT = 3;
    protected int Page;
    public bool isView{get;set;}
    protected int MaxPage;
    protected bool isViewSubMap = true;
    protected abstract List<string> GetPageContent();
    protected abstract PagedListActionBase CreateNew(int newPage);

    public PagedListActionBase(IAction _prevAction, int _page,bool _isView = true)
    {
        PrevAction = _prevAction;
        Page = _page;
        isView = _isView;
    }

    public override void OnExcute()
    {


        PageNavigationFactory.AddPageNavigation(SubActionMap, p => CreateNew(p), Page, MaxPage);
        /*
    SubActionMap.Clear();
        var lines = GetPageContent();
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();
        Console.WriteLine($"[{Page + 1}/{MaxPage}] 페이지");
        Console.WriteLine();

        if (Page > 0)
            Console.WriteLine("-1. 이전 페이지");
        if (Page < MaxPage - 1)
            Console.WriteLine("-2. 다음 페이지");
        if (isView)
        {
            SelectAndRunAction(SubActionMap, isViewSubMap);
        }else if (!isView)
        {
            SelectAndRunAction(SubActionMap, false);
        }
        */
    }
}