(Please keep in mind that these are only the major/important changes. There have been way more than written here)<br>

# 0.3.0.1
- Added 'Watching' to ActivityType.
- Fixed 'since' parameter in SetActivity().
- Minor bug fixes.



# 0.3.0.0

### Additions
- Added support for DM channels.
- Added GetProfile() and GetMutualFriends().
- Added comments throughout the code to document what the different classes/methods do.
- Added more methods to a bunch of models.
- Added more gateway events.

### Improvements
- Improved security and failsafety.
- Changed many exceptions to be more beginner friendly.
- Fixed PartialEmoji, Emoji and MessageReaction having values that never get set.
- Timestamps are now DateTime objects instead of raw strings.
- Improved sending messages through webhooks (you might have to change your bot).

### Bug fixes
- Fixed GetChannelMessages() getting practically random messages.
- Other minor bug fixes.

### Removels
- Removed MessageProperties overload for SendMessage(), making it look nicer (you might have to change ur bot).
<br><br><br>



# 0.1.2.0

### Additions
- Added a temporary solution to the 'messages in voice channels' problem.
- Added more user-account based values in DiscordError.

### Improvements
- Changed PartialGuild to fit in invite responses.
- Improved GuildId populating.
- Changed many int and long property types to restrict them from being negative.
- Updated GuildDuplicator to match newest version.

### Bug fixes
- Minor bug fixes.



# 0.1.1.1
- Added enum for DiscordHttpError's 'Code' property (DiscordError)
- Added more gateway events (such as OnUserBanned and OnEmojisUpdated)
- Minor reorganizations.
<br><br><br>



# 0.1.1.0

### Additions
- Added embed support for webhooks.
- Added support for all different channels.
- Added permission overwrite functionality.
- Added PartialInvite because of missing information at some endpoints.

### Improvements
- Improved relationship functionality.
- Improved ClientUser funcionality.
- Improved HTTP error handling.

### Bug fixes
- A bunch of minor bug fixes.
<br><br><br>



# 0.1.0.0 [STABLE]

### Additions
- Added GetAvatar()/GetIcon() methods, allowing you to download images from guilds n' stuff.
- Added PermissionCalculator.Create(), which allows you to create EditablePermissions objects from lists of permissions.
- Added EmbedMaker creating and sending embeds.
- Implemented IReadOnlyList<T> for more security.
- Added a new Type flag in User for identying users.
- Added activities for DiscordSocketClient.

### Changes
- Changed 'Reaction' to 'Emoji' where it is appropriate.
- Color property in Role and Embed are now actual Color objects, and not int values.

### Bug fixes
- Added an Update() method for ClientUser.
- Improved 'default properties' fix by implementing a new Property<T> class.
<br><br><br>



# 0.1.0.0 [BETA]

### Additions
- Added a much quicker way of getting guild members (tho since it's using the gateway it only supports DiscordSocketClient).
- Added a permission calculator (which has currently not been incorporated into the managable models, but you can still use PermissionCalculator).
- Added audit log functionality.
- Update() methods for all managable Discord models have been added, allowing you to update the local information whenever you want.
- Added multi image format support, meaning that you can now use jpg, png, and gif files.
- Added 'partial types' that get used when Discord does not respond with full ones.
- Completed list of gateway opcodes.

### Changes
- DiscordWebhookClient has been depricated. Hook (webhook object that depends on a DiscordClient) now holds the data as well as basically being the new DiscordWebhookClient.
- Improved object code organization.
- Reorganized files and folders.

### Bug fixes
- Fixed Properties classes setting properties that are not set (or 'null').
<br><br><br>



# 0.0.1.1
#### Additions
- Added AddChannelPermissionOverwrite() (please keep in mind tho that a bit calculator for permissions has not been added yet).
- Added InvalidParametersException. When invalid parameters are passed in, it making it harder for the wrapper to crash unexpectedly. This has been applied to DiscordHttpClient.
- Added Modification capability for reactions.
- Added ability to unfriend someone.

#### Bug fixes
- Fixed clients and guild id's not being passed into reactions and roles when downloading guilds.
- Fixed modification for channels and roles.
- Fixed message properties not being readonly.
- Other minor bug fixes.

#### Other changes
- ChannelProperties has been split into ChannelCreationProperties and ChannelModProperties since you're able to change more when modifying.
<br><br><br>



# 0.0.1.0
- Initial release.
