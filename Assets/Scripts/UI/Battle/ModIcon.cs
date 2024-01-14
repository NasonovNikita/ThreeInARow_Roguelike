using Battle.Modifiers;
using Core;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle
{
    [RequireComponent(typeof(DevDebugAbleObject))]
    public class ModIcon : MonoBehaviour
    {
        [SerializeField] private Text moves;
        [SerializeField] private Image img;
        [SerializeField] private DevDebugAbleObject debug;
        private Modifier mod;

        public void Update()
        {
            if (mod.Use == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                moves.text = mod.moves == -1 ? "-" : mod.moves.ToString();
                if (mod.delay) moves.text += "d";
            }
        }

        public static void Create(Modifier mod, Transform parentTransform)
        {
            ModIcon icon = Instantiate(PrefabsContainer.instance.modIcon, parentTransform);
            icon.mod = mod;
            icon.img.sprite = mod.Sprite;
            icon.debug.text = mod.Description;
        }
    }
}