using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "HotBlood", menuName = "Spells/HotBlood")]
    public class HotBlood : Spell
    {
        [SerializeField] private int chance;
        
        public override void Cast()
        {
            if (CantCast()) return;

            attachedUnit.mana.Waste(useCost);
            
            attachedUnit.AddDamageMod(new DamageMod(-1, ModType.Add, value: value));
            attachedUnit.AddMod(new Modifier(count, ModType.Ignition));
            new RandomIgnitionEvent(chance, attachedUnit, count);
        }
    }
}