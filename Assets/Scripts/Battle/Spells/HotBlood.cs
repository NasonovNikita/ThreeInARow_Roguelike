using Battle.BattleEventHandlers;
using Battle.Modifiers;
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

            attachedUnit.mana -= useCost;
            
            ApplyToDamage(attachedUnit, new Modifier(-1, ModType.Add, value: value), ModAffect.ValueGet);
            attachedUnit.stateModifiers.Add(new Modifier(count, ModType.Ignition));
            new RandomIgnitionEvent(chance, attachedUnit, count);
        }
    }
}