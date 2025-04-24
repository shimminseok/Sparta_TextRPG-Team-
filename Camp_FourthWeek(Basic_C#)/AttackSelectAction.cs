using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackSelectAction : ActionBase
    {
        private Skill? skill;
        public override string Name =>
            skill is null ? "공격하기" : $"{skill.Name} - MP {skill.Stats[StatType.CurMp].FinalValue}\n{skill.Description}";

        public override void OnExcute()
        {
            SubActionMap.Clear();
            EnterBattleAction.DisplayMonsterList();
            List<Monster> aliveMonsterList = new List<Monster>();


            foreach (Monster mon in EnterBattleAction.MonsterSelectList)
            {
                if (EnterBattleAction.MonsterStateDic[mon] == MonsterState.Normal)
                {
                    aliveMonsterList.Add(mon);
                }
            }


            for (int i = 0; i < aliveMonsterList.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                {
                    SubActionMap.Add(i + 1, new AttackAction(aliveMonsterList[i], PrevAction));
                }
            }
            int maxKey = SubActionMap.Keys.Max(); //포켓몬 몇마리인지(최대) 받아서 ResultAction.cs에서 출력!
            ResultAction.SetmaxKey(maxKey);

            SelectAndRunAction(SubActionMap);
        }

        public AttackSelectAction(IAction _prevAction, Skill? _skill = null)
        {
            PrevAction = _prevAction;
            skill = _skill;
        }
    }
}
