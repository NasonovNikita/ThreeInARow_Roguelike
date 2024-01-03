using System;
using System.Collections.Generic;
using System.Linq;
using Battle;
using Battle.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class BattleTargetPicker : MonoBehaviour, IPointerClickHandler
    {
        private static BattleManager manager;

        private static Dictionary<int, bool> isFrontRaw;

        private Enemy enemy;
        
        public static void ResetPick()
        {
            manager = FindFirstObjectByType<BattleManager>();
            isFrontRaw = new Dictionary<int, bool>
            {
                { 0, true },
                { 1, false },
                { 2, true },
                { 3, false },
                { 4, true }
            };
            Pick(0);
        }

        public void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
        }

        private static void Pick(int index)
        {
            if (!IsPossibleToPick(index)) return;
            
            manager.target = manager.enemies[index];
            
            // TODO Drawing pick
        }

        public static void PickNextPossible()
        {

            for (int i = 0; i < manager.enemies.Count; i++)
            {
                if (manager.enemies[i] == null || !IsPossibleToPick(i)) continue;
                Pick(i);
                return;
            }

            throw new IndexOutOfRangeException(
                "All enemies are not suitable to pick, are you trying to pick one when no one left?"
                );
        }

        public static void SetAllRawsAvailable()
        {
            isFrontRaw = new Dictionary<int, bool>
            {
                { 0, true },
                { 1, true },
                { 2, true },
                { 3, true },
                { 4, true }
            };
        }

        private static bool IsPossibleToPick(int index)
        {
            return isFrontRaw[index] || isFrontRaw
                .Where(v => v.Value && v.Key < manager.enemies.Count)
                .All(v => manager.enemies[v.Key] == null);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int index = manager.enemies.FindIndex(v => v == enemy);
            Pick(index);
        }
    }
}