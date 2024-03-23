using Battle.Units.Modifiers;
using Core;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    [RequireComponent(typeof(InfoObject))]
    public class ModIcon : MonoBehaviour
    {
        [SerializeField] private Text subInfo;
        [SerializeField] private Image img;
        [SerializeField] private InfoObject debug;
        private IModifier mod;

        public void Update()
        {
            if (!mod.IsZero) Destroy(gameObject);
            else subInfo.text = mod.SubInfo;
        }

        public static void Create(IModifier mod, Transform parentTransform)
        {
            if (mod.Sprite is null) return;
            ModIcon icon = Instantiate(PrefabsContainer.instance.modIcon, parentTransform);
            icon.mod = mod;
            icon.img.sprite = mod.Sprite;
            icon.debug.text = mod.Description;
        }
    }
}