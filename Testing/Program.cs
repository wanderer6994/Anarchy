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
            DiscordClient client = new DiscordClient("NjQzMTIyNTA4OTA3ODA2Nzcw.XdRJAQ.NbRZN7QetwBFFk-ifx20Fm-j-pE");
            client.QueryGuilds(40);

            Thread.Sleep(-1);
        }
    }
}
