using System;
using System.Collections.Generic;

namespace Map.Nodes
{
    public class SecretNode : Node
    {
        private List<Action> Rooms => new() { Treasure, Battle, Shop };
        // TODO add PlotRoom ("Slay the Spire"-like)
        
        protected override void Action()
        {
            UnityEngine.Random.InitState(seed);
            
            // TODO Choose and create room from Rooms
            
            Other.Tools.Random.ResetRandom();
        }

        private void Treasure() => RoomLoader.LoadTreasure(layer, seed);

        private void Battle() => RoomLoader.LoadBattle(layer, seed, false);

        private void Shop() => RoomLoader.LoadShop(layer, seed);
    }
}