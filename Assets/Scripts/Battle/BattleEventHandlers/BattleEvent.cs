namespace Battle.BattleEventHandlers
{
    public class BattleEvent
    {
        protected virtual void Handle(Log log) {}

        protected BattleEvent()
        {
            Log.OnLog += Handle;
        }
    }
}