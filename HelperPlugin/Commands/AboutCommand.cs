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

namespace HelperPlugin.Commands
{
    class AboutCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "about";

        public string Help => "Tells you more about the plugin";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "helper.about" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedChat.Say(caller, $"{HelperPluginConfiguration.About}");
        }
    }
}
