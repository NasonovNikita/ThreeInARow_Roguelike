using Battle;

namespace DevTools.DebugLogging.LoggingGroups.Battle
{
    public class BattleFlowLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {
            
            BattleFlowManager.Instance.OnBattleLose += () => CheckAndWrite("Lost battle");
            BattleFlowManager.Instance.OnBattleWin += () => CheckAndWrite("Won battle");
            BattleFlowManager.Instance.OnBattleEnd += () => CheckAndWrite("Battle ended");
            BattleFlowManager.Instance.OnEnemiesShuffle += () => CheckAndWrite("Enemies shuffled");
            BattleFlowManager.Instance.OnEnemiesTurnStart += () => CheckAndWrite("Enemies' turn started");
            BattleFlowManager.Instance.OnBattleStart += () => CheckAndWrite("Battle started");
            BattleFlowManager.Instance.OnCycleEnd += () => CheckAndWrite("Cycle ended");
            BattleFlowManager.Instance.OnPlayerTurnStart += () => CheckAndWrite("Player's turn started");
        }

        public override void UnAttach()
        {
            // ignored
        }
    }
}