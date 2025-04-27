namespace Camp_FourthWeek_Basic_C__
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using static Camp_FourthWeek_Basic_C__.Program;


    internal class Program
    {
        static void Main(string[] args)
        {
            IAction? currentAction = new IntroSceneAction();

            while (currentAction != null)
            {
                currentAction.Execute();
                currentAction = (currentAction as ActionBase)?.NextAction;
            }

            GameManager.Instance.SaveGame();
        }
    }

    public class TextRPG
    {
        public void StartGame()
        {
            IntroSceneAction introSceneAction = new IntroSceneAction();
            introSceneAction.Execute();
        }
    }
}