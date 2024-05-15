using System.Collections.Generic;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace UI.Battle.HUD
{
    public class StatHUDDataContainer : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        
        [SerializeField] private StatHUDData hpStatHUDData;
        [SerializeField] private StatHUDData manaStatHUDData;

        public List<(Stat, StatHUDData)> StatHUDDataList => new()
        {
            (unit.hp, hpStatHUDData),
            (unit.mana, manaStatHUDData)
        };
    }
}