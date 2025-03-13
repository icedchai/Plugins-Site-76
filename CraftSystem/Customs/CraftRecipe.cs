namespace CraftSystem.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;

    /// <summary>
    /// A class designed to contain all information related to a crafting recipe.
    /// </summary>
    ///
    public class CraftRecipe
    {
        /// <summary>
        /// Contains the submitted items into a crafting table, accessible by their players.
        /// </summary>
        public static Dictionary<Player, HashSet<Item>> PlayerSubmittedItems = new Dictionary<Player, HashSet<Item>>();

        /// <summary>
        /// The dictionary of registered recipes, accessible by their name.
        /// </summary>
        private static Dictionary<string, CraftRecipe> registeredRecipes = new Dictionary<string, CraftRecipe>();

        private HashSet<string> recipeItems = new HashSet<string>();
        private HashSet<string> outputItems = new HashSet<string>();

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
        /// Gets or sets the name this recipe will be referenced under.
        /// </summary>
        public string RecipeName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> required for the recipe.
        /// </summary>
        public HashSet<string> RecipeItems
        {
            get
            {
                HashSet<string> result = new HashSet<string>();
                foreach (string item in recipeItems)
                {
                    result.Add(item.ToLower());
                }

                return result;
            }

            set
            {
                recipeItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> yielded by the recipe.
        /// </summary>
        public HashSet<string> OutputItems
        {
            get
            {
                HashSet<string> result = new HashSet<string>();
                foreach (string item in outputItems)
                {
                    result.Add(item.ToLower());
                }

                return result;
            }

            set
            {
                outputItems = value;
            }
        }

        /// <summary>
        /// Gets all <see cref="ItemType"/>s in the recipe.
        /// </summary>
        /// <returns>HashSet of <see cref="ItemType"/>s in the recipe.</returns>
        ///
        public HashSet<ItemType> GetRecipeItemTypes()
        {
            HashSet<ItemType> items = new HashSet<ItemType>();
            foreach (string item in RecipeItems)
            {
                if (Enum.TryParse<ItemType>(item, true, out ItemType type))
                {
                    items.Add(type);
                }
            }

            return items;
        }

        /// <summary>
        /// Gets all <see cref="CustomItem"/>s in the recipe.
        /// </summary>
        /// <returns>HashSet of <see cref="CustomItem"/>s in the recipe.</returns>
        ///
        public HashSet<CustomItem> GetRecipeCustomItems()
        {
            HashSet<CustomItem> items = new HashSet<CustomItem>();
            uint id;
            foreach (string item in RecipeItems)
            {
                if (!uint.TryParse(item, out id))
                {
                    break;
                }
                else if (CustomItem.TryGet(id, out CustomItem customItem))
                {
                    items.Add(customItem);
                }
            }

            return items;
        }

        /// <summary>
        /// Gets all <see cref="ItemType"/>s in the recipe.
        /// </summary>
        /// <returns>HashSet of <see cref="ItemType"/>s in the recipe.</returns>
        ///
        public HashSet<ItemType> GetOutputItemTypes()
        {
            HashSet<ItemType> items = new HashSet<ItemType>();
            foreach (string item in OutputItems)
            {
                if (Enum.TryParse(item, true, out ItemType type))
                {
                    items.Add(type);
                }
            }

            return items;
        }

        /// <summary>
        /// Gets all <see cref="CustomItem"/> in the output.
        /// </summary>
        /// <returns>HashSet of <see cref="CustomItem"/> in the output.</returns>
        ///
        public HashSet<CustomItem> GetOutputCustomItems()
        {
            HashSet<CustomItem> items = new HashSet<CustomItem>();
            foreach (string item in OutputItems)
            {
                if (uint.TryParse(item, out uint id))
                {
                    if (CustomItem.TryGet(id, out CustomItem customItem))
                    {
                        items.Add(customItem);
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Registers this recipe under <see cref="registeredRecipes"/>.
        /// </summary>
        public void Register()
        {
            if (registeredRecipes.ContainsKey(RecipeName))
            {
                RecipeName += "0";
                Register();
                return;
            }

            registeredRecipes.Add(RecipeName, this);
            Log.Info($"{RecipeName} registered!");
            return;
        }
    }
}
