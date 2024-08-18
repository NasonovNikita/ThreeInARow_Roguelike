using System.Linq;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    /// <summary>
    ///     Controls picking enemies as a target in battle.
    /// </summary>
    public class PickerManager : MonoBehaviour
    {
        private static bool[] _allowedToPick = { true, false, true, false, true };

        [SerializeField] private Image aimPrefab;

        private Picker _currentPicker;
        private Image _previousAim;
        public static PickerManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public static void SetAllAvailable()
        {
            _allowedToPick = new[] { true, true, true, true, true };
        }

        public void PickNextPossible()
        {
            if (BattleFlowManager.Instance.EnemiesWithoutNulls.All(enemy => enemy.Dead))
                return;

            for (var i = 0; i < _allowedToPick.Length; i++)
            {
                if (!PossibleToPick(i)) continue;
                Pick(i);
                break;
            }
        }

        public void Pick(Picker picker)
        {
            if (!PossibleToPick(picker)) return;

            Player.Instance.target = picker.enemy;
            DrawAim(picker.enemy);
            _currentPicker = picker;
        }

        public void OnPickerDestroyed(Picker picker)
        {
            if (_currentPicker == picker) PickNextPossible();
        }

        private void Pick(int index)
        {
            if (!PossibleToPick(index)) return;
            Player.Instance.target = BattleFlowManager.Instance.EnemiesWithNulls[index];
            DrawAim(BattleFlowManager.Instance.EnemiesWithNulls[index]);
            _currentPicker = BattleFlowManager.Instance.EnemiesWithNulls[index]
                .GetComponentInChildren<Picker>();
        }

        private void DrawAim(Component picker)
        {
            if (_previousAim != null) Destroy(_previousAim.gameObject);
            _previousAim = Instantiate(aimPrefab, picker.transform);
        }

        private bool PossibleToPick(Picker picker)
        {
            var index = BattleFlowManager.Instance.EnemiesWithNulls.IndexOf(picker.enemy);

            return PossibleToPick(index);
        }

        private bool PossibleToPick(int index)
        {
            var noOtherOptions = true;
            for (var i = 0;
                 i < _allowedToPick.Length &&
                 i < BattleFlowManager.Instance.EnemiesWithNulls.Count;
                 i++)
                if (_allowedToPick[i] &&
                    BattleFlowManager.Instance.EnemiesWithNulls[i] != null &&
                    !BattleFlowManager.Instance.EnemiesWithNulls[i].Dead)
                    noOtherOptions = false;

            return BattleFlowManager.Instance.EnemiesWithNulls[index] != null &&
                   !BattleFlowManager.Instance.EnemiesWithNulls[index].Dead &&
                   (_allowedToPick[index] || noOtherOptions);
        }
    }
}