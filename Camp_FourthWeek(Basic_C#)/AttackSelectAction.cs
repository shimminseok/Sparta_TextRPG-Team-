using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    internal class AttackSelectAction : ActionBase
    {
        public override string Name => "공격하기";

        public override void OnExcute()
        {
            SubActionMap.Clear();
            EnterBattleAction.DisplayMonsterList();

            for(int i = 0; i < EnterBattleAction.MonsterSelectList.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                {
                    SubActionMap.Add(i + 1, new AttackAction(EnterBattleAction.MonsterSelectList[i], PrevAction));
                }
            }
            SelectAndRunAction(SubActionMap);
        }

        public AttackSelectAction(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }
    }
}
