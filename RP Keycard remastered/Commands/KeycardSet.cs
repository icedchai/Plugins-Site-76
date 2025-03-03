namespace RP_Keycard_remastered.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommandSystem;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Permissions.Extensions;

    /// <summary>
    /// Command for setting properties on a keycard.
    /// </summary>
    public class KeycardSet : ICommand
    {
        /// <inheritdoc/>
        public string Command => "set";

        /// <inheritdoc/>
        public string[] Aliases => new string[] { };

        /// <inheritdoc/>
        public string Description => "Sets certain properties on a keycard.";

        /// <inheritdoc/>
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
                if (arguments.Count < 1)
                {
                    return false;
                }

                Plugin.SerialToCards[keycard.Serial].Name = arguments.At(1);
                response = $"<color=yellow>Set keycard {keycard.Type} Name to {arguments.At(1)}</color>";
                return true;
            }

            if (arguments.At(0) == "permissions")
            {
                if (arguments.Count < 1)
                {
                    return false;
                }

                keycard.Permissions = (KeycardPermissions)Enum.Parse(typeof(KeycardPermissions), arguments.At(1), true);
                response = $"<color=yellow>Set keycard {keycard.Type} perms to {keycard.Permissions}</color>";
                return true;
            }

            return false;
        }
    }
}
