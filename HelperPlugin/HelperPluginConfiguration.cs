using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Core.Plugins;
using Rocket.Core.Logging;

namespace HelperPlugin
{
    public class HelperPluginConfiguration : IRocketPluginConfiguration
    {
        public string LoadMessage { get; set; }
        public string UnloadMessage { get; set; }
        public string JoinMessage { get; set; }
        public bool AutoClearVehicles { get; set; }
        public bool AutoClearBarricades { get; set; }
        public bool WebhookOn { get; set; }
        public string WebhookURL { get; set; }
        public static string NewsMessage { get; set; }
        public static string About = "This is HelperPlugin by Jdance. Comes with a few useful commands discord features!";
        public string News { get; set; }
        public bool GlobalJoinMessage { get; set; }
        public bool PlayerCoords { get; set; }

        public void LoadDefaults()
        {
            LoadMessage = "Helper Plugin is now loaded!";
            UnloadMessage = "Helper Plugin is now unloaded!";
            JoinMessage = "Welcome to the server!";
            AutoClearVehicles = false;
            AutoClearBarricades = false;
            WebhookURL = "https://discord.com/api/webhooks/824348535045750874/HmYTMVViiA3w5Rm1iooG1nOsMoGXBnzJaKI45I41GRf2Dvzzgu1XmrCgomxbH2exzkrC";
            News = "No current news set!";
            NewsMessage = News;
            GlobalJoinMessage = true;
            WebhookOn = true;
            PlayerCoords = false;
        }
    }
}
