namespace CraftSystem
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

    /// <summary>
    /// The managerial class for 
    /// </summary>
    public static class CraftManager
    {
        /// <summary>
        /// Contains the submitted items into a crafting table, accessible by their players.
        /// </summary>
        public static Dictionary<Player, HashSet<Item>> PlayerSubmittedItems = new Dictionary<Player, HashSet<Item>>();

        /// <summary>
        /// The dictionary of registered recipes, accessible by their name.
        /// </summary>
        private static Dictionary<string, CraftRecipe> registeredRecipes = new Dictionary<string, CraftRecipe>();

        /// <summary>
        /// Gets a hash set of all <see cref="CraftRecipe"/>s currently registered.
        /// </summary>
        public static HashSet<CraftRecipe> RegisteredRecipes => registeredRecipes.Values.ToHashSet();

        /// <summary>
        /// Clears a player's submitted items.
        /// </summary>
        /// <param name="player">The player to clear.</param>
        public static void ClearPlayerSubmittedItems(Player player)
        {
            if (player is null)
            {
                return;
            }

            if (!PlayerSubmittedItems.TryGetValue(player, out var submittedItems))
            {
                return;
            }

            PlayerSubmittedItems.Remove(player);
        }

        /// <summary>
        /// Gives a player the recipe output, and removes their crafting materials.
        /// </summary>
        /// <param name="player">The player to finalize the craft for.</param>
        public static void FinalizeCraft(Player player)
        {
            if (player is null)
            {
                return;
            }

            if (!PlayerSubmittedItems.TryGetValue(player, out var submittedItems))
            {
                return;
            }

            HashSet<string> strings = CommonFuncs.HashItemsToHashString(submittedItems);
            CraftRecipe recipe = CommonFuncs.GetCraftRecipe(strings);

            // Null check to avoid NRE.
            if (recipe is null)
            {
                return;
            }

            // Destroys all crafting materials.
            foreach (Item item in PlayerSubmittedItems[player])
            {
                player.RemoveItem(item);
            }

            PlayerSubmittedItems.Clear();

            if (player.Items.Count + recipe.OutputItems.Count > 8)
            {
                return;
            }

            // Give all custom items.
            foreach (CustomItem citem in recipe.GetOutputCustomItems())
            {
                citem.Give(player);
            }

            // Give all vanilla items.
            foreach (ItemType itemt in recipe.GetOutputItemTypes())
            {
                player.AddItem(itemt);
            }

            return;
        }

        /// <summary>
        /// Attempts to register a recipe.
        /// </summary>
        /// <param name="recipe">The recipe to register.</param>
        /// <returns>Whether the recipe could be registered or not.</returns>
        public static bool TryRegisterRecipe(CraftRecipe recipe)
        {
            if (recipe is null || registeredRecipes.ContainsKey(recipe.RecipeName))
            {
                return false;
            }

            registeredRecipes.Add(recipe.RecipeName, recipe);
            return true;
        }
    }
}
