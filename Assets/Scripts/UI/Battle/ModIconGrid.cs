using Battle.Modifiers;
using UnityEngine;

namespace UI.Battle
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(Modifier mod)
        {
            ModIcon.Create(mod, transform);
        }
    }
}