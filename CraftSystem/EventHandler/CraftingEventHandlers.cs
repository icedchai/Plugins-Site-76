namespace CraftSystem.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;

    /// <summary>
    /// Contains all crafting-related event handlers.
    /// </summary>
    public class CraftingEventHandlers
    {
        /// <summary>
        /// Contains the submitted items into a crafting table.
        /// </summary>
        private Dictionary<Player, HashSet<Item>> playerSubmittedItems = new Dictionary<Player, HashSet<Item>>();

        /// <summary>
        /// Convert a HashSet of items to a HashSet of strings.
        /// </summary>
        /// <param name="items">The HashSet of items to convert to strings.</param>
        /// <returns>Returns the hashset of items as strings.</returns>
        private HashSet<string> HashItemsToHashString(HashSet<Item> items)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (Item item in items)
            {
                if (CustomItem.TryGet(item, out CustomItem citem))
                {
                    result.Add(citem.Name.ToLower());
                }
                else
                {
                    result.Add(item.Type.ToString().ToLower());
                }
            }

            return result;
        }

        /// <summary>
        /// Handles submitting items to the crafting bench.
        /// </summary>
        /// <param name="e">The event args.</param>
        public void OnDroppingItem(DroppingItemEventArgs e)
        {
            Player player = e.Player;
            if (!CommonFuncs.CheckCraftEligible(player))
            {
                return;
            }

            e.IsAllowed = false;
            if (!playerSubmittedItems.TryGetValue(player, out HashSet<Item> items))
            {
                items = new HashSet<Item> { e.Item };
                playerSubmittedItems.Add(player, items);
            }
            else
            {
                items.Add(e.Item);
                playerSubmittedItems[player] = items;
            }
        }
    }
}
