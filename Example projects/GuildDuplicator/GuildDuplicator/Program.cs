using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GuildDuplicator
{
    //makes it easier to make sure categories get created first (which they need to)
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
            //Create a client with the token
            Console.Write("Token: ");
            DiscordClient client = new DiscordClient(Console.ReadLine());

            //find the guild
            Console.Write($"Guild id: ");
            Guild targetGuild = client.GetGuild(long.Parse(Console.ReadLine()));

            Console.WriteLine("Duplicating guild...");

            //create the guild and modify it with settings from the target
            Guild ourGuild = client.CreateGuild(new GuildCreationProperties() { Name = targetGuild.Name, Icon = targetGuild.GetIcon(), Region = targetGuild.Region });
            ourGuild.Modify(new GuildModProperties() { VerificationLevel = targetGuild.VerificationLevel, DefaultNotifications = targetGuild.DefaultNotifications });

            #region delete our channels
            Console.WriteLine("Deleting default guild channels...");

            //when you create a guild it automatically creates some channels, which we have to delete
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

            //duplicate category channels
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

            //duplicate all other channels
            foreach (var channel in channels.TextChannels.Concat(channels.VoiceChannels))
            {
                Channel ourChannel = ourGuild.CreateChannel(new ChannelCreationProperties() { Name = channel.Name, ParentId = channel.ParentId != null ? (long?)ourCategories.First(ca => ca.TargetCategory.Id == channel.ParentId).OurCategory.Id : null, Type = channel.Type });
                ourChannel.Modify(new ChannelModProperties() { Nsfw = channel.Nsfw, Position = channel.Position, Topic = channel.Topic });

                Console.WriteLine($"Duplicated {channel}");

                Thread.Sleep(100);
            }
            #endregion

            #region create roles
            Console.WriteLine("Duplicating roles...");

            //duplicate roles
            foreach (var role in targetGuild.GetRoles())
            {
                if (role.Name == "@everyone") //we don't wanna create another @everyone role, so we just modify ours instead
                    ourGuild.GetRoles().First(r => r.Name == "@everyone").Modify(new RoleProperties() { Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });
                else
                    ourGuild.CreateRole(new RoleProperties() { Name = role.Name, Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });

                Console.WriteLine($"Duplicated {role.ToString()}");

                Thread.Sleep(100);
            }
            #endregion

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}