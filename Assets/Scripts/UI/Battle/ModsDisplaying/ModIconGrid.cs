using UnityEngine;

namespace UI.Battle.ModsDisplaying
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(DisplayedModifier mod) => ModIcon.Create(mod, transform);
    }
}