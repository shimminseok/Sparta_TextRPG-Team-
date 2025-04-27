namespace Camp_FourthWeek_Basic_C__;

public class EnterStageAction : ActionBase
{
    public EnterStageAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "지역 이동";

    public override void OnExcute()
    {
        //체력이 0일떄 돌아가게끔 함
        if (PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue <= 0)
        {
            NextAction = PrevAction;
        }

        SubActionMap.Clear();
        int stageCount = 6;
        Dictionary<int, string> lineDic = new Dictionary<int, string>();

        for (var i = 0; i <= StageManager.Instance.ClearStage; i++)
        {
            //StageAction이 아닌 EnterBattleAction 이걸 추가
            var stage = StageTable.GetDungeonById(i + 1);

            var battleAction = new EnterBattleAction(stage, this);
            SubActionMap.Add(i + 1, battleAction);

            lineDic.Add(stageCount++, $"{i + 1} : {battleAction.Name}");
        }

        SelectAndRunAction(SubActionMap, false, () => UiManager.UIUpdater(UIName.Location, null, (10, lineDic)));
    }
}

public class EnterBattleAction : ActionBase
{
    public override string Name => $"{currentStage.StageName}";
    private Stage currentStage;


    public EnterBattleAction(Stage _currentStage, IAction _prevAction)
    {
        PrevAction = _prevAction;
        currentStage = _currentStage;
        AttackActionBase.InitializeBattle(currentStage);
        SubActionMap = new Dictionary<int, IAction>
        {
            { 1, new AttackSelectAction(this, null) },
            { 2, new SkillSelectAction(this) },
            { 3, new CatchSelectAction(this) }
        };
    }


    public override void OnExcute()
    {
        StageManager.Instance.CurrentStage = currentStage;
        AttackActionBase.battleLogDic.Clear();
        AttackActionBase.uiPivotDic.Clear();
        AttackActionBase.monsterIconList.Clear();

        AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
        };

        AttackActionBase.BattlePlayerInfo();
        AttackActionBase.DisplayMonsterList();

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(
                UIName.Battle,
                AttackActionBase.uiPivotDic,
                (18, AttackActionBase.battleLogDic),
                AttackActionBase.monsterIconList));
    }
}

public class AttackSelectAction : ActionBase
{
    private Skill? skill;

    public override string Name =>
        skill is null ? "공격하기" : $"{skill.Name} - MP {skill.Stats[StatType.CurMp].FinalValue}\n{skill.Description}";

    public AttackSelectAction(IAction _prevAction, Skill? _skill)
    {
        PrevAction = _prevAction;
        skill = _skill;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        AttackActionBase.battleLogDic.Clear();
        AttackActionBase.uiPivotDic.Clear();

        AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
        };

        AttackActionBase.BattlePlayerInfo();
        AttackActionBase.DisplayMonsterList();

        var aliveMonsterList = AttackActionBase.battleMonsters
            .Where(m => AttackActionBase.monsterStates[m] == MonsterState.Normal)
            .ToList();
        int pivotCount = AttackActionBase.uiPivotDic.Last().Key + 1;
        int playerActionLine = AttackActionBase.battleLogDic.Last().Key + 2;
        if (skill is not null)
        {
            if (skill is not null)
            {
                AttackActionBase.battleLogDic[18] = "[ 스킬 대상 선택 ]";
                if (skill.SkillAttackType == SkillAttackType.Select)
                {
                    for (int i = 0; i < aliveMonsterList.Count; i++)
                    {
                        AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
                        AttackActionBase.battleLogDic[playerActionLine++] =
                            $"- {i + 1} : {aliveMonsterList[i].Name}";

                        SubActionMap.Add(i + 1,
                            new AttackAction(new List<Monster> { aliveMonsterList[i] }, skill, PrevAction));
                    }
                }
                else if (skill.SkillAttackType == SkillAttackType.Random)
                {
                    int count = skill.TargetCount;
                    Random rand = new Random(DateTime.Now.Millisecond);
                    var randomTargets = aliveMonsterList.OrderBy(_ => rand.Next()).Take(count).ToList();
                    NextAction = new AttackAction(randomTargets, skill, PrevAction);
                }
                else if (skill.SkillAttackType == SkillAttackType.All)
                {
                    NextAction = new AttackAction(aliveMonsterList, skill, PrevAction);
                }
            }
        }
        else
        {
            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
                AttackActionBase.battleLogDic[playerActionLine++] = $"- {i + 1} : {aliveMonsterList[i].Name}";

                SubActionMap.Add(i + 1,
                    new AttackAction(new List<Monster> { aliveMonsterList[i] }, skill, PrevAction));
            }

