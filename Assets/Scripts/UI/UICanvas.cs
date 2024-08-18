using UnityEngine;

namespace UI
{
    /// <summary>
    ///     A preserved canvas for common UI instantiation.
    /// </summary>
    public class UICanvas : MonoBehaviour
    {
        public static UICanvas Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }
    }
}