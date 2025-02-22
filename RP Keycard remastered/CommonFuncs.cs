using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using RP_Keycard_remastered.Customs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Keycard_remastered
{
    public class CommonFuncs
    {
        static Config config => Plugin.instance.Config;
        public static string PermissionsToString(KeycardPermissions permissions)
        {
            string output = "";
            //This code is real ugly, but I can't think of anything better...
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
        private static string AppendPermission(KeycardPermissions permToAdd, string hint) 
        {
            if (!config.PermissionsToWord.TryGetValue(permToAdd, out string word))
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
        public static void DisplayCard(Player player, ushort serial)
        {
            Item item = Item.Get(serial);
            if (item is null || !(item is Keycard keycard)) return;
            if (!Plugin.serialToCards.TryGetValue(serial, out KeycardContainer container)) return;
            string hint = config.DisplayHint;
            hint = hint.Replace("%name%", container.Name);
            hint = hint.Replace("%permissions%", PermissionsToString(keycard.Permissions));
            player.ShowHint(hint);
        }
    }
}
