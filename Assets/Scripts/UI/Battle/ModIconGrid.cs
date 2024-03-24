using Battle.Units.Modifiers;
using UnityEngine;

namespace UI.Battle
{
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(IModIconAble mod) => ModIcon.Create(mod, transform);
    }
}