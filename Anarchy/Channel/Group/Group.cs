using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;

namespace Discord
{
    public class Group : Channel
    {
        public Group()
        {
            OnClientUpdated += (sender, e) => Recipients.SetClientsInList(Client);
        }


        [JsonProperty("icon")]
        public string IconId { get; private set; }


        [JsonProperty("owner_id")]
        public ulong OwnerId { get; private set; }


        [JsonProperty("recipients")]
        public IReadOnlyList<User> Recipients { get; private set; }


        public override void Update()
        {
            Group group = Client.GetGroup(Id);
            Name = group.Name;
            IconId = group.IconId;
            OwnerId = group.OwnerId;
            Recipients = group.Recipients;
        }


        public void Modify(GroupProperties properties)
        {
            if (!properties.NameProperty.Set)
                properties.Name = Name;
            if (!properties.IconSet)
                properties.Icon = GetIcon();

            Group group = Client.ModifyGroup(Id, properties);
            Name = group.Name;
            IconId = group.IconId;
            OwnerId = group.OwnerId;
            Recipients = group.Recipients;
        }


        public void Leave()
        {
            Client.LeaveGroup(Id);
        }


        public void AddRecipient(ulong userId)
        {
            Client.AddUserToGroup(Id, userId);
        }


        public void AddRecipient(User user)
        {
            AddRecipient(user.Id);
        }


        public void RemoveRecipient(ulong userId)
        {
            Client.RemoveUserFromGroup(Id, userId);
        }


        public void RemoveRecipient(User user)
        {
            RemoveRecipient(user.Id);
        }


        public PartialInvite CreateInvite(InviteProperties properties = null)
        {
            return Client.CreateInvite(Id, properties);
        }


        public Image GetIcon()
        {
            if (IconId == null)
                return null;

            return (Bitmap)new ImageConverter().ConvertFrom(new HttpClient().GetAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.png")
                                                                    .Result.Content.ReadAsByteArrayAsync().Result);
        }
    }
}