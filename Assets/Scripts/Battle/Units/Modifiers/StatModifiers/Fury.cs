using System;
using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    public class Fury : Modifier, IStatModifier
    {
        private int hpBorder;
        private int dmgIncrease;

        public Fury(int hpBorder, int dmgIncrease, bool permanent = false) : base(permanent)
        {
            this.hpBorder = hpBorder;
            this.dmgIncrease = dmgIncrease;
        }

        private bool Condition => unitBelong.hp <= hpBorder;

        public string SubInfo => Condition ?  "✓" : "✕";

        public Sprite Sprite => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public bool IsZero => false;

        bool IModifier.ConcatAble(IModifier other) => other is Fury;

        public void Concat(IModifier other)
        {
            hpBorder = Math.Max(hpBorder, ((Fury)other).hpBorder);
            dmgIncrease = Math.Max(dmgIncrease, ((Fury)other).dmgIncrease);
        }

        int IStatModifier.Modify(int val) => Condition ? val + dmgIncrease : val;
    }
}