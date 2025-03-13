namespace CraftSystem
{
    using CraftSystem.Customs;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;
    using InventorySystem.Items.Firearms.Attachments;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;
    using Utf8Json.Unity;

    /// <summary>
    /// Common functions used throughout the plugin.
    /// </summary>
    public static class CommonFuncs
    {
        /// <summary>
        /// Checks if the given player is eligible to craft.
        /// </summary>
        /// <param name="player">The given player.</param>
        /// <returns>Whether the player is eligible to craft.</returns>
        public static bool CheckCraftEligible(Player player)
        {
            foreach (CraftingTable table in Plugin.CraftingTables)
            {
                if (Vector3.Distance(player.Position, table.Position) < 3f)
                {
                    return true;
                }
            }

            CraftRecipe.PlayerSubmittedItems.Clear();
            return false;
        }

        /// <summary>
        /// Convert a HashSet of items to a HashSet of strings.
        /// </summary>
        /// <param name="items">The HashSet of items to convert to strings.</param>
        /// <returns>Returns the hashset of items as strings.</returns>
        public static HashSet<string> HashItemsToHashString(HashSet<Item> items)
        {
            HashSet<string> result = new HashSet<string>();
            foreach (Item item in items)
            {
                if (CustomItem.TryGet(item, out CustomItem citem))
                {
                    result.Add(citem.Id.ToString());
                }
                else
                {
                    result.Add(item.Type.ToString().ToLower());
                }
            }

            return result;
        }

        /// <summary>
        /// Checks whether <paramref name="items"/> matches any crafting recipe currently available.
        /// </summary>
        /// <param name="items">The submitted hash set of items represented as strings.</param>
        /// <returns>A crafting recipe whose RecipeItems matches <paramref name="items"/>, or null.</returns>
        public static CraftRecipe GetCraftRecipe(HashSet<string> items)
        {
            foreach (CraftRecipe recipe in CraftRecipe.RegisteredRecipes)
            {
                if (items.SetEquals(recipe.RecipeItems))
                {
                    return recipe;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks whether <paramref name="items"/> matches any crafting recipe currently available.
        /// </summary>
        /// <param name="items">The submitted hash set of items.</param>
        /// <returns>A crafting recipe whose RecipeItems matches <paramref name="items"/>, or null.</returns>
        public static CraftRecipe GetCraftRecipe(HashSet<Item> items)
        {
            return GetCraftRecipe(HashItemsToHashString(items));
        }
    }
}
