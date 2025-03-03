namespace RP_Keycard_remastered.EventHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.Events.EventArgs.Map;
    using Exiled.Events.EventArgs.Server;

    /// <summary>
    /// The server & map event handler.
    /// </summary>
    public class ServerEventHandler
    {

        /// <summary>
        /// Removes destroyed pickups from <see cref="Plugin.SerialToCards"/>.
        /// </summary>
        /// <param name="e">The event args.</param>
        public void OnPickupDestroyed(PickupDestroyedEventArgs e)
        {
            if (Plugin.SerialToCards.ContainsKey((int)e.Pickup.Serial))
            {
                Plugin.SerialToCards.Remove((int)e.Pickup.Serial);
            }
        }

        /// <summary>
        /// Clears <see cref="Plugin.SerialToCards"/> on round restart.
        /// </summary>
        /// <param name="e">The event args.</param>
        public void OnServerRestart(RoundEndedEventArgs e)
        {
            Plugin.SerialToCards.Clear();
        }
    }
}
