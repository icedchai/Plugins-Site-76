namespace CraftSystem.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdminToys;
    using CraftSystem.Customs;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Toys;
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

            e.IsAllowed = false;

            // TODO: add proper Craft Finalization process
            if (e.IsThrown)
            {
                CraftManager.FinalizeCraft(player);
                return;
            }

            if (!CraftManager.PlayerSubmittedItems.TryGetValue(player, out HashSet<Item> items))
            {
                items = new HashSet<Item> { e.Item };
                CraftManager.PlayerSubmittedItems.Add(player, items);
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

                CraftManager.PlayerSubmittedItems[player] = items;
            }

            // Temporary debug feature
            // TODO: remove
            strings = CommonFuncs.HashItemsToHashString(CraftManager.PlayerSubmittedItems[player]);
            string hint = "Crafting materials:";
            foreach (string s in strings)
            {
                hint += $"\n{s}";
            }

            player.ShowHint(hint);
        }
    }
}
