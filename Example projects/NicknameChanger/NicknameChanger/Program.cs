using System;
using System.Threading;
using Discord;

namespace NicknameChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Token: ");
            DiscordClient client = new DiscordClient(Console.ReadLine());

            Console.Write("Guild id: ");
            Guild guild = client.GetGuild(long.Parse(Console.ReadLine()));

            Console.Write("Full nickname: ");
            string nickname = Console.ReadLine();

            int index = 0;
            string currentNick = "";
            while (true)
            {
                if (index == nickname.Length)
                {
                    index = 0;
                    currentNick = "";
                }
                currentNick += nickname[index];
                guild.ChangeNickname(currentNick);
                index++;

                Thread.Sleep(1000);
            }
        }
    }
}
