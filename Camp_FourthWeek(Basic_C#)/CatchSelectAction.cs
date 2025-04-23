using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    internal class CatchSelectAction : ActionBase
    {
        public CatchSelectAction(IAction _prevAction) 
        {
            PrevAction = _prevAction;   
        }

        public override string Name => "포획하기";

        public override void OnExcute()
        {
            BattleAction.ShowMonsterList(true);
            for (int i = 0; i < BattleAction.monsters.Length; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(BattleAction.monsters[i],this));
            }


            SelectAndRunAction(SubActionMap);
        }
    }
}
