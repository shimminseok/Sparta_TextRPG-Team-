namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EquipItemManagementAction : ActionBase
{
    public EquipItemManagementAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "도구관리";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        Console.WriteLine("보유 중인 도구를 장착시킬 수 있습니다.");
        Console.WriteLine("[도구 목록]");
        for (var i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = new StringBuilder();
            sb.Append($"{PadRightWithKorean($"- {i + 1}", 5)}");
            if (item.IsEquipment)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                sb.Append("[E]");
            }

            sb.Append($"{PadRightWithKorean($"{item.Name}", 18)}");
            for (var j = 0; j < item.Stats.Count; j++)
                sb.Append(
                    $" | {PadRightWithKorean($"{item.Stats[j].GetStatName()} +{item.Stats[j].FinalValue}", 10)} ");
            sb.Append(" | ");
            sb.Append($"{PadRightWithKorean($"{item.Description}", 50)}");


            Console.WriteLine(sb.ToString());
            Console.ResetColor();

            if (!SubActionMap.ContainsKey(i + 1))
                SubActionMap[i + 1] = new EquipAction(InventoryManager.Instance.Inventory[i], this);
        }

        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }
}