using Exiled.API.Features;
using RP_Keycard_remastered.Customs;
using RP_Keycard_remastered.EventHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerHandler = Exiled.Events.Handlers.Player;
namespace RP_Keycard_remastered
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "RP Keycard Remastered";
        public override string Author => "icedchqi, original by Saskyc";
        public override string Prefix => "rp_keycard";
        public override Version Version => new Version(1,0,0);

        public static Dictionary<int, KeycardContainer> serialToCards = new Dictionary<int, KeycardContainer>();
        public PlayerEventHandler _playerEventHandler;
        public static Plugin instance = null;
        public override void OnEnabled()
        {
            instance = this;
            base.OnEnabled();
            RegisterEvents();
        }
        public override void OnDisabled()
        {
            instance = null;
            base.OnDisabled();
            UnregisterEvents();
        }
        public void RegisterEvents()
        {
            _playerEventHandler = new PlayerEventHandler();
            PlayerHandler.ChangingItem += _playerEventHandler.OnChangingItem;
            PlayerHandler.ItemAdded += _playerEventHandler.OnAddedItem;
        }
        public void UnregisterEvents()
        {
            PlayerHandler.ChangingItem -= _playerEventHandler.OnChangingItem;
            PlayerHandler.ItemAdded -= _playerEventHandler.OnAddedItem;
            _playerEventHandler = null;
        }
    }
}