            for (int i = playerActionLine; i < 22; i++)
            {
                AttackActionBase.battleLogDic[i] = "";
            }
        }

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                AttackActionBase.uiPivotDic,
                (18, AttackActionBase.battleLogDic),
                AttackActionBase.monsterIconList));
    }

    public class AttackAction : AttackActionBase
    {
        private List<Monster> targets;
        private Skill? skill;
        private UIName uiname = UIName.Battle_AttackEnemy;
        private int num = 25;

        public override string Name => $"Lv. {targets[0].Lv} {targets[0].Name} 공격";


        public AttackAction(List<Monster> _monsters, Skill? _skill, IAction _prevAction)
        {
            targets = _monsters;
            skill = _skill;
            PrevAction = _prevAction;
        }


        public override void OnExcute()
        {
            SubActionMap.Clear();
            Random rand = new Random(DateTime.Now.Millisecond);

            if (skill != null)
                UseMp();

            battleLogDic.Clear();
            uiPivotDic.Clear();
            monsterIconList.Clear();

            BattlePlayerInfo();
            int attackLine = 24;

            var player = PlayerInfo.Monster;

            battleLogDic[attackLine++] = $"{GameManager.Instance.PlayerInfo.Monster.Name}의 공격!";
            foreach (var target in targets)
            {
                var (isEvade, isCritical) = CalculateBattleChances(player, target);
                float baseDamage = player.Stats[StatType.Attack].FinalValue;

                if (skill != null)
                    baseDamage *= skill.Stats[StatType.Attack].FinalValue;

                float damage = GetCalculatedDamage(baseDamage);
                float originHp = target.Stats[StatType.CurHp].FinalValue;


                if (!isEvade)
                    ApplyDamage(target, damage, isCritical);

                bool isDead = monsterStates[target] == MonsterState.Dead;

                if (isEvade)
                {
                    battleLogDic[attackLine++] = $"Lv.{target.Lv} {target.Name}은(는) 맞지 않았다.";
                }
                else
                {
                    if (isCritical)
                    {
                        battleLogDic[attackLine++] = $"Lv.{target.Lv} {target.Name}은(는) 급소에 맞았다! [데미지 : {damage}]";
                    }
                    else
                    {
                        battleLogDic[attackLine++] = $"Lv.{target.Lv} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]";
                    }
                }

                battleLogDic[attackLine++] = "";
            }

            for (int i = attackLine; i < 40; i++)
            {
                battleLogDic[i] = "";
            }


            uiPivotDic = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };
            int pivotCount = 2;
            for (int i = 0; i < pivotArr.Length && i < battleMonsters.Count; i++)
            {
                uiPivotDic.Add(pivotCount++, pivotArr[i]);
            }

            monsterIconList.Add(28); // UI 하단 커서

            uiPivotDic.Add(pivotCount, new Tuple<int, int>(0, 0));
            monsterIconList.Add(28);
            CheckBattleEnd();


            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiname, uiPivotDic, (num, battleLogDic),
                    monsterIconList));
        }


        private void CheckBattleEnd()
        {
            bool isAllMonstersDead = !battleMonsters.Any(m => monsterStates[m] == MonsterState.Normal);

            if (isAllMonstersDead)
            {
                uiname = UIName.Battle_Result;
                uiPivotDic = new Dictionary<int, Tuple<int, int>>();
                num = 20;
                monsterIconList = new List<int>();

                NextAction = new ResultAction(true, new ActionMainMenu());
                return;
            }

            SubActionMap[1] = new EnemyAttackAction(PrevAction);
        }

        public void UseMp()
        {
            float playerMp = PlayerInfo.Monster.Stats[StatType.CurMp].FinalValue;
            float skillMp = skill.Stats[StatType.CurMp].FinalValue;
            if (playerMp < skillMp)
            {
                PrevAction?.SetFeedBackMessage("Mp가 부족합니다");
                PrevAction?.Execute();
            }

            PlayerInfo.Monster.Stats[StatType.CurMp].ModifyAllValue(skillMp);
        }
    }
}

