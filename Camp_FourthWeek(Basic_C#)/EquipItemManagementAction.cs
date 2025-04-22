namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EquipItemManagementAction : ActionBase
{
    private readonly PlayerInfo player;

    public EquipItemManagementAction(IAction _prevAction, PlayerInfo _player, InventoryManager _inventory)
    {
        PrevAction = _prevAction;
        player = _player;
    }

    public override string Name => "도구관리";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        Console.WriteLine("보유 중인 도구를 장착시킬 수 있습니다.");

        Console.WriteLine("[장착 중인 도구]");
        Console.WriteLine();


        Console.WriteLine("[보유 도구 목록]");

        var currentMonster = player.Monster;

        for (var i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            var item = InventoryManager.Instance.Inventory[i];
            var sb = new StringBuilder();
            sb.Append($"{PadRightWithKorean($"- {i + 1}", 5)}");
            
            
            if (item.IsEquippedBy(currentMonster))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                sb.Append("[E]");
            }

            sb.Append($"{PadRightWithKorean($"{item.Name}", 18)}");
            for (var j = 0; j < item.Stats.Count; j++)
                sb.Append(
                    $" | {PadRightWithKorean($"{item.Stats[j].GetStatName()} +{item.Stats[j].FinalValue}", 10)} ");
            sb.Append(" | ");
            var monster = player.Monsters.FirstOrDefault(m => m.ItemId == item.Key);
            string equippedMonsterName = string.Empty;

            if (item.IsEquipment)
            {
                if (monster != null)
                    sb.Append($"{PadRightWithKorean($"장착 중인 포켓몬: {monster.Name}", 50)}");
            }

            sb.Append($"{PadRightWithKorean($"{item.Description}{equippedMonsterName}", 50)}");

            Console.WriteLine(sb.ToString());
            Console.ResetColor();

            if (!SubActionMap.ContainsKey(i + 1))
            {
                bool equippedByOther = monster != null && monster != currentMonster;
                if (!equippedByOther)
                    SubActionMap[i + 1] = new EquipAction(item, this);
            }
        }

        Console.WriteLine();
        SelectAndRunAction(SubActionMap);
    }
}