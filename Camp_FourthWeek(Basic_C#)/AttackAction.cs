using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackAction : AttackActionBase
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

                NextAction = new ResultAction(true, new MainMenuAction());
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