public class EnemyAttackAction : AttackActionBase
{
    public override string Name => "적 턴";
    private UIName uiname = UIName.Battle_AttackEnemy;
    private int num = 25;

    public EnemyAttackAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        battleLogDic.Clear();
        uiPivotDic.Clear();
        monsterIconList.Clear();

        BattlePlayerInfo();
        var player = PlayerInfo.Monster;

        var aliveMonsters = battleMonsters.Where(m => monsterStates[m] == MonsterState.Normal).ToList();

        int attackLine = 24;
        bool isPlayerDead = false;

        foreach (var monster in aliveMonsters)
        {
            battleLogDic[attackLine++] = $"Lv.{monster.Lv} {monster.Name}의 공격!";

            var (isEvade, isCritical) = CalculateBattleChances(monster, player);

            float baseDamage = monster.Stats[StatType.Attack].FinalValue;
            float damage = GetCalculatedDamage(baseDamage);

            if (!isEvade)
                ApplyDamage(player, damage, isCritical);

            float afterHp = player.Stats[StatType.CurHp].FinalValue;
            isPlayerDead = afterHp <= 0;

            if (isEvade)
            {
                battleLogDic[attackLine++] = $"그러나 맞지 않았다.";
            }
            else
            {
                if (isCritical)
                    battleLogDic[attackLine++] = $"급소에 맞았다!";

                battleLogDic[attackLine++] = $"{player.Name}을(를) 맞췄습니다. [데미지 : {damage}]";
                battleLogDic[attackLine++] = $"{afterHp} {(isPlayerDead ? "Dead" : "")}";
            }

            battleLogDic[attackLine++] = "";

            if (isPlayerDead)
                break;
        }


        for (int i = attackLine; i < 40; i++)
        {
            battleLogDic[i] = "";
        }

        uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) } // 플레이어 포켓몬
        };
        int pivotCount = 2;
        for (int i = 0; i < pivotArr.Length && i < battleMonsters.Count; i++)
        {
            uiPivotDic.Add(pivotCount++, pivotArr[i]);
        }

        monsterIconList.Add(28); // 하단 커서

        CheckBattleEnd(isPlayerDead);

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(uiname, uiPivotDic, (num, battleLogDic), monsterIconList));
    }

    private void CheckBattleEnd(bool isPlayerDead)
    {
        if (isPlayerDead)
        {
            uiname = UIName.Battle_Result;
            num = 20;
            uiPivotDic.Clear();
            monsterIconList.Clear();
            battleMonsters.Clear();
            NextAction = new ResultAction(false, new ActionMainMenu());
            return;
        }

        SubActionMap[1] = PrevAction;
    }
}

public class ResultAction : ActionBase
{
    public override string Name => "결과";
    private bool isWin;

    public ResultAction(bool _isWin, IAction _prevAction)
    {
        PrevAction = _prevAction;
        isWin = _isWin;
    }

