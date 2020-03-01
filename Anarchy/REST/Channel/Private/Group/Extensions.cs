﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public static class GroupExtensions
    {
        /// <summary>
        /// Joins a group
        /// </summary>
        /// <param name="inviteCode">Invite for the group</param>
        /// <returns>The invite used</returns>
        public static GroupInvite JoinGroup(this DiscordClient client, string inviteCode)
        {
            return client.HttpClient.Post($"/invites/{inviteCode}")
                                .Deserialize<GroupInvite>().SetClient(client);
        }


        /// <summary>
        /// Gets a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        public static Group GetGroup(this DiscordClient client, ulong groupId)
        {
            return client.HttpClient.Get($"/channels/{groupId}")
                    .DeserializeEx<Group>().SetClient(client);
        }


        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="recipients">The IDs of the recipients to add</param>
        /// <returns>The created <see cref="Group"/></returns>
        public static Group CreateGroup(this DiscordClient client, List<ulong> recipients)
        {
            return client.HttpClient.Post($"/users/@me/channels", 
                JsonConvert.SerializeObject(new RecipientList() { Recipients = recipients })).DeserializeEx<Group>().SetClient(client);
        }


        /// <summary>
        /// Modifies a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="properties">Options for modifying the group</param>
        /// <returns>The modified <see cref="Group"/></returns>
        public static Group ModifyGroup(this DiscordClient client, ulong groupId, GroupProperties properties)
        {
            return client.HttpClient.Patch($"/channels/{groupId}",
                                JsonConvert.SerializeObject(properties)).DeserializeEx<Group>().SetClient(client);
        }


        // This does the same as DeleteChannel(), i just decided to leave it be because DeleteChannel() is a weird name for a function for leaving groups
        /// <summary>
        /// Leaves a group.
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <returns>The leaved <see cref="Group"/></returns>
        public static Channel LeaveGroup(this DiscordClient client, ulong groupId)
        {
            return client.DeleteChannel(groupId);
        }


        /// <summary>
        /// Adds a user to a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="userId">ID of the user</param>
        public static void AddUserToGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Put($"/channels/{groupId}/recipients/{userId}");
        }


        /// <summary>
        /// Removes a user from a group
        /// </summary>
        /// <param name="groupId">ID of the group</param>
        /// <param name="userId">ID of the user</param>
        public static void RemoveUserFromGroup(this DiscordClient client, ulong groupId, ulong userId)
        {
            client.HttpClient.Delete($"/channels/{groupId}/recipients/{userId}");
        }
    }
}
