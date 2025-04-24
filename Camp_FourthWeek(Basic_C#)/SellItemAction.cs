namespace Camp_FourthWeek_Basic_C__;

using static Camp_FourthWeek_Basic_C__.StringUtil;

public class SellItemAction : PagedListActionBase
{
    public SellItemAction(IAction _prevAction,int _page=0):base(_prevAction,_page)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "상점 - 아이템 판매";

    protected override List<string> GetPageContent()
    {
        var output = new List<string>();
        isView = false;
        
        int totalInvenCount =  InventoryManager.Instance.Inventory.Count;
        MaxPage = (int)Math.Ceiling(totalInvenCount / (float)VIEW_COUNT);
        int pageStart = Page * VIEW_COUNT;
        int pageEnd = Math.Min(pageStart + VIEW_COUNT, totalInvenCount);
        
        SubActionMap.Clear();
        
        for (var i = 0; i <totalInvenCount; i++)
            SubActionMap.Add(i + 1, new SellAction(InventoryManager.Instance.Inventory[i], this));
        
        for (var i = pageStart; i <pageEnd; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = UiManager.ItemPrinter(item, i, false);
            sb.Append($"| {PadRightWithKorean($"{item.Cost * 0.85}G", 5)}");

            output.Add(sb.ToString());
        }
        
        return output;
    }
    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new SellItemAction(PrevAction, newPage);
    }
}