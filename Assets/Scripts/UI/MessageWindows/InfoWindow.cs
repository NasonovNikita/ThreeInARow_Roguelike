using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    public class InfoWindow : MonoBehaviour
    {
        public static InfoWindow instance;
        [SerializeField] private GameObject windowPrefab;
        private RectTransform textTransform;
        private GameObject window;
        private Text windowText;

        public Vector2 WindowSize => window == null ? Vector2.zero : textTransform.sizeDelta;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Close()
        {
            if (window != null) Destroy(window);
        } // ReSharper disable Unity.PerformanceAnalysis
        public void Write(string content)
        {
            if (window != null) Destroy(window.gameObject);

            window = Instantiate(windowPrefab, UICanvas.Instance.transform);
            windowText = window.GetComponentInChildren<Text>();
            textTransform = windowText.GetComponent<RectTransform>();
            windowText.text = content;
            var fitter = windowText.GetComponent<ContentSizeFitter>();
            fitter.SetLayoutHorizontal();
            fitter.SetLayoutVertical();
        }

        public void MoveTo(Vector3 position, Vector3 localShift = new())
        {
            if (window == null) return;

            Transform wTr = window.transform;

            wTr.position = position;
            Vector3 localPosition = wTr.localPosition;
            Vector3 localScale = wTr.localScale;
            localPosition += localShift;

            wTr.localPosition = localPosition;
            var y = localPosition.y * localScale.y;
            var x = localPosition.x * localScale.x;
            var height = WindowSize.y;
            var width = WindowSize.x;
            var screenHeight = FindFirstObjectByType<CanvasScaler>().referenceResolution.y;
            var screenWidth = FindFirstObjectByType<CanvasScaler>().referenceResolution.x;

            if (y - height / 2 <= -screenHeight / 2 ||
                y + height / 2 >= screenHeight / 2)
                wTr.localPosition -= new Vector3(0, localShift.y) * 2;

            if (x - width / 2 <= -screenWidth / 2 ||
                x + width / 2 >= screenWidth / 2)
                wTr.localPosition -= new Vector3(localShift.x, 0) * 2;
        }
    }
}