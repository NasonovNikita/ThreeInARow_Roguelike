using System;
using Other;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class Counter : IChangeAble
    { 
        [SerializeField] private int count;
        public int Count => count;
    
        public Counter(int count) => this.count = count;
    
        public event Action OnChanged;
    
        public bool EndedWork => count == 0;
    
        public string SubInfo => count.ToString();
    
        public void Concat(Counter other)
        {
            
            if (other.count == 0) return; // Not invoking OnChanged
            count += other.count;
            if (count < 0) count = 0;
    
            OnChanged?.Invoke();
        }
    
        public int Decrease(int val)
        {
            if (EndedWork) return val; // Not invoking OnChanged
            
            int res = Math.Max(val - count, 0);
            count = Math.Max(count - val, 0);
    
            OnChanged?.Invoke();
    
            return res;
        }

        public int Increase(int val)
        {
            if (EndedWork) return val;
            
            int res = val + count;
            count = 0;
    
            OnChanged?.Invoke();
    
            return res;
        }
    
        public static Counter operator +(Counter first, Counter second) =>
            new(first.count + second.count);
    }
}