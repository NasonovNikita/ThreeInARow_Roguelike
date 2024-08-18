using Battle.Spells;
using Battle.Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MapHeal : MonoBehaviour
    {
        private Button _btn;

        public void Awake()
        {
            _btn = GetComponentInChildren<Button>();
        }

        public void Start()
        {
            if (Player.Data.spells.Exists(spell => spell is Healing))
            {
                var spell =
                    (Healing)Instantiate(
                        Player.Data.spells.Find(spell => spell is Healing));
                _btn.onClick.AddListener(spell.MapCast);
                _btn.GetComponentInChildren<Text>().text =
                    $"{spell.Title} {spell.useCost}";
            }
            else
            {
                Destroy(_btn.gameObject);
            }
        }
    }
}