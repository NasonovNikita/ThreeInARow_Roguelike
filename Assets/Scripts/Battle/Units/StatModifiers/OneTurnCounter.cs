using Other;

namespace Battle.Units.StatModifiers
{
    public abstract class OneTurnCounter : UnitModifier
    {
        protected readonly Counter Counter;
        
        protected override bool HiddenEndedWork => Counter.EndedWork;

        protected OneTurnCounter(int count, bool isSaved = false) : base(isSaved)
        {
            Counter = CreateChangeableSubSystem(new Counter(count));
            
            BattleFlowManager.Instance.OnCycleEnd += OnCycleEnd;
            return;
            void OnCycleEnd()
            {
                Counter.Kill();
                Kill();

                BattleFlowManager.Instance.OnCycleEnd -= OnCycleEnd;
            }
        }
    }
}