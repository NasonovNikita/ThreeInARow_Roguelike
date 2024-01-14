using System;
using UnityEngine;

namespace Other
{
    [Serializable]
    public class GetAble : ScriptableObject
    {
        [SerializeField] protected string title;
        [SerializeField] protected string description;
        public Sprite img;
        public int frequency;

        public string Title => title;
        public string Description => description;
        
        public virtual void OnGet() {}
        public virtual void Get()
        {
            OnGet();
        }
    }
}