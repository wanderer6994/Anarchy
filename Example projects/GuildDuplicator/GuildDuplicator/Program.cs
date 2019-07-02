using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GuildDuplicator
{
    class OrganizedChannelList
    {
        public OrganizedChannelList(IReadOnlyList<Channel> channels)
        {
            Categories = channels.Where(channel => channel.Type == ChannelType.Category).ToList();
            TextChannels = channels.Where(channel => channel.Type == ChannelType.Text).ToList();
            VoiceChannels = channels.Where(channel => channel.Type == ChannelType.Voice).ToList();
        }

        public List<Channel> Categories { get; private set; }
        public List<Channel> TextChannels { get; private set; }
        public List<Channel> VoiceChannels { get; private set; }
    }

    struct CategoryDupe
    {
        public Channel OurCategory;
        public Channel TargetCategory;
    }

    class Program
    {
        static void Main(string[] args)
        {
            DiscordClient client = new DiscordClient("your token here");

            Console.Write($"Guild id: ");
            Guild targetGuild = client.GetGuild(long.Parse(Console.ReadLine()));

            Console.WriteLine("Duplicating guild...");

            Guild ourGuild = client.CreateGuild(new GuildCreationProperties() { Name = targetGuild.Name, Icon = targetGuild.GetIcon(), Region = targetGuild.Region });
            ourGuild.Modify(new GuildModProperties() { VerificationLevel = targetGuild.VerificationLevel, DefaultNotifications = targetGuild.DefaultNotifications });

            #region delete our channels
            Console.WriteLine("Deleting default guild channels...");

            foreach (var channel in ourGuild.GetChannels())
            {
                channel.Delete();

                Console.WriteLine($"Deleted {channel.Name}");

                Thread.Sleep(100);
            }
            #endregion

            #region create channels
            OrganizedChannelList channels = new OrganizedChannelList(targetGuild.GetChannels());

            Console.WriteLine("Duplicating categories...");

            List<CategoryDupe> ourCategories = new List<CategoryDupe>();
            foreach (var category in channels.Categories)
            {
                CategoryDupe dupe = new CategoryDupe
                {
                    TargetCategory = category,
                    OurCategory = ourGuild.CreateChannel(new ChannelCreationProperties() { Name = category.Name, Type = ChannelType.Category })
                };
                ourCategories.Add(dupe);

                Console.WriteLine($"Duplicated {category}");

                Thread.Sleep(100);
            }

            Console.WriteLine("Duplicating channels...");

            foreach (var channel in channels.TextChannels.Concat(channels.VoiceChannels))
            {
                ourGuild.CreateChannel(new ChannelCreationProperties() { Name = channel.Name, ParentId = channel.ParentId != null ? (long?)ourCategories.First(ca => ca.TargetCategory.Id == channel.ParentId).OurCategory.Id : null, Type = channel.Type });

                Console.WriteLine($"Duplicated {channel}");

                Thread.Sleep(100);
            }
            #endregion

            #region create roles
            Console.WriteLine("Duplicating roles...");

            foreach (var role in targetGuild.GetRoles())
            {
                if (role.Name == "@everyone")
                    ourGuild.GetRoles().First(r => r.Name == "@everyone").Modify(new RoleProperties() { Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });
                else
                    ourGuild.CreateRole(new RoleProperties() { Name = role.Name, Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });

                Console.WriteLine($"Duplicated {role.ToString()}");

                Thread.Sleep(100);
            }
            #endregion
        }
    }
}