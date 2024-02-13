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

        #region Elements

        public KnotTextKeyReference fires;
        public KnotTextKeyReference colds;
        public KnotTextKeyReference poisons;
        public KnotTextKeyReference lights;
        public KnotTextKeyReference physics;
        public KnotTextKeyReference magics;
        
        public KnotTextKeyReference fire;
        public KnotTextKeyReference cold;
        public KnotTextKeyReference poison;
        // ReSharper disable once InconsistentNaming
        public KnotTextKeyReference light_;
        public KnotTextKeyReference physic;
        public KnotTextKeyReference magic;

        #endregion
        
        
        public KnotTextKeyReference instakillProtectionLocalizedKey;
        public KnotTextKeyReference unitInfoLocalizedKey;
        public KnotTextKeyReference miss;
        
        public string DmgTypes(DmgType dmgType)
        {
            return dmgType switch
            {
                Battle.DmgType.Fire => fires.Value,
                Battle.DmgType.Cold => colds.Value,
                Battle.DmgType.Poison => poisons.Value,
                Battle.DmgType.Light => lights.Value,
                Battle.DmgType.Physic => physics.Value,
                Battle.DmgType.Magic => magics.Value,
                _ => throw new ArgumentOutOfRangeException(nameof(dmgType), dmgType, null)
            };
        }

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