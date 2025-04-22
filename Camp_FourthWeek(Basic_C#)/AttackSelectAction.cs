namespace Camp_FourthWeek_Basic_C__
{

    public class AttackSelectAction : ActionBase
    {
        private Monster[] monsters;
        public AttackSelectAction(Monster[] _monsters ,IAction _prevAction)
        {
            PrevAction = _prevAction;
            monsters = _monsters;
        }

        public override string Name => $"공격";

        public override void OnExcute()
        {
            var message = string.Empty;
            if (PlayerInfo != null)
            {
            }

            if (PrevAction != null)
            {
                PrevAction.SetFeedBackMessage(message);
                PrevAction.Execute();
            }

            SelectAndRunAction(SubActionMap);
        }
    }
}
