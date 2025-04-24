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
        private float playerCurHpStatus; //플레이어의 최종 HP를 가져오기 위해

        public ResultAction(bool _isWin, IAction _prevAction, float playerCurHpStatus)
        {
            PrevAction = _prevAction;
            isWin = _isWin;
            this.playerCurHpStatus = playerCurHpStatus;
        }

        public override void OnExcute()
        {
            if (isWin)
            {
                Console.WriteLine("Victort");
                Console.WriteLine($"풀숲에서 포켓몬을 {maxKey}마리 잡았다.");

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{PlayerInfo.Monster.Lv}  {PlayerInfo.Name}");
                Console.WriteLine($"HP {PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue} -> {playerCurHpStatus}"); //이거 수정=====================================================================================
            }
            else
            {
                Console.WriteLine("Defeat");
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}

/*Victory

풀숲에서 포켓몬을 3마리 잡았습니다

[캐릭터 정보]
Lv .1 피츄->@@@@ Lv2.피카츄
HP 100 -> 74 @@@@@
exp 5 -> 13

[획득 아이템]
500 Gold
구구(포켓몬) - 1
날카로운 발톱(도) -1*/