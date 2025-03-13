namespace CraftSystem.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdminToys;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.API.Features.Toys;
    using UnityEngine;

    /// <summary>
    /// Command to spawn a crafting bench.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SpawnCraftingBenchCommand : ICommand
    {
        /// <inheritdoc/>
        public string Command => "benchspawn";

        /// <inheritdoc/>
        public string[] Aliases => new string[] { "craftingbench", "spawncraftingbench", "spawntable" };

        /// <inheritdoc/>
        public string Description => "Spawn a crafting bench at your location.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            Plugin.CraftingTables.Add(new (player.Position, PrimitiveType.Cube));
            response = "Added new crafting table";
            return true;
        }
    }
}
