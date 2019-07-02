using System;
using System.Threading;
using Discord;
using Discord.Gateway;

namespace MessageLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            DiscordSocketClient client = new DiscordSocketClient();
            client.OnLoggedIn += Client_OnLoggedIn;
            client.OnMessageReceived += Client_OnMessageReceived;

            Console.Write("Token: ");
            client.Login(Console.ReadLine());

            Thread.Sleep(-1);
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            Console.WriteLine($"[{args.Message.ChannelId}/{args.Message.Author.Username}#{args.Message.Author.Discriminator}] {args.Message.Content}");
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, GatewayLoginEventArgs args)
        {
            Console.WriteLine($"Logged into {args.Login.User}");
        }
    }
}
