using UnityEngine;

namespace Core.Singleton
{
    /// <summary>
    ///     Point not visible on screen. Can be used to hide things instead of destroying.
    /// </summary>
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
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        ///     Shortcut to disabling and <see cref="Hide">hiding</see> an Object.
        /// </summary>
        public void DisableAndHide(GameObject obj)
        {
            obj.SetActive(false);
            Hide(obj);
        }

        /// <summary>
        ///     Moves given object to <see cref="OffScreenPoint"/>,
        ///     so it is not visible on screen.
        /// </summary>
        public void Hide(GameObject obj)
        {
            obj.transform.SetParent(transform, false);
        }
    }
}