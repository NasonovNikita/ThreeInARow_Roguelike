#if UNITY_EDITOR

using UI;
using UnityEditor;
using UnityEngine;

namespace DevTools.DeveloperConsole
{
    public class DevConsoleCreator : Editor
    {
        [MenuItem("Tools/DebugConsole")]
        public static void Create()
        {
            Instantiate(
                Resources.Load<DeveloperConsole>("Prefabs/UI/Windows/DevConsole"),
                UICanvas.Instance.transform);
        }
    }
}

#endif