using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Rocket.Unturned.Permissions;
using SDG.Unturned;
using Rocket.API.Extensions;

namespace HelperPlugin.Commands
{
    class HoldingCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "holding";

        public string Help => "Tells you the ID of the item your holding";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "helper.holding" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            ushort itemId = player.Player.equipment.itemID;

            if (itemId == 0)
            {
                UnturnedChat.Say(caller, "You don't seem to have an item equiped");
            }
            else
            {
                UnturnedChat.Say(caller, $"You are holding an item with the ID: {itemId}");
            }

        }
    }
}
