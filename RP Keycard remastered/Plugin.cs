namespace RP_Keycard_remastered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using RP_Keycard_remastered.Customs;
    using RP_Keycard_remastered.EventHandlers;
    using PlayerHandler = Exiled.Events.Handlers.Player;

    /// <inheritdoc/>
    public class Plugin : Plugin<Config>
    {
        /// <inheritdoc/>
        public override string Name => "RP Keycard Remastered";

        /// <inheritdoc/>
        public override string Author => "icedchqi, original by Saskyc";

        /// <inheritdoc/>
        public override string Prefix => "rp_keycard";

        /// <inheritdoc/>
        public override Version Version => new Version(1,0,0);

        /// <summary>
        /// The dictionary keeping track of the item serials compared to cards.
        /// </summary>
        public static Dictionary<int, KeycardContainer> SerialToCards = new Dictionary<int, KeycardContainer>();

        /// <summary>
        /// The singleton.
        /// </summary>
        public static Plugin Instance;

        /// <summary>
        /// The <see cref="PlayerEventHandler"/> instance.
        /// </summary>
        private PlayerEventHandler playerEventHandler;

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
            RegisterEvents();
        }

    /// <inheritdoc/>
        public override void OnDisabled()
        {
            Instance = null;
            base.OnDisabled();
            UnregisterEvents();
        }

        /// <summary>
        /// Registers plugin events.
        /// </summary>
        public void RegisterEvents()
        {
            playerEventHandler = new PlayerEventHandler();
            PlayerHandler.ChangingItem += playerEventHandler.OnChangingItem;
            PlayerHandler.ItemAdded += playerEventHandler.OnAddedItem;
        }

        /// <summary>
        /// Unregisters plugin events.
        /// </summary>
        public void UnregisterEvents()
        {
            PlayerHandler.ChangingItem -= playerEventHandler.OnChangingItem;
            PlayerHandler.ItemAdded -= playerEventHandler.OnAddedItem;
            playerEventHandler = null;
        }
    }
}
