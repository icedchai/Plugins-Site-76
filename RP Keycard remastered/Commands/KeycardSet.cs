using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Keycard_remastered.Commands
{
    public class KeycardSet : ICommand
    {
        public string Command => "set";

        public string[] Aliases => null;

        public string Description => "Sets certain properties on a keycard.";
        // keycard set (name/permissions) (input)
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("rpk.set"))
            {
                response = $"Insufficient permissions! Permission: rpk.set";
                return false;
            }
            Player target = Player.Get(sender);
            if (target is null) 
            {
                response = "Target does not exist in-game!";
                return false;
            }
            Keycard keycard;
            if (target.Items.Where(i => i.IsKeycard).First() is Keycard _keycard)
            {
                keycard = _keycard;
            }
            else
            {
                response = "Target does not own a keycard.";
                return false;
            }
            response = "USAGE: set (name/permissions) (input)";
            if (arguments.At(0) == "name")
            {
                if (arguments.Count < 1) return false;
                Plugin.serialToCards[keycard.Serial].Name = arguments.At(1);
                response = $"Set keycard Name to {arguments.At(1)}";
                return true;
            }
            if (arguments.At(0) == "permissions")
            {
                if (arguments.Count < 1) return false;
                keycard.Permissions = (KeycardPermissions)Enum.Parse(typeof(KeycardPermissions), arguments.At(1), true);
                response = $"Set keycard perms to {keycard.Permissions}";
                return true;
            }
            return false;
        }
    }
}
