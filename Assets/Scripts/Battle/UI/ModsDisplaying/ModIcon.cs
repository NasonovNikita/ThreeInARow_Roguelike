using Core.Singleton;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI.ModsDisplaying
{
    /// <summary>
    ///     View for an <see cref="IModIconModifier"/> mod.
    /// </summary>
    [RequireComponent(typeof(InfoObject))]
    public class ModIcon : MonoBehaviour
    {
        [SerializeField] private Text subInfo;
        [SerializeField] private Image img;
        [SerializeField] private InfoObject modInfo;
        private IModIconModifier _mod;

        public void OnDestroy()
        {
            _mod.OnChanged -= CheckMod;
        }

        public static void Create(IModIconModifier mod, Transform parentTransform)
        {
            if (mod.Sprite is null) return;

            var icon = Instantiate(PrefabsContainer.Instance.modIcon, parentTransform);

            icon._mod = mod;

            icon._mod.OnChanged += icon.CheckMod;
            icon.img.sprite = mod.Sprite;
            icon.subInfo.text = mod.SubInfo;
            icon.modInfo.text = mod.Description;
        }

        private void CheckMod()
        {
            if (_mod.ToDelete)
            {
                Delete();
            }
            else
            {
                subInfo.text = _mod.SubInfo;
                modInfo.text = _mod.Description;
            }
        }

        private void Delete()
        {
            Destroy(gameObject);
        }
    }
}