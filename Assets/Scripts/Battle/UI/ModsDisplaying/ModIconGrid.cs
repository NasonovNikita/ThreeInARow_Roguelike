using UnityEngine;

namespace Battle.UI.ModsDisplaying
{
    /// <summary>
    ///     A view, that contains <see cref="ModIcon">ModIcons</see> in its grid.
    /// </summary>
    public class ModIconGrid : MonoBehaviour
    {
        public void Add(IModIconModifier mod)
        {
            // Has content-fitter magic, so attach to its transform
            ModIcon.Create(mod, transform);
        }
    }
}