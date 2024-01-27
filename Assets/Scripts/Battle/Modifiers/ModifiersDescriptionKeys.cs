using System;
using Knot.Localization;
using UnityEngine;

namespace Battle.Modifiers
{
    public class ModifiersDescriptionKeys : MonoBehaviour
    {
        public static ModifiersDescriptionKeys instance;
        
        public KnotTextKeyReference blind;
        public KnotTextKeyReference stun;
        public KnotTextKeyReference freezing;
        public KnotTextKeyReference burning;
        public KnotTextKeyReference poisoning;
        public KnotTextKeyReference frozen;
        public KnotTextKeyReference irritated;
        public KnotTextKeyReference ignition;
        public KnotTextKeyReference dealDmg;
        public KnotTextKeyReference dealDmgPerGem;
        public KnotTextKeyReference getDmg;
        public KnotTextKeyReference healHp;
        public KnotTextKeyReference refillMana;
        public KnotTextKeyReference wasteMana;
        
        public KnotTextKeyReference more;
        public KnotTextKeyReference less;

        public KnotTextKeyReference fire;
        public KnotTextKeyReference cold;
        public KnotTextKeyReference poison;
        // ReSharper disable once InconsistentNaming
        public KnotTextKeyReference light_;
        public KnotTextKeyReference physic;
        public KnotTextKeyReference magic;

        public string DmgType(DmgType dmgType)
        {
            return dmgType switch
            {
                Battle.DmgType.Fire => fire.Value,
                Battle.DmgType.Cold => cold.Value,
                Battle.DmgType.Poison => poison.Value,
                Battle.DmgType.Light => light_.Value,
                Battle.DmgType.Physic => physic.Value,
                Battle.DmgType.Magic => magic.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(dmgType), dmgType, null)
            };
        }
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}