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
            SubActionMap.Clear();
            AttackActionBase.battleLogDic.Clear();
            AttackActionBase.uiPivotDic.Clear();
            AttackActionBase.monsterIconList.Clear();

            int line = 2;
            if (isWin)
            {
                int getExp = AttackActionBase.battleMonsters.Count * 10;
                int beforeExp = GameManager.Instance.PlayerInfo.Monster.Exp;
                int beforeLv = GameManager.Instance.PlayerInfo.Monster.Lv;
                string beforeName = GameManager.Instance.PlayerInfo.Monster.Name;

                GameManager.Instance.PlayerInfo.Monster.Exp += getExp;

                AttackActionBase.battleLogDic[line++] = "[ Victory ]";
                AttackActionBase.battleLogDic[line++] = $"풀숲에서 포켓몬을 {AttackActionBase.battleMonsters.Count}마리 잡았다.";
                AttackActionBase.battleLogDic[line++] = "[ 캐릭터 정보 ]";
                AttackActionBase.battleLogDic[line++] =
                    $"Lv.{beforeLv} {beforeName}" +
                    $"{(beforeLv == GameManager.Instance.PlayerInfo.Monster.Lv ? "" : $" -> Lv.{GameManager.Instance.PlayerInfo.Monster.Lv} {GameManager.Instance.PlayerInfo.Monster.Name}")}";
                AttackActionBase.battleLogDic[line++] =
                    $"HP {GameManager.Instance.PlayerInfo.Monster.Stats[StatType.CurHp].FinalValue}";
                AttackActionBase.battleLogDic[line++] =
                    $"EXP {beforeExp} -> {GameManager.Instance.PlayerInfo.Monster.Exp}";

                StageManager.Instance.ClearCurrentStage();
            }
            else
            {
                AttackActionBase.battleLogDic[line++] = "[ Defeat ]";
                AttackActionBase.battleLogDic[line++] = "눈 앞이 깜깜해 졌다!";
                AttackActionBase.battleLogDic[line++] = "서둘러서 포켓몬을 치료해야 한다.";
            }

            for (int i = line; i < 18; i++)
            {
                AttackActionBase.battleLogDic[i] = "";
            }

            SubActionMap[1] = new MainMenuAction();

            SelectAndRunAction(SubActionMap, false,
                () => UiManager.UIUpdater(UIName.Battle_Result,
                    null,
                    (20, AttackActionBase.battleLogDic)));
        }
    }
}