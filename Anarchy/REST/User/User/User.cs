using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;

namespace Discord
{
    public class User : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("username")]
        public string Username { get; protected set; }


        [JsonProperty("discriminator")]
        public uint Discriminator { get; protected set; }


        [JsonProperty("avatar")]
        public string AvatarId { get; protected set; }


        [JsonProperty("flags")]
        public Badge Badges { get; protected set; }


        [JsonProperty("bot")]
#pragma warning disable 0414, 0649
        private readonly bool _bot;
#pragma warning restore 0414, 0649


        public UserType Type
        {
            get
            {
                if (Discriminator == 0)
                    return UserType.Webhook;

                return _bot ? UserType.Bot : UserType.User;
            }
        }



        public Hypesquad Hypesquad
        {
            get
            {
                return (Hypesquad)Enum.Parse(typeof(Hypesquad), 
                                        (Badges & (Badge.HypeBravery | Badge.HypeBrilliance | Badge.HypeBalance))
                                         .ToString().Replace("Hype", ""));
            }
        }


        /// <summary>
        /// Updates the user's info
        /// </summary>
        public virtual void Update()
        {
            User user = Client.GetUser(Id);
            Username = user.Username;
            Discriminator = user.Discriminator;
            AvatarId = user.AvatarId;
        }


        /// <summary>
        /// Gets the user's profile
        /// </summary>
        public Profile GetProfile()
        {
            return Client.GetProfile(Id);
        }


        /// <summary>
        /// Sends a friend request to the user
        /// </summary>
        public void SendFriendRequest()
        {
            if (Id == Client.User.Id)
                return;

            Client.SendFriendRequest(Username, Discriminator);
        }


        /// <summary>
        /// Blocks the user
        /// </summary>
        public void Block()
        {
            if (Id == Client.User.Id)
                return;

            Client.BlockUser(Id);
        }


        /// <summary>
        /// Removes any relationship (unfriending, unblocking etc.)
        /// </summary>
        public void RemoveRelationship()
        {
            if (Id == Client.User.Id)
                return;

            Client.RemoveRelationship(Id);
        }


        /// <summary>
        /// Gets the user's avatar
        /// </summary>
        /// <returns>The avatar (returns null if AvatarId is null)</returns>
        public Image GetAvatar()
        {
            if (AvatarId == null)
                return null;

            return (Bitmap)new ImageConverter()
                        .ConvertFrom(new HttpClient().GetByteArrayAsync($"https://cdn.discordapp.com/avatars/{Id}/{AvatarId}.png").Result);
        }


        public override string ToString()
        {
            return $"{Username}#{"0000".Remove(4 - Discriminator.ToString().Length) + Discriminator.ToString()}";
        }
    }
}