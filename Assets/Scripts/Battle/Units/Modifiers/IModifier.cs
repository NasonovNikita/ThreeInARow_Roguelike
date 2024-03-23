using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Units.Modifiers
{
    public interface IModifier
    {
        public string SubInfo { get; }
        public Sprite Sprite { get; }
        public string Description { get; }

        public bool IsZero { get; }
        public bool KeepBetweenBattles { get; }

        protected bool ConcatAble(IModifier other);
        public void Concat(IModifier other);

        public static void AddModToList<T>(List<T> list, IModifier modToAdd) where T : IModifier
        {
            var mod = list.FirstOrDefault(mod => mod.ConcatAble(modToAdd));

            if (mod != null) mod.Concat(modToAdd);
            else list.Add((T)modToAdd);
        }

        public static List<T> CleanedModifiers<T>(IEnumerable<T> mods) where T : IModifier =>
            mods.Where(mod => mod.KeepBetweenBattles && !mod.IsZero).ToList();
    }
}