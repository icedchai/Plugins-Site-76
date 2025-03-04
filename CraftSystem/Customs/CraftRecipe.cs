namespace CraftSystem.Customs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;

    /// <summary>
    /// A class designed to contain all information related to a crafting recipe.
    /// </summary>
    ///
    public class CraftRecipe
    {
        /// <summary>
        /// Gets or sets the name this recipe will be referenced under.
        /// </summary>
        public string RecipeName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> required for the recipe.
        /// </summary>
        public HashSet<string> RecipeItems { get; set; } = new HashSet<string>();

        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> yielded by the recipe.
        /// </summary>
        public HashSet<string> OutputItems { get; set; } = new HashSet<string>();

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
            foreach (string item in RecipeItems)
            {
                if (CustomItem.TryGet(item, out CustomItem customItem))
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
        /// Registers this recipe under <see cref="Plugin.RegisteredRecipes"/>.
        /// </summary>
        public void Register()
        {
            if (Plugin.RegisteredRecipes.ContainsKey(RecipeName))
            {
                RecipeName += "0";
                Register();
                return;
            }

            Plugin.RegisteredRecipes.Add(RecipeName, this);
            Log.Info($"{RecipeName} registered!");
            return;
        }
    }
}
