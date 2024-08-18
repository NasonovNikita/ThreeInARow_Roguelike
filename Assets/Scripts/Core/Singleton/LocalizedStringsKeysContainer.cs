using System;
using Knot.Localization;
using UnityEngine;

namespace Core.Singleton
{
    [Obsolete("Is never used or replaced by other similar objects.")]
    public class LocalizedStringsKeysContainer : MonoBehaviour
    {
        public static LocalizedStringsKeysContainer Instance;

        public KnotTextKeyReference instakillProtectionLocalizedKey;
        public KnotTextKeyReference unitInfoLocalizedKey;
        public KnotTextKeyReference miss;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

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
    }
}