using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(IModIconModifier mod) => ModIcon.Create(mod, transform);
    }
}