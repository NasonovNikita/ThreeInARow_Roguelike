using System;
using Battle.Units;
using Other;

namespace Battle.Items
{
    /// Base class for all Items. See
    /// <see cref="Get"/>
    /// , its base function.
    [Serializable]
    public abstract class Item : LootItem
    {
        /// Adds an Item to
        /// <see cref="PlayerData">Player's data</see>
        /// and activates it.
        public override void Get()
        {
            Player.Data.items.Add(this);
            OnGet();
        }

        public abstract void OnGet();
    }
}