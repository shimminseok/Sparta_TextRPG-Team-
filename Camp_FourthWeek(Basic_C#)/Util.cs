namespace Camp_FourthWeek_Basic_C__;

public static class PageNavigationFactory
{
    public static void AddPageNavigation<T>(Dictionary<int, IAction> _map,
        Func<int, T> _createAction, int _page, int _maxPage) where T : IAction
    {
        if (_page > 0)
        {
            _map[-1] = _createAction(_page - 1);
        }

        if (_page < _maxPage - 1)
        {
            _map[-2] = _createAction(_page + 1);
        }
    }
}