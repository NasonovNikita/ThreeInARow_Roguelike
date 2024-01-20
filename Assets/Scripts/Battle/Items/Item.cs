using System;
using Battle.Units;
using Other;

namespace Battle.Items
{
    [Serializable]
    public abstract class Item : LootItem
    {
        public abstract void Use(Unit unitBelong);

        public override void Get()
        {
            Player.data.items.Add(this);
            base.Get();
        }
    }
}