using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Singleton
{
    public class ModifierSpritesContainer : MonoBehaviour
    {
        public Sprite burning;
        public Sprite damage;
        public Sprite deal;
        public Sprite fury;
        public Sprite manaMod;
        public Sprite shield;
        public Sprite healing;
        public Sprite immortality;
        public Sprite passiveBomb;
        public Sprite stun;
        public Sprite irritation;
        public Sprite randomIgnition;
        public Sprite sharp;
        [FormerlySerializedAs("Vampirism")] public Sprite vampirism;
        public static ModifierSpritesContainer Instance { get; private set; }

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