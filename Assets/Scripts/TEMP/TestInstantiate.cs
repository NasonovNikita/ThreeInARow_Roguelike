
using UnityEngine;

namespace TEMP
{
    public class TestInstantiate : MonoBehaviour
    {
        private static bool _done;

        public void Awake()
        {
            if (_done) return;
            _done = true;
            
            Instantiate(this);
            Instantiate(this);
            Instantiate(this);
            Instantiate(this);
            Destroy(gameObject);
        }
    }
}