using Discord;
using Discord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.Login("");

            Thread.Sleep(-1);
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, GatewayLoginEventArgs args)
        {
            Console.WriteLine("logged in");
        }
    }
}
