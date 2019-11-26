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
            client.Login("NjQzMTIyNTA4OTA3ODA2Nzcw.XdaZHA.E-rKnCMCYuxwMHYUZ_JQnMcveTM");

            Thread.Sleep(-1);
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
        }
    }
}
