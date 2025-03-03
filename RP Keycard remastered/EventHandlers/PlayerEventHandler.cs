namespace RP_Keycard_remastered.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Pickups;
    using Exiled.Events.EventArgs.Player;
    using RP_Keycard_remastered.Customs;

    /// <summary>
    /// The player event handler.
    /// </summary>
    public class PlayerEventHandler
    {
        public void OnAddedItem(ItemAddedEventArgs e)
        {
            if (!e.Item.Type.IsKeycard())
            {
                return;
            }

            Player player = e.Player;
            ushort serial = e.Item.Serial;
            Log.Debug($"Item not registered {serial}");
            if (e.Pickup is null)
            {
                if (!Plugin.SerialToCards.ContainsKey(serial))
                {
                    KeycardContainer container = new KeycardContainer();
                    container.Name = player.DisplayNickname;
                    Plugin.SerialToCards.Add(serial, container);
                    Log.Debug($"Keycard registered {serial}, {container.Name}");
                }
            }
        }

        public void OnChangingItem(ChangingItemEventArgs e)
        {
            if (e.Item is null || !e.Item.Type.IsKeycard())
            {
                return;
            }

            Player player = e.Player;
            ushort serial = e.Item.Serial;
            if (Plugin.SerialToCards.ContainsKey(serial))
            {
                CommonFuncs.DisplayCard(player, serial);
            }
        }
    }
}
