using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Keycard_remastered.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class KeycardCommand : ParentCommand
    {
        public KeycardCommand() => LoadGeneratedCommands();
        public override string Command => "keycard";

        public override string[] Aliases => new string[]{"kc", "customcard"};

        public override string Description => "Get or set information about a certain keycard.";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new KeycardGet());
            RegisterCommand(new KeycardSet());
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "\nPlease enter a valid subcommand:";

            foreach (ICommand command in AllCommands)
            {
                if (sender.CheckPermission($"mpr.{command.Command}"))
                {
                    response += $"\n\n- {command.Command} ({string.Join(", ", command.Aliases)})\n{command.Description}";
                }
            }

            return false;
        }
    }
}
