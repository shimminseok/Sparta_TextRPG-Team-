namespace Camp_FourthWeek_Basic_C__;

class MonsterChangeAction : ActionBase
{
    public override string Name => "포켓몬 관리";

    public MonsterChangeAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        MonsterTestUtility.AddTestMonster();
        Console.WriteLine("이곳에서 포켓몬을 교체 할 수 있습니다.");
        Console.WriteLine();
        Console.WriteLine("[포켓몬 목록]");

        var player = GameManager.Instance.PlayerInfo;

        int index = 1;

        foreach (var m in player.Monsters)
        {
            var isEquipped = (m == player.Monster) ? "[E]" : " ";
            var name = m.Name;
            var level = m.Lv;
            var hp = m.Stats[StatType.CurHp].FinalValue;
            var mp = m.Stats[StatType.CurMp].FinalValue;
            var itemName = m.ItemId != 0 ? ItemTable.GetItemById(m.ItemId)?.Name ?? "알수 없음" : "없음";
            Console.WriteLine($"{isEquipped}{name,-10} | LV {level} | HP {hp} / MP {mp} | 장착 중인 도구: {itemName} |");

            if (!SubActionMap.ContainsKey(index))
            {
                SubActionMap[index] = new ChangeMonsterSelectAction(m, this);
            }

            index++;
        }
        SelectAndRunAction(SubActionMap,true);
    }
}

//테스트용 
public static class MonsterTestUtility
{
    public static void AddTestMonster()
    {
        var player = GameManager.Instance.PlayerInfo;

        // 예시 몬스터 타입
        var newType = MonsterType.Pidgey;

        // 이미 보유 중인 경우 중복 방지
        if (player.Monsters.Any(m => m.Type == newType))
        {
            return;
        }

        if (MonsterTable.MonsterDataDic.TryGetValue(newType, out var monster))
        {
            player.Monsters.Add(monster);
        }
    }
}