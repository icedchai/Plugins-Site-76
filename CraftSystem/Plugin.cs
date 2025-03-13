namespace CraftSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CraftSystem.Customs;
    using CraftSystem.EventHandlers;
    using Exiled.API.Features;
    using InventorySystem.Items.Usables;
    using PlayerHandler = Exiled.Events.Handlers.Player;

    /// <inheritdoc/>
    public class Plugin : Plugin<Config>
    {
        /// <summary>
        /// Event handler.
        /// </summary>
        private static CraftingEventHandlers craftingEventHandlers = null;

        /// <summary>
        /// The singleton.
        /// </summary>
        public static Plugin Instance = null;

        /// <inheritdoc/>
        public override string Name => "icedchqi's Crafting System";

        /// <inheritdoc/>
        public override string Author => "icedchqi";

        /// <inheritdoc/>
        public override string Prefix => "crafting";

        /// <inheritdoc/>
        public override Version Version => new Version(1, 0, 0);

        public static List<CraftingTable> CraftingTables = new List<CraftingTable>();

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
            craftingEventHandlers = new ();
            PlayerHandler.DroppingItem += craftingEventHandlers.OnDroppingItem;
        }

        /// <summary>
        /// Unregisters the plugin's events.
        /// </summary>
        public void UnregisterEvents()
        {
            PlayerHandler.DroppingItem -= craftingEventHandlers.OnDroppingItem;
            craftingEventHandlers = null;
        }
    }
}
