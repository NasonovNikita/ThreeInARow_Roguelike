using UnityEngine;

namespace Core.Singleton
{
    public class OffScreenPoint : MonoBehaviour
    {
        public static OffScreenPoint Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public void Hide(GameObject obj) => 
            obj.transform.SetParent(transform, false);
    }
}