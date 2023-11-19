using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
    [Serializable]
    public class PlayerData : UnitData
    {
        [SerializeField]
        public int money;

    
        public PlayerData() : base()
        {
            manaPerGem = 20;
            money = 0;
        }
    }
}