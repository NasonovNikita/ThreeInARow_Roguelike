using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public static List<Modifier> mods = new();

        public int moves;

        public ModType type;
    
        public float value;

        private bool delay;

        [SerializeField]
        private List<Condition> conditions;

        private Action onMove;

        public Modifier(int moves, ModType type, List<Condition> conditions = null, float value = 1, Action onMove = null, bool delay = false)
        {
            this.moves = moves;
            this.type = type;
            this.conditions = conditions ?? new List<Condition>();
            this.value = value;
            this.onMove = onMove;
            this.delay = delay;
            mods.Add(this);
        }

        public float Use()
        {
            bool res = conditions.Aggregate(true, (current, cond) => current && cond.Use());
            return res && moves != 0 ? value : 0;
        }
        public static void Move()
        {
            foreach (Modifier mod in mods.ToList())
            {
                if (mod.delay)
                {
                    mod.delay = false;
                    return;
                }
                mod.onMove?.Invoke();
                mod.moves -= 1;
                if (mod.moves != 0) continue;
                mods.Remove(mod);
            }
        }
    }
}