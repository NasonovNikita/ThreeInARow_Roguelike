using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DevDebugWindow : MonoBehaviour
    {
        public static DevDebugWindow instance;
        [SerializeField] private Object windowPrefab;
        private GameObject window;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        public void Close()
        {
            Destroy(window);
        }

        public void Write(string content)
        {
            if (window != null) Destroy(window);
            window = (GameObject) Instantiate(windowPrefab, FindFirstObjectByType<Canvas>().transform);
            window.GetComponentInChildren<Text>().text = content;
            window.GetComponentInChildren<Button>().onClick.AddListener(Close);
        }
    }
}