using System;
using Battle.Spells;
using Battle.Units;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace Treasure
{
    /// <summary>
    ///     Contains <see cref="LootItem"/> treasure.
    ///     Adds some logic to obtaining of an item (e.g. spell).
    /// </summary>
    public class TreasureBox : MonoBehaviour
    {
        public LootItem treasure;
        [SerializeField] private Button button; // TODO place TryGet in editor

        public void Start()
        {
            button.InitButton(TryGet, treasure.Title);
            button.GetComponent<InfoObject>().text = treasure.Description;
        }

        public void TryGet()
        {
            if (treasure is Spell spell)
                switch (Player.Data.spells.Count)
                {
                    case > 4:
                        throw new Exception(
                            "Player has more then 4 spells. He couldn't have them normally");
                    case 4:
                        SpellGettingWarningWindow.Create(spell, OnGet);
                        return;
                }

            treasure.Get();
            OnGet();
        }

        private void OnGet()
        {
            button.GetComponentInChildren<Text>().text = "got";
            button.onClick.RemoveAllListeners();
        }
    }
}