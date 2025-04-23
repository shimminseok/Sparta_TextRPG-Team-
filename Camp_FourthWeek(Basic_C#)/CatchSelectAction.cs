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
            EnterBattleAction.DisplayMonsterList();
            for (int i = 0; i < EnterBattleAction.MonsterSelectList.Count; i++)
            {
                if (!SubActionMap.ContainsKey(i + 1))
                    SubActionMap.Add(i + 1, new CatchAction(EnterBattleAction.MonsterSelectList[i],this));
            }


            SelectAndRunAction(SubActionMap);
        }
    }
}