    public override void OnExcute()
    {
        SubActionMap.Clear();
        AttackActionBase.battleLogDic.Clear();
        AttackActionBase.uiPivotDic.Clear();
        AttackActionBase.monsterIconList.Clear();

        int line = 2;
        if (isWin)
        {
            int getExp = AttackActionBase.battleMonsters.Count * 10;
            int beforeExp = GameManager.Instance.PlayerInfo.Monster.Exp;
            int beforeLv = GameManager.Instance.PlayerInfo.Monster.Lv;
            string beforeName = GameManager.Instance.PlayerInfo.Monster.Name;

            GameManager.Instance.PlayerInfo.Monster.Exp += getExp;

            AttackActionBase.battleLogDic[line++] = "[ Victory ]";
            AttackActionBase.battleLogDic[line++] = $"풀숲에서 포켓몬을 {AttackActionBase.battleMonsters.Count}마리 잡았다.";
            AttackActionBase.battleLogDic[line++] = "[ 캐릭터 정보 ]";
            AttackActionBase.battleLogDic[line++] =
                $"Lv.{beforeLv} {beforeName}" +
                $"{(beforeLv == GameManager.Instance.PlayerInfo.Monster.Lv ? "" : $" -> Lv.{GameManager.Instance.PlayerInfo.Monster.Lv} {GameManager.Instance.PlayerInfo.Monster.Name}")}";
            AttackActionBase.battleLogDic[line++] =
                $"HP {GameManager.Instance.PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}";
            AttackActionBase.battleLogDic[line++] =
                $"EXP {beforeExp} -> {GameManager.Instance.PlayerInfo.Monster.Exp}";

            StageManager.Instance.ClearCurrentStage();
        }
        else
        {
            AttackActionBase.battleLogDic[line++] = "[ Defeat ]";
            AttackActionBase.battleLogDic[line++] = "눈 앞이 깜깜해 졌다!";
            AttackActionBase.battleLogDic[line++] = "서둘러서 포켓몬을 치료해야 한다.";
        }

        for (int i = line; i < 18; i++)
        {
            AttackActionBase.battleLogDic[i] = "";
        }

        SubActionMap[1] = new ActionMainMenu();

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Battle_Result,
                null,
                (20, AttackActionBase.battleLogDic)));
    }
}

public class SkillSelectAction : ActionBase
{
    private List<Skill> skills = new();

    public SkillSelectAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => $"스킬 선택";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        AttackActionBase.battleLogDic.Clear();
        AttackActionBase.uiPivotDic.Clear();
        AttackActionBase.monsterIconList.Clear();

        AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
        };

        // 플레이어 정보 출력
        AttackActionBase.BattlePlayerInfo();

        skills = SkillTable.SkillDataDic.Values
            .Where(skill => GameManager.Instance.PlayerInfo.Skills.Contains(skill.Id))
            .ToList();

        int skillInfoLine = 24;

        AttackActionBase.battleLogDic[18] = "";
        int playerActionLog = AttackActionBase.battleLogDic.Last().Key + 1;

        foreach (var skill in skills)
        {
            AttackActionBase.battleLogDic[skillInfoLine++] = $"- {skill.Id} : {skill.Name}";
            AttackActionBase.battleLogDic[skillInfoLine++] = skill.Description;
            AttackActionBase.battleLogDic[skillInfoLine++] = "";
        }

        for (int i = skillInfoLine; i < 40; i++)
        {
            AttackActionBase.battleLogDic[i] = "";
        }

        for (int i = 0; i < skills.Count; i++)
        {
            if (!SubActionMap.ContainsKey(i + 1))
                SubActionMap.Add(i + 1, new AttackSelectAction(PrevAction, skills[i]));
        }

        for (int i = playerActionLog; i < 22; i++)
        {
            AttackActionBase.battleLogDic.Add(i, "");
        }

        AttackActionBase.monsterIconList.Add(28); // 하단 커서 추가

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                AttackActionBase.uiPivotDic,
                (18, AttackActionBase.battleLogDic),
                AttackActionBase.monsterIconList));
    }
}

