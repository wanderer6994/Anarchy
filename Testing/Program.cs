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
            DiscordClient client = new DiscordClient("NjQzMTIyNTA4OTA3ODA2Nzcw.Xd4UiQ.xvSvAnm_pQ1YKTQgyelMMDkweOQ");
            Guild haxx = client.LurkGuild(client.GetInvite("fortnite").ToGuildInvite().Guild.Id, "09d3e9ea20f487e05a45077d38979805");
        }
    }
}
