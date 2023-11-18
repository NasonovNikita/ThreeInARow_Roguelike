using UnityEngine;

namespace Other
{
    public class PromisedObject : MonoBehaviour
    {
        [SerializeField]
        private string component;
        
        [SerializeField]
        private GameObject obj;

        [SerializeField]
        private SourceType sourceType;

        private enum SourceType
        {
            FirstObject,
            Indirect        // Object attached from any other point of code
        }

        public void Awake()
        {
            if (sourceType == SourceType.FirstObject)
            {
                
            }
        }
    }
}