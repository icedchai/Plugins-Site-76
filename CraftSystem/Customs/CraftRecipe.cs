using Exiled.CustomItems.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftSystem.Customs
{
    /// <summary>
    /// A class designed to contain all information related to a crafting recipe.
    /// </summary>
    ///
    public class CraftRecipe
    {
        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> required for the recipe.
        /// </summary>
        public List<string> RecipeItems { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the list of items represented by <see cref="string"/> yielded by the recipe.
        /// </summary>
        public List<string> OutputItems { get; set; } = new List<string>();

        /// <summary>
        /// Gets all <see cref="ItemType"/>s in the recipe.
        /// </summary>
        /// <returns>List of <see cref="ItemType"/>s in the recipe.</returns>
        ///
        public List<ItemType> GetRecipeItemTypes
        {
            get
            {
                List<ItemType> items = new List<ItemType>();
                foreach (string item in this.RecipeItems)
                {
                    if (Enum.TryParse<ItemType>(item, true, out ItemType type))
                    {
                        items.Add(type);
                    }
                }

                return items;
            }
        }

        /// <summary>
        /// Gets all <see cref="CustomItem"/>s in the recipe.
        /// </summary>
        /// <returns>List of <see cref="CustomItem"/>s in the recipe.</returns>
        ///
        public List<CustomItem> GetRecipeCustomItems
        {
            get 
            { 
                List<CustomItem> items = new List<CustomItem>();
                foreach (string item in RecipeItems)
                {
                    if(uint.TryParse(item, out uint id))
                    {
                        if(CustomItem.TryGet(id, out CustomItem customItem))
                        {
                            items.Add(customItem);
                        }
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// Gets all <see cref="ItemType"/>s in the recipe.
        /// </summary>
        /// <returns>List of <see cref="ItemType"/>s in the recipe.</returns>
        ///
        public List<ItemType> GetOutputItemTypes
        {
            get
            {
                List<ItemType> items = new List<ItemType>();
                foreach (string item in OutputItems)
                {
                    if (Enum.TryParse<ItemType>(item, true, out ItemType type))
                    {
                        items.Add(type);
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// Gets all <see cref="CustomItem"/>s in the output.
        /// </summary>
        /// <returns>List of <see cref="CustomItem"/>s in the output.</returns>
        ///
        public List<CustomItem> GetOutputCustomItems
        {
            get
            {
                List<CustomItem> items = new List<CustomItem>();
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
        }
    }
}
