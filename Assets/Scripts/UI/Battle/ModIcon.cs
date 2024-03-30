using Core;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Battle
{
    [RequireComponent(typeof(InfoObject))]
    public class ModIcon : MonoBehaviour
    {
        [SerializeField] private Text subInfo;
        [SerializeField] private Image img;
        [SerializeField] private InfoObject modInfo;
        private IModIconAble mod;

        public void Update()
        {
            if (mod.ToDelete) Destroy(gameObject);
            else subInfo.text = mod.SubInfo;
        }

        public static void Create(IModIconAble mod, Transform parentTransform)
        {
            if (mod.Sprite is null) return;
            ModIcon icon = Instantiate(PrefabsContainer.instance.modIcon, parentTransform);
            icon.mod = mod;
            icon.img.sprite = mod.Sprite;
            icon.modInfo.text = mod.Description;
        }
    }

    public interface IModIconAble
    {
        public Sprite Sprite { get; }
        public string Tag { get; }
        public string Description { get; }
        public string SubInfo { get; }
        public bool ToDelete { get; }
        public const string EmptyInfo = "";
    }
}