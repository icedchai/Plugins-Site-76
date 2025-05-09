﻿namespace RP_Keycard_remastered.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using Exiled.Permissions.Extensions;

    /// <summary>
    /// Command for getting information about a player's keycards.
    /// </summary>
    public class KeycardGet : ICommand
    {
        /// <inheritdoc/>
        public string Command => "get";

        /// <inheritdoc/>
        public string[] Aliases => new string[] { };

        /// <inheritdoc/>
        public string Description => "Gets certain information about a keycard. If no player is specified, will use sender as target.";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("rpk.get"))
            {
                response = $"Insufficient permissions! Permission: rpk.get";
                return false;
            }

            if (arguments.Count < 1) {
                response = $"USAGE: get (OPTIONAL: player identifier)";
            }

            Player target;
            if (arguments.Count > 1)
            {
                target = Player.GetProcessedData(arguments, 0).First();
            }
            else
            {
                target = Player.Get(sender);
            }

            if (target is null)
            {
                response = "Target does not exist in-game!";
                return false;
            }

            List<Keycard> cards = new();
            foreach (Item item in target.Items)
            {
                if (item is Keycard keycard)
                {
                    cards.Add(keycard);
                }
            }

            if (cards.IsEmpty())
            {
                response = $"Specified player does not have a keycard!";
                return false;
            }

            response = $"Keycard info for every keycard {target.Nickname} has:";
            foreach (Keycard card in cards)
            {
                response += $"<color=yellow>\n{card.Type} \n(Serial: {card.Serial})" +
                    $"\n(Card Name: {Plugin.SerialToCards[card.Serial].Name})" +
                    $"\nPermissions: {card.Permissions}</color>";
            }

            return true;

        }
    }
}
