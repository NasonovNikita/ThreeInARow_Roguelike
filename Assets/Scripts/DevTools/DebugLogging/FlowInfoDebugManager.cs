#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DevTools.DebugLogging
{
    public class FlowInfoDebugManager : MonoBehaviour // Battle only yet
    {
        private static FlowInfoDebugManager _instance;

        [SerializeField] private List<DebugLoggingGroup> battleLoggingGroups;
        [SerializeField] private List<DebugLoggingGroup> mapLoggingGroups;

        private static string CurrentScene => UnitySceneManager.GetActiveScene().name;
        private static string SeparatingLine => 
            "--------------------------------------------------------------------------------------------------------";

        public static void WriteDebug(string content) => Debug.unityLogger.Log(content);

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                AttachToScenes();
            }
            else Destroy(this);
        }

        private void AttachToScenes()
        {
            Battle.SceneManager.OnSceneFullyLoaded += () => AttachScene(battleLoggingGroups);
            Battle.SceneManager.OnSceneLeave += () => UnAttachScene(battleLoggingGroups);

            Map.SceneManager.OnSceneFullyLoaded += () => AttachScene(mapLoggingGroups);
            Map.SceneManager.OnSceneLeave += () => UnAttachScene(mapLoggingGroups);
        }

        private void AttachScene(List<DebugLoggingGroup> groups)
        {
            WriteDebug($"Attached to scene { CurrentScene }\n{ SeparatingLine }");
            
            AttachGroups(groups);
        }

        private void UnAttachScene(List<DebugLoggingGroup> groups)
        {
            UnAttachGroups(groups);
            
            WriteDebug($"UnAttached from scene { CurrentScene }\n{ SeparatingLine }");
        }

        private void AttachGroups(List<DebugLoggingGroup> groups)
        {
            foreach (DebugLoggingGroup group in groups) group.Attach();
        }

        private void UnAttachGroups(List<DebugLoggingGroup> groups)
        {
            foreach (DebugLoggingGroup group in groups) group.UnAttach();
        }
    }
}

#endif