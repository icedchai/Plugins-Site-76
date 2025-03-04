namespace CraftSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CraftSystem.Customs;
    using Exiled.API.Features;
    using InventorySystem.Items.Usables;

    /// <inheritdoc/>
    public class Plugin : Plugin<Config>
    {
        /// <summary>
        /// The singleton.
        /// </summary>
        public static Plugin Instance = null;

        /// <summary>
        /// The dictionary of registered recipes, accessible by their name.
        /// </summary>
        public static Dictionary<string, CraftRecipe> RegisteredRecipes = new Dictionary<string, CraftRecipe>();

        /// <inheritdoc/>
        public override string Name => "icedchqi's Crafting System";

        /// <inheritdoc/>
        public override string Author => "icedchqi";

        /// <inheritdoc/>
        public override string Prefix => "crafting";

        /// <inheritdoc/>
        public override Version Version => new Version(1, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
            RegisterEvents();
            foreach (CraftRecipe recipe in Config.Recipes)
            {
                recipe.Register();
            }
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
            UnregisterEvents();
        }

        /// <summary>
        /// Registers the plugin's events.
        /// </summary>
        public void RegisterEvents()
        {

        }

        /// <summary>
        /// Unregisters the plugin's events.
        /// </summary>
        public void UnregisterEvents()
        {

        }
    }
}
