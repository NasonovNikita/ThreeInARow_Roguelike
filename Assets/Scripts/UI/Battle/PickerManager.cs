using System.Linq;
using Battle;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class PickerManager : MonoBehaviour
    {
        public static PickerManager Instance { get; private set; }
        
        [SerializeField] private Player player;

        [SerializeField] private Image aimPrefab;
        private Image previousAim;

        private Picker currentPicker;

        private static bool[] _allowedToPick = { true, false, true, false, true };

        public static void SetAllAvailable() => _allowedToPick = new[] { true, true, true, true, true };

        public void Awake() => Instance = this;

        public void PickNextPossible()
        {
            if (BattleFlowManager.Instance.EnemiesWithoutNulls.All(enemy => enemy.Dead)) return;
            
            for (int i = 0; i < _allowedToPick.Length; i++)
            {
                if (!PossibleToPick(i)) continue;
                Pick(i);
                break;
            }
        }

        public void Pick(Picker picker)
        {
            if (!PossibleToPick(picker)) return;
            player.target = picker.enemy;
            DrawAim(picker.enemy);
            currentPicker = picker;
        }

        private void Pick(int index)
        {
            if (!PossibleToPick(index)) return;
            player.target = BattleFlowManager.Instance.enemiesWithNulls[index];
            DrawAim(BattleFlowManager.Instance.enemiesWithNulls[index]);
            currentPicker = BattleFlowManager.Instance.enemiesWithNulls[index].GetComponentInChildren<Picker>();
        }

        public void OnPickerDestroyed(Picker picker)
        {
            if (currentPicker == picker) PickNextPossible();
        }
        
        private void DrawAim(Component picker)
        {
            if (previousAim != null) Destroy(previousAim.gameObject);
            previousAim = Instantiate(aimPrefab, picker.transform);
        }

        private bool PossibleToPick(Picker picker)
        {
            int index = BattleFlowManager.Instance.enemiesWithNulls.IndexOf(picker.enemy);

            return PossibleToPick(index);
        }

        private bool PossibleToPick(int index)
        {
            bool noOtherOptions = true;
            for (int i = 0; i < _allowedToPick.Length && i < BattleFlowManager.Instance.enemiesWithNulls.Count; i++)
            {
                if (_allowedToPick[i] && BattleFlowManager.Instance.enemiesWithNulls[i] != null && 
                    !BattleFlowManager.Instance.enemiesWithNulls[i].Dead)
                    noOtherOptions = false;
            }

            return BattleFlowManager.Instance.enemiesWithNulls[index] != null &&
                   !BattleFlowManager.Instance.enemiesWithNulls[index].Dead && 
                   (_allowedToPick[index] || noOtherOptions);
        }
    }
}