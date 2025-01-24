using System;
using System.Collections.Generic;
using Other;
using UnityRandom = UnityEngine.Random;

namespace Map.Nodes
{
    public class SecretNode : Node
    {
        private List<Action> Rooms => new() { Treasure, Battle, Shop };
        // TODO add Plot ("Slay the Spire"-like)

        protected override void Action()
        {
            UnityRandom.InitState(Seed);

            var chosenRoom = Tools.Random.RandomChoose(Rooms);
            chosenRoom?.Invoke();

            Tools.Random.ResetRandom();
        }

        private void Treasure()
        {
            RoomLoader.LoadTreasure(Layer, Seed);
        }

        private void Battle()
        {
            RoomLoader.LoadBattle(Layer, Seed, false);
        }

        private void Shop()
        {
            RoomLoader.LoadShop(Layer, Seed);
        }
    }
}