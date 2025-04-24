namespace Camp_FourthWeek_Basic_C__;

using System.Text;
using static Camp_FourthWeek_Basic_C__.StringUtil;

public class EquipItemManagementAction : ActionBase
{
    private readonly PlayerInfo player;
    private int curItemPage = 0;
    private const int pageItemSize = 3;

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


        var inventory = InventoryManager.Instance.Inventory;
        var currentMonster = player.Monster;

        int total = inventory.Count;
        var maxPage= (int)Math.Ceiling(total/(float)pageItemSize);
        int start = curItemPage * pageItemSize;
        int end = Math.Min(start + pageItemSize, total);
        Console.WriteLine($"[보유 도구 목록] ({curItemPage + 1}/{maxPage})");


        for (var i = start; i < end; i++)
        {
            var item = inventory[i];
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

            var monster = InventoryManager.Instance.MonsterBox.FirstOrDefault(m => m.ItemId == item.Key);
            string equippedMonsterName = string.Empty;

            if (item.IsEquipment)
            {
                if (monster != null)
                    sb.Append($"{PadRightWithKorean($"장착 중인 포켓몬: {monster.Name}", 50)}");
            }

            sb.Append($"{PadRightWithKorean($"{item.Description}{equippedMonsterName}", 50)}");

            Console.WriteLine(sb.ToString());
            Console.ResetColor();

            if (!SubActionMap.ContainsKey(i - start + 1)) 
            {
                bool equippedByOther = monster != null && monster != currentMonster;
                if (!equippedByOther)
                    SubActionMap[i + 1] = new EquipAction(item, this);
            }
        }
        Console.WriteLine();
        Console.WriteLine("-1. 다음페이지");
        Console.WriteLine("-2. 이전페이지");
        Console.WriteLine();
        SubActionMap[-1] = new ItemPageMoveAction(this, this, 1);
        SubActionMap[-2] = new ItemPageMoveAction(this, this, -1);

        Console.WriteLine();
        SelectAndRunAction(SubActionMap,true);
    } 
    public void MovePage(int _dir)
    {
        SetFeedBackMessage("");
        var totalPage = InventoryManager.Instance.Inventory.Count;
        int maxPage = (int)Math.Ceiling(totalPage / (float)pageItemSize);
        int nextPage = curItemPage + _dir;

        if (nextPage < 0 || nextPage >= maxPage)
        {
            SetFeedBackMessage("더이상 페이지가 없습니다.");
            return;
        }

        curItemPage = nextPage;
    }
}