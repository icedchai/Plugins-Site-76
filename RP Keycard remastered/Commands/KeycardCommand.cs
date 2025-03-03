namespace RP_Keycard_remastered.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommandSystem;
    using Exiled.Permissions.Extensions;

    /// <summary>
    /// Parent command for keycards.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class KeycardCommand : ParentCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeycardCommand"/> class.
        /// </summary>
        public KeycardCommand() => LoadGeneratedCommands();

        /// <inheritdoc/>
        public override string Command => "keycard";

        /// <inheritdoc/>
        public override string[] Aliases => new string[]{"kc", "customcard"};

        /// <inheritdoc/>
        public override string Description => "Get or set information about a certain keycard.";

        /// <inheritdoc/>
        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new KeycardGet());
            RegisterCommand(new KeycardSet());
        }

        /// <inheritdoc/>
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
