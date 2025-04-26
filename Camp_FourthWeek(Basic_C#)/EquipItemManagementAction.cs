namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EquipItemManagementAction : PagedListActionBase
{
    public override string Name => "도구관리";


    public EquipItemManagementAction(IAction _prevAction, int _page = 0) : base(_prevAction, _page)
    {
        PrevAction = _prevAction;
    }


    protected override List<string> GetPageContent()
    {
        SubActionMap.Clear();
        MaxPage = (int)Math.Ceiling(InventoryManager.Instance.Inventory.Count / (float)VIEW_COUNT);
        var output = new List<string>();


        var inventory = InventoryManager.Instance.Inventory;
        var currentMonster = PlayerInfo.Monster;

        int start = Page * VIEW_COUNT;
        int end = Math.Min(start + VIEW_COUNT, inventory.Count);

        for (var i = start; i < end; i++)
        {
            var item = inventory[i];
            var sb = new StringBuilder();
            sb.Append($"{PadRightWithKorean($"- {i + 1}", 5)}");


            if (item.IsEquippedBy(currentMonster))
            {
                sb.Append("[E]");
            }

            sb.Append($"{PadRightWithKorean($"{item.Name}", 10)}");
            for (var j = 0; j < item.Stats.Count; j++)
            {
                sb.Append(
                    $" | {PadRightWithKorean($"{item.Stats[j].GetStatName()} +{item.Stats[j].FinalValue}", 10)} ");
                sb.Append(" | ");
            }

            Monster? monster = InventoryManager.Instance.MonsterBox.FirstOrDefault(m => m.Item == item);
            string equippedMonsterName = string.Empty;

            if (item.IsEquipment)
            {
                if (monster != null)
                    sb.Append($"{PadRightWithKorean($"장착 중인 포켓몬: {monster.Name}", 25)} | ");
            }

            sb.Append($"{PadRightWithKorean($"{item.Description}{equippedMonsterName}", 20)}");

            output.Add(sb.ToString());

            bool equippedByOther = monster != null && monster != currentMonster;
            if (!equippedByOther)
                SubActionMap[i + 1] = new EquipAction(item, this);
        }

        return output;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        var lines = GetPageContent();
        base.OnExcute();
        int LineCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();
        for (int i = 0; i < lines.Count; i++)
        {
            lineDic.Add(LineCount++, lines[i]);
        }

        for (int i = LineCount; i < 9; i++)
        {
            lineDic.Add(LineCount++, "");
        }

        lineDic[9] = Page > 0 ? "-1. 이전 페이지" : "";
        lineDic[10] = Page < MaxPage - 1 ? "-2. 다음 페이지" : "";

        SelectAndRunAction(SubActionMap, isViewSubMap, () => UiManager.UIUpdater(UIName.Equipment, null, (5, lineDic)));
    }

    protected override PagedListActionBase CreateNew(int newPage)
    {
        return new EquipItemManagementAction(PrevAction, newPage);
    }
}