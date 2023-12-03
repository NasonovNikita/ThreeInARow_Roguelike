using Battle.Spells;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MapHeal : MonoBehaviour
    {
        private Button btn;

        public void Awake()
        {
            btn = GetComponentInChildren<Button>();
        }

        public void Start()
        {
            if (Player.data.spells.Exists(spell => spell is Healing))
            {
                Healing spell = (Healing) Instantiate(Player.data.spells.Find(spell => spell is Healing));
                spell.offBattle = true;
                btn.onClick.AddListener(spell.Cast);
                btn.GetComponentInChildren<Text>().text = spell.title;
            }
            else
            {
                Destroy(btn.gameObject);
            }
        }
    }
}