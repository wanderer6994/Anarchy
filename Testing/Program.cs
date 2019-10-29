using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Leaf.xNet;

namespace Testing
{
    class Program
    {
        static void Main()
        {
            foreach (var proxy in File.ReadAllLines("Proxies.txt"))
            {
                try
                {
                    DiscordClient client = new DiscordClient(proxy, ProxyType.Socks4);

                    Console.WriteLine("ayyy we did it chief");
                }
                catch
                {
                    Console.WriteLine("bruh moment");
                }
            }
        }
    }
}
