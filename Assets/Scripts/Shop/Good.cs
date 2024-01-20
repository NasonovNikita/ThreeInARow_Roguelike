using System;
using Battle.Spells;
using Battle.Units;
using Other;
using UI.MessageWindows;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "Good", menuName = "Good")]
    [Serializable]
    public class Good : ScriptableObject
    {
        [SerializeField] public LootItem target;
        [SerializeField] public int price;

        public void TryBuy(Action onBuy)
        {
            if (Player.data.money < price) return;
            if (target is Spell spell)
            {
                switch (Player.data.spells.Count)
                {
                    case > 4:
                        throw new Exception("Player has more then 4 spells. He couldn't have them normally");
                    case 4:
                        SpellGettingWarningWindow.Create(spell, () =>
                        {
                            Player.data.money -= price;
                            onBuy?.Invoke();
                        });
                        return;
                }
            }

            Buy();
            onBuy?.Invoke();
        }

        private void Buy()
        {
            Player.data.money -= price;
            target.Get();
        }
    }
}