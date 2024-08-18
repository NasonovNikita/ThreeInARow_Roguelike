using System;

namespace Core.Saves
{
    /// <summary>
    ///     Applies saved data.
    /// </summary>
    [Serializable]
    public abstract class SaveObject
    {
        /// <summary>
        ///     Is used to apply existing Save.
        ///     Use other options to create.
        /// </summary>
        public abstract void Apply();
    }
}