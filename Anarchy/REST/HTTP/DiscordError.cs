﻿namespace Discord
{
    public enum DiscordError
    {
        UnknownAccount = 10001,
        UnknownApp,
        UnknownChannel,
        UnknownGuild,
        UnknownIntegration,
        UnknownInvite,
        UnknownMember,
        UnknownMessage,
        UnknownOverwrite,
        UnknownProvider,
        UnknownRole,
        UnknownToken,
        UnknownUser,
        UnknownEmoji,
        UnknownWebhook,
        UserOnly = 20001,
        BotOnly,
        MaximumGuilds = 30001,
        MaximumFriends,
        MaximumPins,
        MaximumRoles,
        MaximumReactions = 30010,
        MaximumGuildChannels = 30013,
        Unauthorized = 40001,
        AccountUnverified,
        InvalidInvite = 40007,
        AccountOwnsGuilds = 40011,
        NotConnectedToVoice = 40032,
        MissingAccess = 50001,
        InvalidAccountType,
        CannotExecuteInDM,
        WidgetDisabled,
        CannotEditMessageByOtherUser,
        CannotSendEmptyMessage,
        CannotDMThisUser,
        CannotSendMessagesInVC,
        ChannelVerificationTooHigh,
        OAuth2AppDoesNotHaveABot,
        OAuth2AppLimitReached,
        InvalidOAuthState,
        MissingPermissions,
        InvalidAuthToken,
        NoteTooLong,
        InvalidBulkDeleteAmount,
        PasswordDoesNotMatch = 50018,
        CannotPinMessageInOtherChannel = 50019,
        CannotExecuteOnSysMessage = 50025,
        InvalidRecipient = 50033,
        InvalidOAuthAccessToken = 50034,
        InvalidFormBody,
        InvalidAPIVersion = 50041,
        InvalidGuild = 50055,
        IncomingFriendRequestsDisabled = 80000,
        ReactionBlocked = 90001,
        ResourceOverloaded = 130000
    }
}
