using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

//if the server is nitro boosted you might encounter problems
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

    struct RoleDupe
    {
        public Role OurRole;
        public Role TargetRole;
    }

    struct CategoryDupe
    {
        public GuildChannel OurCategory;
        public GuildChannel TargetCategory;
    }

    class Program
    {
        static void Main()
        {
            //Create a client with the token
            Console.Write("Token: ");
            DiscordClient client = new DiscordClient(Console.ReadLine());

            //find the guild
            Console.Write($"Guild id: ");
            Guild targetGuild = client.GetGuild(long.Parse(Console.ReadLine()));
            Guild ourGuild = DuplicateGuild(client, targetGuild);

            DeleteAllChannels(client, ourGuild);

            List<RoleDupe> ourRoles = DuplicateRoles(client, targetGuild, ourGuild);

            #region create channels
            OrganizedChannelList channels = new OrganizedChannelList(targetGuild.GetChannels());

            Console.WriteLine("Duplicating categories...");

            //duplicate category channels
            List<CategoryDupe> ourCategories = new List<CategoryDupe>();
            foreach (var c in channels.Categories)
            {
                GuildChannel category;

                try
                {
                    category = client.GetGuildChannel(c.Id);
                }
                catch (DiscordHttpErrorException e)
                {
                    //ofcourse you could make it return no matter what error, but this is better for debugging
                    if (e.Error.Code == 50001)
                        continue;
                    else
                        throw;
                }

                //create the category
                GuildChannel ourCategory = ourGuild.CreateChannel(new ChannelCreationProperties() { Name = category.Name, Type = ChannelType.Category });
                ourCategory.Modify(new GuildChannelProperties() { Position = category.Position });

                foreach (var overwrite in category.PermissionOverwrites)
                {
                    if (overwrite.Type == PermissionOverwriteType.Member)
                        continue;

                    PermissionOverwrite ourOverwrite = overwrite;
                    ourOverwrite.Id = ourRoles.First(ro => ro.TargetRole.Id == overwrite.Id).OurRole.Id;
                    ourCategory.AddPermissionOverwrite(ourOverwrite);
                }

                CategoryDupe dupe = new CategoryDupe
                {
                    TargetCategory = category,
                    OurCategory = ourCategory
                };
                ourCategories.Add(dupe);

                Console.WriteLine($"Duplicated {category.Name}");

                Thread.Sleep(50);
            }

            Console.WriteLine("Duplicating channels...");

            //duplicate text channels
            foreach (var c in channels.TextChannels)
            {
                TextChannel channel;

                try
                {
                    channel = client.GetTextChannel(c.Id);
                }
                catch (DiscordHttpErrorException e)
                {
                    //ofcourse you could make it return no matter what error, but this is better for debugging
                    if (e.Error.Code == 50001)
                        continue;
                    else
                        throw;
                }

                TextChannel ourChannel = ourGuild.CreateTextChannel(new ChannelCreationProperties() { Name = channel.Name, ParentId = channel.ParentId != null ? (long?)ourCategories.First(ca => ca.TargetCategory.Id == channel.ParentId).OurCategory.Id : null });
                ourChannel.Modify(new TextChannelProperties() { Nsfw = channel.Nsfw, Position = channel.Position, Topic = channel.Topic, SlowMode = channel.SlowMode });

                foreach (var overwrite in channel.PermissionOverwrites)
                {
                    if (overwrite.Type == PermissionOverwriteType.Member)
                        continue;

                    PermissionOverwrite ourOverwrite = overwrite;
                    ourOverwrite.Id = ourRoles.First(ro => ro.TargetRole.Id == overwrite.Id).OurRole.Id;
                    ourChannel.AddPermissionOverwrite(ourOverwrite);
                }

                Console.WriteLine($"Duplicated {channel.Name}");

                Thread.Sleep(50);
            }

            //duplicate voice channels
            foreach (var c in channels.VoiceChannels)
            {
                VoiceChannel channel;

                try
                {
                    channel = client.GetVoiceChannel(c.Id);
                }
                catch (DiscordHttpErrorException e)
                {
                    //ofcourse you could make it return no matter what error, but this is better for debugging
                    if (e.Error.Code == 50001)
                        continue;
                    else
                        throw;
                }


                //create voice channels
                VoiceChannel ourChannel = ourGuild.CreateVoiceChannel(new ChannelCreationProperties() { Name = channel.Name, ParentId = channel.ParentId != null ? (long?)ourCategories.First(ca => ca.TargetCategory.Id == channel.ParentId).OurCategory.Id : null });
                ourChannel.Modify(new VoiceChannelProperties() { Bitrate = channel.Bitrate, Position = channel.Position, UserLimit = channel.UserLimit });

                foreach (var overwrite in channel.PermissionOverwrites)
                {
                    if (overwrite.Type == PermissionOverwriteType.Member)
                        continue;

                    PermissionOverwrite ourOverwrite = overwrite;
                    ourOverwrite.Id = ourRoles.First(ro => ro.TargetRole.Id == overwrite.Id).OurRole.Id;
                    ourChannel.AddPermissionOverwrite(ourOverwrite);
                }

                Console.WriteLine($"Duplicated {channel.Name}");

                Thread.Sleep(100);
            }
            #endregion

            Console.WriteLine("Done!");
            Console.ReadLine();
        }


        private static Guild DuplicateGuild(DiscordClient client, Guild guild)
        {
            Console.WriteLine("Duplicating guild...");

            //create the guild and modify it with settings from the target
            Guild ourGuild = client.CreateGuild(new GuildCreationProperties() { Name = guild.Name, Icon = guild.GetIcon(), Region = guild.Region });
            ourGuild.Modify(new GuildProperties() { VerificationLevel = guild.VerificationLevel, DefaultNotifications = guild.DefaultNotifications });

            return ourGuild;
        }


        private static void DeleteAllChannels(DiscordClient client, Guild guild)
        {
            Console.WriteLine("Deleting default guild channels...");

            //when you create a guild it automatically creates some channels, which we have to delete
            foreach (var channel in guild.GetChannels())
            {
                channel.Delete();

                Console.WriteLine($"Deleted {channel}");

                Thread.Sleep(100);
            }
        }


        private static List<RoleDupe> DuplicateRoles(DiscordClient client, Guild targetGuild, Guild ourGuild)
        {
            List<RoleDupe> ourRoles = new List<RoleDupe>();

            Console.WriteLine("Duplicating roles...");

            //duplicate roles
            foreach (var role in targetGuild.GetRoles())
            {
                RoleDupe dupe = new RoleDupe();
                dupe.TargetRole = role;

                if (role.Name == "@everyone") //we don't wanna create another @everyone role, so we just modify ours instead
                {
                    Role ourRole = ourGuild.GetRoles().First(r => r.Name == "@everyone");
                    ourRole.Modify(new RoleProperties() { Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });
                    dupe.OurRole = ourRole;
                }
                else
                    dupe.OurRole = ourGuild.CreateRole(new RoleProperties() { Name = role.Name, Permissions = new EditablePermissions(role.Permissions), Color = role.Color, Mentionable = role.Mentionable, Seperated = role.Seperated });
                ourRoles.Add(dupe);

                Console.WriteLine($"Duplicated {role}");

                Thread.Sleep(100);
            }

            return ourRoles;
        }
    }
}