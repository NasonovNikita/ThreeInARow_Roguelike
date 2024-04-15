using Knot.Localization;
using UnityEngine;

namespace Core.SingletonContainers
{
    public class ModDescriptionsContainer : MonoBehaviour
    {
        public static ModDescriptionsContainer Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        [SerializeField] public KnotTextKeyReference shieldDescription;
        [SerializeField] public KnotTextKeyReference damageModDescriptionPositive;
        [SerializeField] public KnotTextKeyReference damageModDescriptionNegative;
        [SerializeField] public KnotTextKeyReference healingModDescriptionPositive;
        [SerializeField] public KnotTextKeyReference healingModDescriptionNegative;
    }
}