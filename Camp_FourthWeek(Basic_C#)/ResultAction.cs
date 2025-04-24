using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    internal class ResultAction : ActionBase
    {
        private static int maxKey;
        public static void SetmaxKey(int vaule) //포켓몬 몇마리 잡았는지 받아오기
        {
            maxKey = vaule;
        }
        public override string Name => "결과";
        private bool isWin;

        public ResultAction(bool _isWin, IAction _prevAction)
        {
            PrevAction = _prevAction;
            isWin = _isWin;
        }

        public override void OnExcute()
        {
            if (isWin)
            {
                Console.WriteLine("Victort");
                Console.WriteLine($"풀숲에서 포켓몬을 {maxKey}마리 잡았다.");

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{PlayerInfo.Monster.Lv}  {PlayerInfo.Name}");
                Console.WriteLine($"HP {PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}");
            }
            else
            {
                Console.WriteLine("Defeat");
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}