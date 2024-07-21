using Knot.Localization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Singleton
{
    public class ModDescriptionsContainer : MonoBehaviour
    {
        public KnotTextKeyReference shield;
        public KnotTextKeyReference damagePositive;
        public KnotTextKeyReference damageNegative;
        public KnotTextKeyReference hpDamagePositive;
        public KnotTextKeyReference hpDamageNegative;
        public KnotTextKeyReference healingPositive;
        public KnotTextKeyReference healingNegative;
        [FormerlySerializedAs("manaPositive")] public KnotTextKeyReference manaWastingPositive;
        [FormerlySerializedAs("manaNegative")] public KnotTextKeyReference manaWastingNegative;
        public KnotTextKeyReference burning;
        public KnotTextKeyReference deal;
        public KnotTextKeyReference fury;
        public KnotTextKeyReference immortality;
        public KnotTextKeyReference irritation;
        public KnotTextKeyReference passiveBomb;
        public KnotTextKeyReference randomIgnition;
        public KnotTextKeyReference sharp;
        public KnotTextKeyReference stun;
        public KnotTextKeyReference vampirism;
        public static ModDescriptionsContainer Instance { get; private set; }

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
    }
}