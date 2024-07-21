using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(IModIconModifier mod)
        {
            ModIcon.Create(mod, transform);
        }
    }
}