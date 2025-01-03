#if UNITY_EDITOR

using System.Collections.Generic;
using Battle;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DevTools.DebugLogging
{
    /// <summary>
    ///     Uses <see cref="DebugLoggingGroup">logging groups</see>
    ///     to write debug info about flow.
    /// </summary>
    public class FlowInfoDebugManager : MonoBehaviour // Battle only yet
    {
        private static FlowInfoDebugManager _instance;

        [SerializeField] private List<DebugLoggingGroup> battleLoggingGroups;
        [SerializeField] private List<DebugLoggingGroup> mapLoggingGroups;

        private static string CurrentScene => UnitySceneManager.GetActiveScene().name;

        private static string SeparatingLine =>
            "--------------------------------------------------------------------------------------------------------";

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                AttachToScenes();
            }
            else
            {
                Destroy(this);
            }
        }

        public static void WriteDebug(string content)
        {
            Debug.unityLogger.Log(content);
        }

        private void AttachToScenes()
        {
            SceneManager.OnSceneFullyLoaded += () => AttachScene(battleLoggingGroups);
            SceneManager.OnSceneLeave += () => UnAttachScene(battleLoggingGroups);

            Map.SceneManager.OnSceneFullyLoaded += () => AttachScene(mapLoggingGroups);
            Map.SceneManager.OnSceneLeave += () => UnAttachScene(mapLoggingGroups);
        }

        private void AttachScene(List<DebugLoggingGroup> groups)
        {
            WriteDebug($"Attached to scene {CurrentScene}\n{SeparatingLine}");

            AttachGroups(groups);
        }

        private void UnAttachScene(List<DebugLoggingGroup> groups)
        {
            UnAttachGroups(groups);

            WriteDebug($"UnAttached from scene {CurrentScene}\n{SeparatingLine}");
        }

        private void AttachGroups(List<DebugLoggingGroup> groups)
        {
            foreach (var group in groups) group.Attach();
        }

        private void UnAttachGroups(List<DebugLoggingGroup> groups)
        {
            foreach (var group in groups) group.UnAttach();
        }
    }
}

#endif