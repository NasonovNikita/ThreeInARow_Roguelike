using UnityEngine;

namespace Other
{
    public class FieldReference : MonoBehaviour
    {
        public GameObject obj;

        public string component;
        public string field;

        public bool fromPromised;
        [SerializeField]
        private PromisedObject promisedObject;

        public T GetValue<T>()
        {
            Component cmp = obj.GetComponent(component);
            return (T)cmp.GetType().GetField(field).GetValue(cmp);
        }

        public void SetValue<T>(T value)
        {
            Component cmp = obj.GetComponent(component);
            cmp.GetType().GetField(field).SetValue(cmp, value);
        }
    }
}