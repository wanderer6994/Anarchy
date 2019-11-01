using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Gateway;
using Leaf.xNet;

namespace Testing
{
    class Program
    {
        static void Main()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.Login("")
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Task.Run(() =>
            {
                foreach (var member in client.GetAllGuildMembers(637346043377483813))
                {
                    if (member.User.Username == "Alex Jones")
                        member.Ban();
                }
            });
        }
    }
}
