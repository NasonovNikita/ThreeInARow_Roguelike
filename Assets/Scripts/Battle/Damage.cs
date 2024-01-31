using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public readonly struct Damage
    {
        private readonly Dictionary<DmgType, int> parts;

        public static Damage Zero => new(0);

        public Damage(int phDmg=0, int fDmg=0, int cDmg=0, int pDmg=0, int lDmg=0, int mDmg=0)
        {
            parts = new Dictionary<DmgType, int>
            {
                { DmgType.Physic, phDmg },
                { DmgType.Fire, fDmg },
                { DmgType.Cold, cDmg },
                { DmgType.Poison, pDmg },
                { DmgType.Light, lDmg },
                { DmgType.Magic, mDmg}
            };
        }

        public IReadOnlyDictionary<DmgType, int> Parts => parts;

        public bool IsZero => parts.Values.All(val => val == 0);
    }

    public enum DmgType
    {
        Fire,
        Cold,
        Poison,
        Light,
        Physic,
        Magic
    }
}