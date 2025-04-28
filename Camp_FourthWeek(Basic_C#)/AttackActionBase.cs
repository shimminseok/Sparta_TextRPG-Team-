namespace Camp_FourthWeek_Basic_C__;

public class AttackActionBase : ActionBase
{
    #region UI

    protected const int addId = 199;
    protected const int addMonsterId = 99;

    public static Dictionary<int, string> battleLogDic = new Dictionary<int, string>(); // UI 추가를 위한 딕셔너리
    public static Dictionary<int, Tuple<int, int>?> uiPivotDic;
    public static List<int> monsterIconList = new List<int>();

    public static Tuple<int, int>[] pivotArr = new Tuple<int, int>[]
        { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };

    #endregion


    public override string Name { get; }
    protected readonly Random random = new Random();

    public static Dictionary<Monster, MonsterState>
        monsterStates = new Dictionary<Monster, MonsterState>(); //랜덤으로 선택된 몬스터를 넣을 공간

    public static List<Monster> battleMonsters = new List<Monster>(); //랜덤으로 선택된 몬스터를 넣을 공간


    protected (bool isEvade, bool isCritical) CalculateBattleChances(Monster _attacker, Monster _target)
    {
        bool isEvade = random.NextDouble() < _target.Stats[StatType.EvadeChance].FinalValue * 0.01;
        bool isCritical = random.NextDouble() < _attacker.Stats[StatType.CriticalChance].FinalValue * 0.01;
        return (isEvade, isCritical);
    }

    protected float GetCalculatedDamage(float _baseDamage)
    {
        float minDamage = _baseDamage * 0.9f;
        float maxDamage = _baseDamage * 1.1f;

        float randomValue = (float)random.NextDouble() * (maxDamage - minDamage);
        return maxDamage - (float)Math.Round(randomValue, 0);
    }

    protected void ApplyDamage(Monster _target, float _damage, bool _isCritical)
    {
        if (_isCritical)
            _damage *= _target.Stats[StatType.CriticlaDamage].FinalValue;

        _target.Stats[StatType.CurHp].ModifyAllValue(MathF.Round(_damage));

        if (_target.Stats[StatType.CurHp].FinalValue <= 0)
        {
            monsterStates[_target] = MonsterState.Dead;
        }
    }

    public static void BattlePlayerInfo()
    {
        var player = GameManager.Instance.PlayerInfo.Monster;

        string name = player.Name;
        int level = player.Lv;
        float maxHP = player.Stats[StatType.MaxHp].FinalValue;
        float curHP = player.Stats[StatType.CurHp].FinalValue;

        float maxMP = player.Stats[StatType.MaxMp].FinalValue;
        float curMP = player.Stats[StatType.CurMp].FinalValue;

        int index = Array.IndexOf(Enum.GetValues(typeof(MonsterType)), player.Type) + addId;
        monsterIconList.Add(index);


        battleLogDic = new Dictionary<int, string>
        {
            // 플레이어 정보
            { 0, $"{level}" },
            { 1, $"{name}" },
            { 2, $"HP : {curHP}/{maxHP}" },
            { 3, StringUtil.GetBar(curHP, maxHP) },
            { 4, $"PP : {curMP}/{maxMP}" },
            { 5, StringUtil.GetBar(curMP, maxMP) },
        };
    }

    public static void DisplayMonsterList() // 몬스터 hp <= 0이면 dead 표시, 색 변경
    {
        monsterIconList.RemoveRange(1, monsterIconList.Count - 1);
        int monsterCount = 2;
        int uiCount = 6;
        foreach (Monster monster in battleMonsters) //-> 최종적으로 화면에 출력하기 위해
        {
            float maxHP = monster.Stats[StatType.MaxHp].FinalValue; //maxHP를 가져옴
            float curHP = monster.Stats[StatType.CurHp].FinalValue;

            if (monsterStates[monster] == MonsterState.Normal)
            {
                // 여기가 살아있는 포켓몬

                int index = Array.IndexOf(Enum.GetValues(typeof(MonsterType)), monster.Type) + addMonsterId;
                monsterIconList.Add(index);
                uiPivotDic.Add(monsterCount, pivotArr[monsterCount - 2]);
                battleLogDic.Add(uiCount++, $"Lv. {monster.Lv}");
                battleLogDic.Add(uiCount++, monster.Name);
                battleLogDic.Add(uiCount++, $"HP {curHP}/{maxHP}");
                battleLogDic.Add(uiCount++, StringUtil.GetBar(curHP, maxHP));
                monsterCount++;
            }
        }

        for (int i = uiCount; i < 18; i++)
        {
            battleLogDic.Add(i, "");
        }
    }

    protected void ClearBattleData()
    {
        battleLogDic.Clear();
        uiPivotDic.Clear();
        monsterIconList.Clear();
        battleMonsters.Clear();
        monsterStates.Clear();
    }

    public static void InitializeBattle(Stage _currentStage)
    {
        ClearStaticData();

        List<Monster> allMonsters = _currentStage.SpawnedMonsters
            .Select(type => MonsterTable.Instance.MonsterDataDic[type])
            .ToList();

        Random rand = new Random();
        int randomCount = rand.Next(1, 4);

        for (int i = 0; i < randomCount; i++)
        {
            Monster randomMonster = allMonsters[rand.Next(allMonsters.Count)];
            Monster copiedMonster = randomMonster.Copy();
            battleMonsters.Add(copiedMonster);
            monsterStates[copiedMonster] = MonsterState.Normal;
            CollectionManager.Instnace.OnDiscovered(randomMonster.Type);
        }
    }

    private static void ClearStaticData()
    {
        battleLogDic = new();
        uiPivotDic = new();
        monsterIconList = new();
        battleMonsters = new();
        monsterStates = new();
    }

    public override void OnExcute()
    {
    }
}