using System;

namespace Other
{
    public interface IChangeAble
    {
        public bool EndedWork { get; }

        public event Action OnChanged;
    }
}