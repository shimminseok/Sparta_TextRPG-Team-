using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__;
class MonsterChangeAction : ActionBase
{
    public override string Name => "포켓몬 관리";
    public MonsterChangeAction(IAction _prevAction)
    {
        PrevAction = _prevAction;
    }
    public override void OnExcute()
    {
        Console.WriteLine();
        Console.WriteLine("[포켓몬 목록]");
        
        
        SelectAndRunAction(SubActionMap);
    }
}
