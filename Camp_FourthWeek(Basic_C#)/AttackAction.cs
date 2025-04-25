using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackAction : ActionBase
    {
        static Dictionary<int, string> lineDic;
        static Dictionary<int, Tuple<int, int>?> pivotDict;
        static List<int> monsterUIList;
        static Tuple<int, int>[] pivotArr = new Tuple<int, int>[] { new Tuple<int, int>(5, 6), new Tuple<int, int>(60, 6), new Tuple<int, int>(115, 6) };
        private List<Monster> monsters;
        public override string Name => $"Lv. {monsters[0].Lv} {monsters[0].Name} 공격";
        private Skill? skill;
        static Random random = new Random();

        public AttackAction(List<Monster> _monsters, Skill? _skill, IAction _prevAction)
        {
            PrevAction = _prevAction;
            monsters = _monsters;
            skill = _skill;
        }
        public AttackAction(Monster monster, Skill? _skill, IAction _prevAction, List<int> _monsterUIList, Dictionary<int, string> _lineDic)
            : this(new List<Monster> { monster }, _skill, _prevAction)
        {
            monsterUIList = _monsterUIList;
            lineDic = _lineDic;
        }
        int attackCount = 24;
        public override void OnExcute()
        {
            random = new Random(DateTime.Now.Millisecond);
            if (skill != null)
                UseMp();

            var player = PlayerInfo.Monster;

            if(lineDic != null)
            {
                for (int i = attackCount; i <= 40; i++)
                {
                    if (lineDic.ContainsKey(i))
                        lineDic.Remove(i);
                }
            }
         
  
            lineDic.Add(attackCount++, $"{player.Name}의 공격!");
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
                    lineDic.Add(attackCount++, $"그러나 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        lineDic.Add(attackCount++, $"급소에 맞았다!");
                        lineDic.Add(attackCount++, $" Lv.{target.Lv} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                    }
                    else
                    {
                        lineDic.Add(attackCount++, $" Lv.{target.Lv} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                    }
                }
                for(int i = attackCount; i < 40; i++)
                {
                    lineDic.Add(i, "");
                }

               // Console.WriteLine($"HP {originHp} -> {(isDead ? "Dead" : target.Stats[StatType.CurHp].FinalValue.ToString())}");
            }


            pivotDict = new Dictionary<int, Tuple<int, int>?>
                  {
                      {0, new Tuple<int, int>(0,0) }, // 배경
                      {1, new Tuple<int, int>(7,28)}, // 내 포켓몬
                  };
            int pivotCount = 2;
            for (int i =  0; i < monsterUIList.Count-1 && i < pivotArr.Length ; i++)
            {
                pivotDict.Add(pivotCount++, pivotArr[i]);
            }
            pivotDict.Add(pivotCount, new Tuple<int, int>(0, 0));
            monsterUIList.Add(28);

            InputNumber();

            CheckBattleEnd();
            attackCount = 24;
            new EnemyAttackAction(PrevAction,monsterUIList,lineDic).Execute();
        }


        private void CheckBattleEnd()
        {
            bool isAllMonstersDead = !EnterBattleAction.GetAliveMonsters().Any();

            if (isAllMonstersDead)
            {
                SubActionMap[1] = new ResultAction(true, new MainMenuAction());
                SubActionMap[1].Execute();
            }
        }

        public static void InputNumber()
        {



            while (true)
            {
                string input = UiManager.UIUpdater(UIName.Battle_AttackEnemy, pivotDict, (25, lineDic), monsterUIList);

                if (int.TryParse(input, out int number))
                {
                    if (number == 1)
                        break;
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
                else
                    Console.WriteLine("잘못된 입력입니다.");
            }
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
