using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackAction : ActionBase
    {
        private int num = 25;

        private List<Monster> monsters;
        public override string Name => $"Lv. {monsters[0].Lv} {monsters[0].Name} 공격";
        private Skill? skill;
        static Random random = new Random();

        private UIName uiname = UIName.Battle_AttackEnemy;

        public AttackAction(List<Monster> _monsters, Skill? _skill, IAction _prevAction)
        {
            monsters = _monsters;
            PrevAction = _prevAction;
            skill = _skill;
        }


        int attackCount = 24;

        public override void OnExcute()
        {
            random = new Random(DateTime.Now.Millisecond);
            if (skill != null)
                UseMp();

            EnterBattleAction.lineDic.Clear();
            var player = PlayerInfo.Monster;


            EnterBattleAction.lineDic.Add(attackCount++, $"{player.Name}의 공격!");
            foreach (var target in monsters)
            {
                var (isEvade, isCritical) = CalculateBattleChances(player, target);

                float baseDamage = player.Stats[StatType.Attack].FinalValue;
                if (skill != null)
                    baseDamage *= skill.Stats[StatType.Attack].FinalValue;

                float damage = GetCalculatedDamage(baseDamage);
                float originHp = target.Stats[StatType.CurHp].FinalValue;
                if (isCritical)
                    damage *= player.Stats[StatType.CriticlaDamage].FinalValue;

                if (!isEvade)
                    target.Stats[StatType.CurHp].ModifyAllValue(damage);
                if (target.Stats[StatType.CurHp].FinalValue <= 0)
                    EnterBattleAction.MonsterStateDic[target] = MonsterState.Dead;
                bool isDead = EnterBattleAction.MonsterStateDic[target] == MonsterState.Dead;

                if (isEvade)
                {
                    EnterBattleAction.lineDic.Add(attackCount++, $"Lv.{target.Lv} {target.Name}은(는) 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        EnterBattleAction.lineDic.Add(attackCount++,
                            $"Lv.{target.Lv} {target.Name}은(는) 급소에 맞았다! [데미지 : {damage}]");
                    }
                    else
                    {
                        EnterBattleAction.lineDic.Add(attackCount++,
                            $"Lv.{target.Lv} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                    }
                }

                EnterBattleAction.lineDic.Add(attackCount++, $"");
            }

            for (int i = attackCount; i < 40; i++)
            {
                EnterBattleAction.lineDic.Add(i, "");
            }


            EnterBattleAction.pivotDict = new Dictionary<int, Tuple<int, int>?>
            {
                { 0, new Tuple<int, int>(0, 0) }, // 배경
                { 1, new Tuple<int, int>(7, 28) }, // 내 포켓몬
            };
            int pivotCount = 2;
            for (int i = 0; i < EnterBattleAction.monsterUIList.Count - 1 && i < EnterBattleAction.pivotArr.Length; i++)
            {
                EnterBattleAction.pivotDict.Add(pivotCount++, EnterBattleAction.pivotArr[i]);
            }

            EnterBattleAction.pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            EnterBattleAction.monsterUIList.Add(28);


            attackCount = 24;
            CheckBattleEnd();
            if (SubActionMap.Count == 0)
                return;

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(uiname, EnterBattleAction.pivotDict, (num, EnterBattleAction.lineDic),
                    EnterBattleAction.monsterUIList));
        }


        private void CheckBattleEnd()
        {
            bool isAllMonstersDead = !EnterBattleAction.GetAliveMonsters().Any();

            if (isAllMonstersDead)
            {
                uiname = UIName.Battle_Result;
                EnterBattleAction.pivotDict = new Dictionary<int, Tuple<int, int>>();
                num = 20;
                EnterBattleAction.monsterUIList = new List<int>();

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

        public static float GetCalculatedDamage(float _originDamage)
        {
            float minDamage = _originDamage * 0.9f; //공격력의 최소값
            float maxDamage = _originDamage * 1.1f; //공격력의 최대값

            float randomNum = (float)random.NextDouble() * (maxDamage - minDamage); //최소값과 최대값 사이에서 랜덤 값 구하기
            float roundedValue = (float)Math.Round(randomNum, 0); //소수점 1자리에서 반올림

            return maxDamage - roundedValue; //공격력에서 10% 오차가 생긴 데미지
        }

        public static (bool isEvade, bool isCritical) CalculateBattleChances(Monster _origin, Monster _target)
        {
            bool isEvade = random.NextDouble() < _target.Stats[StatType.EvadeChance].FinalValue * 0.01f;
            bool isCritical = random.NextDouble() < _origin.Stats[StatType.CriticalChance].FinalValue * 0.01f;
            return (isEvade, isCritical);
        }
    }
}