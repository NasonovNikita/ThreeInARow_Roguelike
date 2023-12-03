using System.Collections.Generic;
using System.Linq;

namespace Battle
{
    public readonly struct Damage
    {
        private readonly Dictionary<DmgType, int> parts;

        public Damage(int fDmg=0, int cDmg=0, int pDmg=0, int lDmg=0, int phDmg=0, int mDmg=0)
        {
            parts = new Dictionary<DmgType, int>
            {
                { DmgType.Fire, fDmg },
                { DmgType.Cold, cDmg },
                { DmgType.Poison, pDmg },
                { DmgType.Light, lDmg },
                { DmgType.Physic, phDmg },
                { DmgType.Magic, mDmg}
            };
        }
        
        public IReadOnlyDictionary<DmgType, int> Get()
        {
            return parts;
        }

        public bool IsZero()
        {
            return parts.Values.All(val => val == 0);
        }
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