using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camp_FourthWeek_Basic_C__
{
    public class EnterBattleBasic : ActionBase
    {
        public EnterBattleBasic(IAction _prevAction)
        {
            PrevAction = _prevAction;
        }

        public override string Name => "전투 시작";

        public override void OnExcute()
        {
            //몬스터 1~4마리 랜덤 등장
            //몬스터 정보 - LV 이름 HP ATK

            //1. Battle!! -> 출력
            // 몬스터 정보 출력

            //2. [내정보] -> 출력
            //Ex) Lv.1 Chad (전사)
            //Ex) HP 100/100

            //3. 1. 공격
            // 원하시는 행동을 입력해주세요. >>
        }

    }
}
