using Battle;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    public class PickerManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private BattleManager manager;

        [SerializeField] private Image aimPrefab;
        private Image previousAim;

        private Picker currentPicker;

        private static bool[] allowedToPick = { true, false, true, false, true };

        public void Start()
        {
            PickNextPossible();
        }

        public static void SetAllAvailable() => allowedToPick = new[] { true, true, true, true, true };

        private void PickNextPossible()
        {
            for (int i = 0; i < allowedToPick.Length; i++)
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
            player.target = manager.Enemies[index];
            DrawAim(manager.Enemies[index]);
            currentPicker = manager.Enemies[index].GetComponent<Picker>();
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
            int index = manager.Enemies.IndexOf(picker.enemy);

            return PossibleToPick(index);
        }

        private bool PossibleToPick(int index)
        {
            bool noOtherOptions = true;
            for (int i = 0; i < allowedToPick.Length; i++)
            {
                if (allowedToPick[i] && manager.Enemies[i] != null) noOtherOptions = false;
            }

            return allowedToPick[index] || noOtherOptions;
        }
    }
}