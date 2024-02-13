using System;
using Battle.Spells;
using Battle.Units;
using Other;
using UI.MessageWindows;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Treasure
{
    public class TreasureBox : MonoBehaviour
    {
        public LootItem treasure;
        [SerializeField] private Button button;

        public void Start()
        {
            Tools.InitButton(button, TryGet, treasure.Title);
            button.GetComponent<InfoObject>().text = treasure.Description;
        }

        private void TryGet()
        {
            if (treasure is Spell spell)
            {
                switch (Player.data.spells.Count)
                {
                    case > 4:
                        throw new Exception("Player has more then 4 spells. He couldn't have them normally");
                    case 4:
                        SpellGettingWarningWindow.Create(spell, OnGet);
                        return;
                }
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