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
            int LineCount = 2;

            Dictionary<int, string> lineDic = new Dictionary<int, string>();

            if (isWin)
            {
                int getExp = EnterBattleAction.MonsterSelectList.Count * 10;
                int curExp = PlayerInfo.Monster.Exp;
                int curLv = PlayerInfo.Monster.Lv;
                string curName = PlayerInfo.Monster.Name;
                PlayerInfo.Monster.Exp += getExp;
                lineDic.Add(LineCount++, "[ Victory ] ");
                lineDic.Add(LineCount++, $"풀숲에서 포켓몬을 {EnterBattleAction.MonsterSelectList.Count}마리 잡았다.");
                lineDic.Add(LineCount++, "[캐릭터 정보]");
                lineDic.Add(LineCount++,
                    $"Lv.{curLv}  {curName} {(curLv == PlayerInfo.Monster.Lv ? "" : $"-> Lv.{PlayerInfo.Monster.Lv} {PlayerInfo.Monster.Name}")}");
                lineDic.Add(LineCount++, $"HP {PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}");
                lineDic.Add(LineCount++, $"exp {curExp} -> {PlayerInfo.Monster.Exp}");
            }
            else
            {
                lineDic.Add(LineCount++, "[ Defeat ] ");
                lineDic.Add(LineCount++, "눈 앞이 깜깜해 졌다!");
                lineDic.Add(LineCount++, "서둘러서 포켓몬을 치료해야 한다.");
            }

            for (int i = LineCount; i < 18; i++)
            {
                lineDic.Add(i, "");
            }


            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_Result, null, (20, lineDic)));
        }
    }
}