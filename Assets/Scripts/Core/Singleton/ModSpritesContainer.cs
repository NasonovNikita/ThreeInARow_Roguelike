using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Singleton
{
    public class ModSpritesContainer : MonoBehaviour
    {
        public static ModSpritesContainer Instance { get; private set; }
        
        public Sprite burning;
        public Sprite damage;
        public Sprite deal;
        public Sprite endurance;
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
        public Sprite frozen;
        public Sprite reflection;
        [FormerlySerializedAs("Vampirism")] public Sprite vampirism;

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