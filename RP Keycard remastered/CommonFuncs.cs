namespace RP_Keycard_remastered
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using RP_Keycard_remastered.Customs;

    /// <summary>
    /// Common functions this plugin uses.
    /// </summary>
    public static class CommonFuncs
    {
        private static Config Config => Plugin.Instance.Config;

        /// <summary>
        /// Converts a <see cref="KeycardPermissions"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="permissions">The <see cref="KeycardPermissions"/> to convert to <see cref="string"/>.</param>
        /// <returns>A <see cref="string"/> representative of <paramref name="permissions"/>.</returns>
        public static string PermissionsToString(KeycardPermissions permissions)
        {
            string output = string.Empty;

            // This code is real ugly, but I can't think of anything better...
            if (permissions.HasFlag(KeycardPermissions.ContainmentLevelThree))
            {
                output = AppendPermission(KeycardPermissions.ContainmentLevelThree, output);
            }
            else if (permissions.HasFlag(KeycardPermissions.ContainmentLevelTwo))
            {
                output = AppendPermission(KeycardPermissions.ContainmentLevelTwo, output);
            }
            else if (permissions.HasFlag(KeycardPermissions.ContainmentLevelOne))
            {
                output = AppendPermission(KeycardPermissions.ContainmentLevelOne, output);
            }

            if (permissions.HasFlag(KeycardPermissions.ArmoryLevelThree))
            {
                output = AppendPermission(KeycardPermissions.ArmoryLevelThree, output);
            }
            else if (permissions.HasFlag(KeycardPermissions.ArmoryLevelTwo))
            {
                output = AppendPermission(KeycardPermissions.ArmoryLevelTwo, output);
            }
            else if (permissions.HasFlag(KeycardPermissions.ArmoryLevelOne))
            {
                output = AppendPermission(KeycardPermissions.ArmoryLevelOne, output);
            }

            if (permissions.HasFlag(KeycardPermissions.ExitGates))
            {
                output = AppendPermission(KeycardPermissions.ExitGates, output);
            }

            if (permissions.HasFlag(KeycardPermissions.AlphaWarhead))
            {
                output = AppendPermission(KeycardPermissions.AlphaWarhead, output);
            }

            if (permissions.HasFlag(KeycardPermissions.Checkpoints))
            {
                output = AppendPermission(KeycardPermissions.Checkpoints, output);
            }

            if (permissions.HasFlag(KeycardPermissions.Intercom))
            {
                output = AppendPermission(KeycardPermissions.Intercom, output);
            }

            return output;
        }

        /// <summary>
        /// Displays a keycard's permissions to a player.
        /// </summary>
        /// <param name="player">The player to show the permissions to.</param>
        /// <param name="serial">The serial number of the keycard.</param>
        public static void DisplayCard(Player player, ushort serial)
        {
            Item item = Item.Get(serial);
            if (item is null || !(item is Keycard keycard))
            {
                return;
            }

            if (!Plugin.SerialToCards.TryGetValue(serial, out KeycardContainer container))
            {
                return;
            }

            string hint = Config.DisplayHint;
            hint = hint.Replace("%name%", container.Name);
            hint = hint.Replace("%permissions%", PermissionsToString(keycard.Permissions));
            player.ShowHint(hint, 9);
        }

        /// <summary>
        /// Appends a <see cref="KeycardPermissions"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="permToAdd">The <see cref="KeycardPermissions"/> to append to the <see cref="string"/>.</param>
        /// <param name="hint">The <see cref="string"/>.</param>
        /// <returns>A <see cref="string"/> with the permission to the end.</returns>
        private static string AppendPermission(KeycardPermissions permToAdd, string hint)
        {
            if (!Config.PermissionsToWord.TryGetValue(permToAdd, out string word))
            {
                hint += $"{permToAdd}\n";
                return hint;
            }
            else
            {
                hint += $"{word}\n";
                return hint;
            }
        }
    }
}
