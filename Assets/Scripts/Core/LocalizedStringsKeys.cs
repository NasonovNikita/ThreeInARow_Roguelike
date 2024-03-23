using System;
using Battle;
using Knot.Localization;
using UnityEngine;

namespace Core
{
    public class LocalizedStringsKeys : MonoBehaviour
    {
        public static LocalizedStringsKeys instance;

        #region Modifiers
        
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
        
        #endregion
        
        
        public KnotTextKeyReference instakillProtectionLocalizedKey;
        public KnotTextKeyReference unitInfoLocalizedKey;
        public KnotTextKeyReference miss;
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