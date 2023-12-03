namespace Battle.BattleEventHandlers
{
    public class BattleEvent
    {
        protected virtual void Handle(ILog log) {}

        protected BattleEvent()
        {
            ILog.OnLog += Handle;
        }
    }
}