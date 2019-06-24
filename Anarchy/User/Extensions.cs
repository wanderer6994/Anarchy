﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Discord
{
    public static class UserExtensions
    {
        public static User GetUser(this DiscordClient client, long userId)
        {
            var resp = client.HttpClient.Get($"/users/{userId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new UserNotFoundException(client, userId);

            return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
        }


        public static ClientUser GetClientUser(this DiscordClient client)
        {
            HttpResponseMessage resp;

            try
            {
                resp = client.HttpClient.Get("/users/@me");
            }
            catch (AccessDeniedException)
            {
                client.User = null;
                throw;
            }

            client.User = JsonConvert.DeserializeObject<ClientUser>(resp.Content.ReadAsStringAsync().Result);
            return client.User;
        }


        public static List<BasicGuild> GetClientGuilds(this DiscordClient client)
        {
            var resp = client.HttpClient.Get("/users/@me/guilds");

            List<BasicGuild> guilds = JsonConvert.DeserializeObject<List<BasicGuild>>(resp.Content.ReadAsStringAsync().Result);
            foreach (var guild in guilds) guild.Client = client;
            return guilds;
        }


        public static Invite JoinGuild(this DiscordClient client, string invCode)
        {
            var resp = client.HttpClient.Post($"/invite/{invCode}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new InvalidInviteException(client, invCode);

            Invite invite = JsonConvert.DeserializeObject<Invite>(resp.Content.ReadAsStringAsync().Result);
            invite.Client = client;
            return invite;
        }


        public static bool LeaveGuild(this DiscordClient client, long guildId)
        {
            var resp = client.HttpClient.Delete($"/users/@me/guilds/{guildId}");

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new GuildNotFoundException(client, guildId);

            return resp.StatusCode == HttpStatusCode.NoContent;
        }


        public static bool SetHypesquad(this DiscordClient client, HypesquadHouse house)
        {
            //the request protocol is different if we don't want any hypesquad
            if (house == HypesquadHouse.None)
                return client.HttpClient.Post("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).StatusCode == HttpStatusCode.NoContent;

            return client.HttpClient.Post("/hypesquad/online", JsonConvert.SerializeObject(new Hypesquad() { House = house })).StatusCode == HttpStatusCode.NoContent;
        }


        public static bool ChangeSettings(this DiscordClient client, UserSettings settings)
        {
            if (client.HttpClient.Patch("/users/@me", JsonConvert.SerializeObject(settings)).StatusCode == HttpStatusCode.OK)
            {
                client.GetClientUser();
                return true;
            }
            else
                return false;
        }


        internal static string GetFingerprint(this DiscordClient client)
        {
            return JsonConvert.DeserializeObject<Experiments>(client.HttpClient.Get("/experiments").Content.ReadAsStringAsync().Result).Fingerprint;
        }
    }
}