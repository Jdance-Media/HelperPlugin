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
using Rocket.API.Extensions;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace HelperPlugin.Commands
{
    class EscapeCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "escape";

        public string Help => "Heals a player and puts them in vanish. When you want to be shown again use /escape exit.";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "helper.escape" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            string first = command.GetParameterString(0);
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length < 1)
            {
                player.Heal(100);
                player.Bleeding = false;
                player.Broken = false;
                player.Infection = 0;
                player.Hunger = 0;
                player.Thirst = 0;
                player.Features.VanishMode = true;
                UnturnedChat.Say(caller, $"Put {player.DisplayName} into escape mode. Do '/escape exit' to exit!");
                Logger.Log($"{player.DisplayName} has been put into escape mode!");
            }
            if (command.Length >= 1)
            {
                if (first == "exit")
                {
                    player.Features.VanishMode = false;
                    UnturnedChat.Say(caller, "You have been taken out of escape mode!");
                }
                else
                {
                    UnturnedChat.Say(caller, "Invalid command usage!", Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(caller, "Invalid command usage!", Color.red);
            }
        }
    }
}
