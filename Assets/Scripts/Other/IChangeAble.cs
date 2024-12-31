using System;

namespace Other
{
    /// <summary>
    ///     Invokes <see cref="OnChanged"/> when changed.
    /// </summary>
    public interface IChangeAble
    {
        public event Action OnChanged;
    }
}