using UnityEngine;
using UnityEngine.UI;

namespace UI.MessageWindows
{
    /// <summary>
    ///     Placeholder for <see cref="InfoObject"/> text.
    /// </summary>
    public class InfoWindow : MonoBehaviour
    {
        public static InfoWindow Instance;
        [SerializeField] private GameObject windowPrefab;
        private RectTransform _textTransform;
        private GameObject _window;
        private Text _windowText;

        public Vector2 WindowSize =>
            _window == null ? Vector2.zero : _textTransform.sizeDelta;

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

        public void Close()
        {
            if (_window != null) Destroy(_window);
        } // ReSharper disable Unity.PerformanceAnalysis
        public void Write(string content)
        {
            if (_window != null) Destroy(_window.gameObject);

            _window = Instantiate(windowPrefab, UICanvas.Instance.transform);
            _windowText = _window.GetComponentInChildren<Text>();
            _textTransform = _windowText.GetComponent<RectTransform>();
            _windowText.text = content;
            var fitter = _windowText.GetComponent<ContentSizeFitter>();
            fitter.SetLayoutHorizontal();
            fitter.SetLayoutVertical();
        }

        /// <summary>
        ///     Moves the object to a certain position.
        /// </summary>
        /// <param name="position">Position in world (canvas).</param>
        /// <param name="localShift">
        ///     Shift applied to <see cref="Transform.localPosition"/>.
        /// </param>
        public void MoveTo(Vector3 position, Vector3 localShift = new())
        {
            if (_window == null) return;

            var windowsTransform = _window.transform;

            windowsTransform.position = position;
            var localPosition = windowsTransform.localPosition;
            var localScale = windowsTransform.localScale;
            localPosition += localShift;

            windowsTransform.localPosition = localPosition;

            var y = localPosition.y * localScale.y;
            var x = localPosition.x * localScale.x;
            var height = WindowSize.y;
            var width = WindowSize.x;
            var screenHeight =
                FindFirstObjectByType<CanvasScaler>().referenceResolution.y;
            var screenWidth =
                FindFirstObjectByType<CanvasScaler>().referenceResolution.x;

            // Choose side (left/right) near pointer in Y axis
            if (y - height / 2 <= -screenHeight / 2 ||
                y + height / 2 >= screenHeight / 2)
                windowsTransform.localPosition -= new Vector3(0, localShift.y) * 2;

            // Choose side (left/right) near pointer in X axis
            if (x - width / 2 <= -screenWidth / 2 ||
                x + width / 2 >= screenWidth / 2)
                windowsTransform.localPosition -= new Vector3(localShift.x, 0) * 2;
        }
    }
}