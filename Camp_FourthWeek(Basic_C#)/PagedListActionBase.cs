namespace Camp_FourthWeek_Basic_C__;

public abstract class PagedListActionBase : ActionBase
{
    protected const int VIEW_COUNT = 3;
    protected int Page;
    public bool isView { get; set; }
    protected int MaxPage;
    protected bool isViewSubMap = true;
    protected abstract List<string> GetPageContent();
    protected abstract PagedListActionBase CreateNew(int newPage);

    public PagedListActionBase(IAction _prevAction, int _page, bool _isView = true)
    {
        PrevAction = _prevAction;
        Page = _page;
        isView = _isView;
    }

    public override void OnExcute()
    {
        PageNavigationFactory.AddPageNavigation(SubActionMap, p => CreateNew(p), Page, MaxPage);
    }
}