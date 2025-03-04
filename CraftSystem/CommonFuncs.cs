namespace CraftSystem
{
    using Exiled.API.Features;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Common functions used throughout the plugin.
    /// </summary>
    public static class CommonFuncs
    {
        /// <summary>
        /// Checks if the given player is eligible to craft.
        /// </summary>
        /// <param name="player">The given player.</param>
        /// <returns>Whether the player is eligible to craft.</returns>
        public static bool CheckCraftEligible(Player player)
        {
            // TODO: ADD CHECK FOR CRAFTABILITY.
            return true;
        }
    }
}
