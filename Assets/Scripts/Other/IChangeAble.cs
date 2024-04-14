using System;

namespace Other
{
    public interface IChangeAble
    {
        public event Action OnChanged;
    }
}