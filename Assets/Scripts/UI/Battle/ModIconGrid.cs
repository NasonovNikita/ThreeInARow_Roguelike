using Battle.Units.Modifiers;
using UnityEngine;

namespace UI.Battle
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(IModifier mod)
        {
            if (this != null) ModIcon.Create(mod, transform);
        }
    }
}