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
                int getExp = EnterBattleAction.MonsterSelectList.Count * 10;
                int curExp = PlayerInfo.Monster.Exp;
                int curLv = PlayerInfo.Monster.Lv;
                string curName = PlayerInfo.Monster.Name;
                PlayerInfo.Monster.Exp += getExp;
                Console.WriteLine("Victory");
                Console.WriteLine($"풀숲에서 포켓몬을 {EnterBattleAction.MonsterSelectList.Count}마리 잡았다.");
                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv.{curLv}  {curName} {(curLv==PlayerInfo.Monster.Lv ? "" : $"-> Lv.{PlayerInfo.Monster.Lv} {PlayerInfo.Monster.Name}")}");
                Console.WriteLine($"HP {PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}");
                Console.WriteLine($"exp {curExp} -> {PlayerInfo.Monster.Exp}\n");

                Console.WriteLine("[획득 아이템]");
                int rewardGold = EnterBattleAction.CurrentStage.RewardGold;
                PlayerInfo.Gold += rewardGold;
                Console.WriteLine($"{rewardGold} Gold");
                foreach (Monster mon in EnterBattleAction.MonsterSelectList)
                {
                    if (EnterBattleAction.MonsterStateDic[mon] == MonsterState.Catched)
                    {
                        Console.WriteLine($"{mon.Name}(포켓몬)");
                    }
                }
            }
            else
            {
                Console.WriteLine("Defeat");
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}