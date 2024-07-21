using System;

namespace Core.Saves
{
    [Serializable]
    public abstract class SaveObject
    {
        public abstract void Apply();
    }
}