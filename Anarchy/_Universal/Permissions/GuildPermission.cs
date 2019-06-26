namespace Discord
{
    public enum GuildPermission
    {
        CreateInstantInvite = 1,
        KickMembers = 2,
        BanMembers = 4,
        Administrator = 8,
        ManageChannels = 10,
        ManageGuild = 20,
        AddReactions = 40,
        ViewAuditLog = 80,
        ViewChannel = 400,
        SendMessages = 800,
        SendTtsMessages = 1000,
        ManageMessages = 2000,
        EmbedLinks = 4000,
        AttachFiles = 8000,
        ReadMessageHistory = 10000,
        MentionEveryone = 20000,
        UseExternalEmojis = 40000,
        ConnectToVC = 100000,
        SpeakInVC = 200000,
        MuteMembers = 400000,
        DeafenVCMembers = 800000,
        MoveVCMembers = 1000000,
        UseVAD = 2000000,
        VCPrioritySpeaker = 100,
        ChangeNickname = 4000000,
        ManageNicknames = 8000000,
        ManageRoles = 10000000,
        ManageWebhook = 20000000,
        ManageEmojis = 40000000
    }
}
