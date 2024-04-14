using UnityEngine;

namespace Core.SingletonContainers
{
    public class SpritesContainer : MonoBehaviour
    {
        public static SpritesContainer instance;

        public Sprite damageMod;
        public Sprite manaMod;
        public Sprite shield;
        public Sprite shieldBroken;
        public Sprite hpHealing;
        public Sprite stun;
        public Sprite blind;
        public Sprite irritation;
        public Sprite ignition;
        public Sprite frozen;
        public Sprite empty;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}