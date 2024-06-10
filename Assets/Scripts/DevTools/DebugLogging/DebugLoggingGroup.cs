#if UNITY_EDITOR

using System;
using UnityEngine;

namespace DevTools.DebugLogging
{
    [Serializable]
    public abstract class DebugLoggingGroup : MonoBehaviour
    {
        [SerializeField] private bool doLog;

        public void CheckAndWrite(string content)
        {
            if (doLog) FlowInfoDebugManager.WriteDebug(content);
        }

        public abstract void Attach();

        public abstract void UnAttach();
    }
}

#endif