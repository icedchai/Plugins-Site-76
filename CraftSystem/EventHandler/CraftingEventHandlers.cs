namespace CraftSystem.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CraftSystem.Customs;
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

            HashSet<string> strings;

            // TODO: add proper Craft Finalization process
            if (e.IsThrown)
            {
                CraftRecipe.FinalizeCraft(player);
                return;
            }

            e.IsAllowed = false;
            if (!CraftRecipe.PlayerSubmittedItems.TryGetValue(player, out HashSet<Item> items))
            {
                items = new HashSet<Item> { e.Item };
                CraftRecipe.PlayerSubmittedItems.Add(player, items);
            }
            else
            {
                if (items.Contains(e.Item))
                {
                    items.Remove(e.Item);
                }
                else
                {
                    items.Add(e.Item);
                }

                CraftRecipe.PlayerSubmittedItems[player] = items;
            }

            // Temporary debug feature
            // TODO: remove
            strings = CommonFuncs.HashItemsToHashString(CraftRecipe.PlayerSubmittedItems[player]);
            string hint = string.Empty;
            foreach (string s in strings)
            {
                hint += $"\n{s}";
            }

            player.ShowHint(hint);
        }
    }
}
