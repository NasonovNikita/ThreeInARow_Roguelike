#if UNITY_EDITOR

using System;
using UnityEngine;

namespace DevTools.DebugLogging
{
    [Serializable]
    public abstract class DebugLoggingGroup : MonoBehaviour
    {
        [SerializeField] private bool doLog;

        /// <summary>
        ///     Checks if enabled and writes debug.
        /// </summary>
        public void CheckAndWrite(string content)
        {
            if (doLog) FlowInfoDebugManager.WriteDebug(content);
        }

        /// <summary>
        ///     Attach to events to log.
        /// </summary>
        public abstract void Attach();

        /// <summary>
        ///     UnAttach from events used to be logged.
        /// </summary>
        public abstract void UnAttach();
    }
}

#endif