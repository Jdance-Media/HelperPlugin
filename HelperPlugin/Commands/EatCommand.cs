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
using UnityEngine;

namespace HelperPlugin.Commands
{
    class EatCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "eat";

        public string Help => "Feeds you or another player";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "helper.eat" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (command.Length < 1)
            {
                player.Hunger = 0;
                player.Thirst = 0;
                UnturnedChat.Say(caller, "You have eaten some stuff", Color.yellow );
            }
            if (command.Length >= 1)
            {
                UnturnedPlayer otherPlayer = UnturnedPlayer.FromName(command[0]);

                if (otherPlayer != null)
                {
                    otherPlayer.Hunger = 0;
                    otherPlayer.Thirst = 0;

                    UnturnedChat.Say(caller, $"Fed {otherPlayer.DisplayName}", Color.yellow);
                    UnturnedChat.Say(otherPlayer, "You were fed!", Color.yellow);
                }
                else
                {
                    UnturnedChat.Say(caller, "Invaild Command Usage", Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(caller, "Invaild Command Usage", Color.red);
            }
        }
    }
}
