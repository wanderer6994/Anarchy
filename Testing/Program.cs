using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Gateway;
using Leaf.xNet;
using System.Threading;

namespace Testing
{
    class Program
    {
        static void Main()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.Login("NjQzMTIyNTA4OTA3ODA2Nzcw.Xd4WBg.hsK6VeL0x9G8K6NGd__h7OcgJx0");

            Thread.Sleep(-1);
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine("Logged in, lol");
        }
    }
}
