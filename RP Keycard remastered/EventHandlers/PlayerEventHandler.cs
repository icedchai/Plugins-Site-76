using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using RP_Keycard_remastered.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Keycard_remastered.EventHandlers
{
    public class PlayerEventHandler
    {
        public void OnAddedItem(ItemAddedEventArgs e)
        {
            if (!e.Item.Type.IsKeycard()) return;
            Player player = e.Player;
            ushort serial = e.Item.Serial;
            Log.Debug($"Item not registered {serial}");
            if (e.Pickup is null)
            {
                if (!Plugin.serialToCards.ContainsKey(serial))
                {
                    KeycardContainer container = new KeycardContainer();
                    container.Name = player.DisplayNickname;
                    Plugin.serialToCards.Add(serial, container);
                    Log.Debug($"Keycard registered {serial}, {container.Name}");
                }
            }
        }
        public void OnChangingItem(ChangingItemEventArgs e)
        {
            if (!e.Item.Type.IsKeycard()) return;
            Player player = e.Player;
            ushort serial = e.Item.Serial;
            if (Plugin.serialToCards.ContainsKey(serial))
            {
                CommonFuncs.DisplayCard(player, serial);
            }
        }
    }
}
