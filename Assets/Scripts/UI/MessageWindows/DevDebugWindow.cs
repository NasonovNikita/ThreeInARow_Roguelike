using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class DevDebugWindow : MonoBehaviour
    {
        public static DevDebugWindow instance;
        [SerializeField] private Object windowPrefab;
        private GameObject window;
        private RectTransform windowTransform;

        public Vector2 WindowSize 
        { 
            get
            {
                if (window == null) return new Vector2();
                return window.GetComponent<RectTransform>().rect.size * 0.6f;
            }
        }
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
            if (window != null) Destroy(window);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Write(string content)
        {
            if (window != null) Destroy(window.gameObject);
            window = (GameObject) Instantiate(windowPrefab, FindFirstObjectByType<Canvas>().transform);
            window.GetComponentInChildren<Text>().text = content;
            windowTransform = window.GetComponent<RectTransform>();
        }

        public void MoveTo(Vector3 position, Vector3 localShift = new())
        {
            if (window == null) return;

            Transform wTr = window.transform;
            
            wTr.position = position;
            var localPosition = wTr.localPosition;
            var localScale = wTr.localScale;
            localPosition += localShift;
            
            wTr.localPosition = localPosition;
            Rect rect = windowTransform.rect;
            float y = localPosition.y * localScale.y;
            float x = localPosition.x * localScale.x;
            float height = rect.height;
            float width = rect.width;
            float screenHeight = FindFirstObjectByType<CanvasScaler>().referenceResolution.y;
            float screenWidth = FindFirstObjectByType<CanvasScaler>().referenceResolution.x;

            if (y - height / 2 <= -screenHeight / 2 ||
                y + height / 2 >= screenHeight / 2)
            {
                wTr.localPosition -= new Vector3(0, localShift.y) * 2;
            }
            
            if (x - width / 2 <= -screenWidth / 2 ||
                x + width / 2 >= screenWidth / 2)
            {
                wTr.localPosition -= new Vector3(localShift.x, 0) * 2;
            }
        }
    }
}