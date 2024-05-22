using System;
using Core.Singleton;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle.ModsDisplaying
{
    [RequireComponent(typeof(InfoObject))]
    public class ModIcon : MonoBehaviour
    {
        [SerializeField] private Text subInfo;
        [SerializeField] private Image img;
        [SerializeField] private InfoObject modInfo;
        private DisplayedModifier mod;

        public static void Create(DisplayedModifier mod, Transform parentTransform)
        {
            if (mod.Sprite is null) return;
            
            ModIcon icon = Instantiate(PrefabsContainer.instance.modIcon, parentTransform);
            
            icon.mod = mod;
            
            icon.img.sprite = mod.Sprite;
            icon.subInfo.text = mod.SubInfo;
            icon.modInfo.text = mod.Description;
            icon.mod.OnChanged += icon.CheckMod;
        }

        public void OnDestroy()
        {
            mod.OnChanged -= CheckMod;
        }

        private void CheckMod()
        {
            if (mod.ToDelete)
            {
                Delete();
            }
            else
            {
                subInfo.text = mod.SubInfo;
                modInfo.text = mod.Description;
            }
        }

        private void Delete()
        {
            Destroy(gameObject);
        }
    }
}