using UnityEngine;

namespace Core
{
    public class SpritesContainer : MonoBehaviour
    {
        public static SpritesContainer instance;

        public Sprite damageMod;
        public Sprite manaMod;
        public Sprite shield;
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