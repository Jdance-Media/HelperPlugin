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
using Rocket.Unturned.Chat;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using Rocket.Unturned.Permissions;
using SDG.Unturned;
using System.Runtime.CompilerServices;
using Steamworks;

namespace HelperPlugin
{
    public class HelperPlugin : RocketPlugin<HelperPluginConfiguration>
    {
        public static void sendDiscordWebhook(string URL, string escapedjson)
        {
            var wr = WebRequest.Create(URL);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
                sw.Write(escapedjson);
            wr.GetResponse();
        }

        protected override void Load()
        {
            Logger.Log(Configuration.Instance.LoadMessage);
            Logger.Log($"{name} {Assembly.GetName().Version}");
            if (Configuration.Instance.PlayerCoords == true)
            {
                Logger.Log("Player Coords are on! It is recommended to keep this off unless you are developing!");
            }
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerChatted += UnturnedPlayerEvents_OnPlayerChatted;
            if (Configuration.Instance.AutoClearVehicles == true)
            {
                if (Level.isLoaded == true)
                {
                    VehicleManager.askVehicleDestroyAll();
                    Logger.Log($"Auto cleared vehicles from server!");
                }
            }

            if (Configuration.Instance.AutoClearBarricades == true)
            {
                if (Level.isLoaded == true)
                {
                    BarricadeManager.askClearAllBarricades();
                    Logger.Log($"Auto cleared barricades from server!");
                }
            }

            if (Configuration.Instance.PlayerCoords == true)
            {
                UnturnedPlayerEvents.OnPlayerUpdatePosition += UnturnedPlayerEvents_OnPlayerUpdatePosition;
            }

            if (Configuration.Instance.WebhookOn == true)
            {
                sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
          "{\"username\": \"Server Alert\",\"embeds\":[    {\"description\":\"The service has been started!\", \"title\":\"Plugin Online!\", \"color\":1018364}]  }");
            }
        }

        private void UnturnedPlayerEvents_OnPlayerUpdatePosition(UnturnedPlayer player, UnityEngine.Vector3 position)
        {
            if (Configuration.Instance.PlayerCoords == true)
            {
                UnturnedChat.Say(player, $"Position: {position}");
            }
        }

        private void UnturnedPlayerEvents_OnPlayerChatted(UnturnedPlayer player, ref UnityEngine.Color color, string message, SDG.Unturned.EChatMode chatMode, ref bool cancel)
        {
            if (Convert.ToString(chatMode) == "GLOBAL")
            {
                
                if (Configuration.Instance.WebhookOn == true)
                {
                    string steamurl = $"https://steamcommunity.com/profiles/{player.CSteamID}";
                    sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
                            "{\"username\": \"Server Chat\", \"avatar_url\":\""+player.SteamProfile.AvatarMedium+"\",\"embeds\":[    {\"description\":\""+message+ "\", \"title\":\""+player.DisplayName+"\", \"url\":\""+steamurl+"\", \"color\":1018364}]  }");
                }
                
            }
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(Rocket.Unturned.Player.UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            string main = $"{player.DisplayName} was killed by {murderer}";
            sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
                    "{\"username\": \"Server Death\",\"embeds\":[    {\"description\":\""+main+"\", \"title\":\"Player Death\", \"color\":1018364}]  }");
        }

        private void Events_OnPlayerDisconnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            Logger.Log($"{player.DisplayName} has left the server. Their Steam64ID is {player.CSteamID}");
            string disconnect = $"{player.DisplayName} has left the server. Their Steam64ID is {player.CSteamID}";
            if (Configuration.Instance.WebhookOn == true)
            {
                sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
                    "{\"username\": \"Server Alert\",\"embeds\":[    {\"description\":\""+disconnect+"\", \"title\":\"Player Disconnect\", \"color\":1018364}]  }");
            }
        }

        private void Events_OnPlayerConnected(Rocket.Unturned.Player.UnturnedPlayer player)
        {
            UnturnedChat.Say(player, Configuration.Instance.JoinMessage);
            Logger.Log($"{player.DisplayName} has joined the server. Their Steam64ID is {player.CSteamID}");
            string connect = $"{player.DisplayName} has joined the server. Their Steam64ID is {player.CSteamID}";
            if (Configuration.Instance.WebhookOn == true)
            {
                sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
                    "{\"username\": \"Server Alert\",\"embeds\":[    {\"description\":\""+connect+"\", \"title\":\"Player Connect\", \"color\":1018364}]  }");
            }
            
            if (Configuration.Instance.GlobalJoinMessage == true)
            {
                UnturnedChat.Say($"+ {player.DisplayName} has joined the server!");
            }
        }

        protected override void Unload()
        {
            Logger.Log($"{name} has been unloaded");
            Logger.Log(Configuration.Instance.UnloadMessage);
            if (Configuration.Instance.WebhookOn == true)
            {
                sendDiscordWebhook($"{Configuration.Instance.WebhookURL}",
                    "{\"username\": \"Server Alert\",\"embeds\":[    {\"description\":\"The service has been shutdown\", \"title\":\"Plugin Offine!\", \"color\":1018364}]  }");
            }
        }
    }
}
