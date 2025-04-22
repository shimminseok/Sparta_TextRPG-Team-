namespace Camp_FourthWeek_Basic_C__;


public class MonsterBoxAction : ActionBase
{
    public override string Name => "포켓몬 박스";
    public MonsterBoxAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new MonsterChangeAction(this) }
        };
    }

    public override void OnExcute()
    {
        Console.WriteLine("포켓몬을 관리 할 수 있습니다.");
        Console.WriteLine("\n[포켓몬 목록]");
        
        var player = GameManager.Instance.PlayerInfo;

        foreach (var m in player.Monsters)
        {
            var isEquipped = (m == player.Monster) ? "[E]" : " ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.ItemId != 0 ? ItemTable.GetItemById(m.ItemId)?.Name ?? "알수 없음" : "없음";

            Console.WriteLine($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");

        }
        
        
        SelectAndRunAction(SubActionMap);
    }
}