public class CatchSelectAction : ActionBase
{
    public CatchSelectAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }

    public override string Name => "포획하기";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        AttackActionBase.battleLogDic.Clear();
        AttackActionBase.uiPivotDic.Clear();

        AttackActionBase.uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
        };

        // 플레이어 정보 출력
        AttackActionBase.BattlePlayerInfo();

        var aliveMonsterList = AttackActionBase.battleMonsters
            .Where(mon => AttackActionBase.monsterStates[mon] == MonsterState.Normal)
            .ToList();

        int pivotCount = AttackActionBase.uiPivotDic.Count;
        int playerActionLine = 19;

        AttackActionBase.battleLogDic[18] = "[ 포획 대상 지정 ]";

        for (int i = 0; i < aliveMonsterList.Count; i++)
        {
            AttackActionBase.uiPivotDic.Add(pivotCount++, AttackActionBase.pivotArr[i]);
            AttackActionBase.battleLogDic[playerActionLine++] = $"- {i + 1} : {aliveMonsterList[i].Name}";

            if (!SubActionMap.ContainsKey(i + 1))
                SubActionMap.Add(i + 1, new CatchAction(aliveMonsterList[i], PrevAction));
        }

        for (int i = playerActionLine; i < 22; i++)
        {
            AttackActionBase.battleLogDic[i] = "";
        }

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(UIName.Battle_AttackSelect,
                AttackActionBase.uiPivotDic,
                (18, AttackActionBase.battleLogDic),
                AttackActionBase.monsterIconList));
    }
}

public class CatchAction : AttackActionBase
{
    private Monster targetMonster;
    private int num = 25;
    private UIName uiName = UIName.Battle_AttackEnemy;

    public CatchAction(Monster _targetMonster, IAction _prevAction)
    {
        PrevAction = _prevAction;
        targetMonster = _targetMonster;
    }

    public override string Name => $"Lv.{targetMonster.Lv} {targetMonster.Name} 포획";

    public override void OnExcute()
    {
        SubActionMap.Clear();
        battleLogDic.Clear();
        uiPivotDic.Clear();
        monsterIconList.Clear();

        BattlePlayerInfo();

        TryCatchMonster();

        SelectAndRunAction(SubActionMap, false,
            () => UiManager.UIUpdater(uiName, uiPivotDic, (num, battleLogDic), monsterIconList));
    }

    private void TryCatchMonster()
    {
        float basicCatchChance = 0.3f;
        float maximumCatchChance = 0.9f;
        Random rand = new Random(DateTime.Now.Millisecond);

        float hpRatio = targetMonster.Stats[StatType.CurHp].FinalValue /
                        targetMonster.Stats[StatType.MaxHp].FinalValue;
        float catchChance = maximumCatchChance - (maximumCatchChance - basicCatchChance) * hpRatio;

        int line = 24;

        if (rand.NextDouble() < catchChance)
        {
            QuestManager.Instance.UpdateCurrentCount((QuestTargetType.Monster, QuestConditionType.Catch),
                (int)targetMonster.Type);

            InventoryManager.Instance.AddMonsterToBox(targetMonster);
            monsterStates[targetMonster] = MonsterState.Catched;

            battleLogDic[line++] = $"Lv.{targetMonster.Lv} {targetMonster.Name}을(를) 포획했습니다!";
        }
        else
        {
            battleLogDic[line++] = $"Lv.{targetMonster.Lv} {targetMonster.Name} 포획 실패...";
        }

        for (int i = line; i < 40; i++)
        {
            battleLogDic[i] = "";
        }

        uiPivotDic = new Dictionary<int, Tuple<int, int>?>
        {
            { 0, new Tuple<int, int>(0, 0) }, // 배경
            { 1, new Tuple<int, int>(7, 28) } // 내 포켓몬
        };
        int pivotCount = 2;
        for (int i = 0; i < pivotArr.Length && i < battleMonsters.Count; i++)
        {
            uiPivotDic.Add(pivotCount++, pivotArr[i]);
        }

        monsterIconList.Add(28);

        SetNextAction();
    }

    private void SetNextAction()
    {
        bool isAllEnemiesDead = !battleMonsters.Any(m => monsterStates[m] == MonsterState.Normal);

        if (isAllEnemiesDead)
        {
            uiName = UIName.Battle_Result;
            num = 20;
            uiPivotDic.Clear();
            monsterIconList.Clear();
            battleLogDic.Clear();
            NextAction = new ResultAction(true, new ActionMainMenu());
        }
        else
        {
            SubActionMap[1] = new EnemyAttackAction(PrevAction);
        }
    }
}