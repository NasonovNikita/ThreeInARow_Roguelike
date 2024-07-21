using UnityEngine;

namespace UI
{
    public class UICanvas : MonoBehaviour
    {
        public static UICanvas Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }
    }
}