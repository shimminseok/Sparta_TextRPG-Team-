using System.Threading;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackAction : ActionBase
    {
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
        public AttackAction(Monster monster, Skill? _skill, IAction _prevAction)
            : this(new List<Monster> { monster }, _skill, _prevAction)
        {
        }

        public override void OnExcute()
        {
            random = new Random(DateTime.Now.Millisecond);
            if (skill != null)
                UseMp();

            var player = PlayerInfo.Monster;
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
                Console.WriteLine($"{player.Name}의 공격!");

                if (isEvade)
                {
                    Console.WriteLine("그러나 맞지 않았다.");
                }
                else
                {
                    if (isCritical)
                    {
                        Console.WriteLine($"급소에 맞았다!");
                    }

                    Console.WriteLine($"Lv.{target.Lv} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]\n");
                    Console.WriteLine($"Lv.{target.Lv} {target.Name}");


                }
                Console.WriteLine($"HP {originHp} -> {(isDead ? "Dead" : target.Stats[StatType.CurHp].FinalValue.ToString())}");
            }
            InputNumber();

            Console.Clear();

            CheckBattleEnd();

            new EnemyAttackAction(PrevAction).Execute();
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
            Console.WriteLine("1. 다음");
            Console.Write(">> ");
            while (true)
            {
                string input = Console.ReadLine();

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
