# 0.1.0.0 [STABLE]
(Please keep in mind that these are only the major/important changes. There has been a lot more than written here)

### Additions
Added GetAvatar()/GetIcon() methods, allowing you to download images from guilds n' stuff.
Added PermissionCalculator.Create(), which allows you to create EditablePermissions objects from lists of permissions.
Added embed support for sending messages.
Implemented IReadOnlyList<T> for more security.
Added a new Type flag in User for identying users.

### Changes
Changed 'Reaction' to 'Emoji' where it is appropriate.

### Bug fixes
Added an Update() method for ClientUser.
Improved 'default properties' fix by implementing a new Property<T> class.



# 0.1.0.0 [BETA]
(Please keep in mind that these are only the major/important changes. There has been a lot more than written here)

### Additions
Added a much quicker way of getting guild members (tho since it's using the gateway it only supports DiscordSocketClient).<br><br>
Added a permission calculator (which has currently not been incorporated into the managable models, but you can still use PermissionCalculator).<br><br>
Added audit log functionality.<br><br>
Update() methods for all managable Discord models have been added, allowing you to update the local information whenever you want.<br><br>
Added multi image format support, meaning that you can now use jpg, png, and gif files.<br><br>
Added 'partial types' that get used when Discord does not respond with full ones.<br><br>
Completed list of gateway opcodes.<br><br>

### Changes
DiscordWebhookClient has been depricated. Hook (webhook object that depends on a DiscordClient) now holds the data as well as basically being the new DiscordWebhookClient.<br><br>
Improved object code organization.<br><br>
Reorganized files and folders.<br><br>

### Bug fixes
Fixed Properties classes setting properties that are not set (or 'null').
<br><br><br>


# 0.0.1.1
#### Additions
Added AddChannelPermissionOverwrite() (please keep in mind tho that a bit calculator for permissions has not been added yet).
Added InvalidParametersException. When invalid parameters are passed in, it making it harder for the wrapper to crash unexpectedly. This has been applied to DiscordHttpClient.
Added Modification capability for reactions.
Added ability to unfriend someone.

#### Bug fixes
Fixed clients and guild id's not being passed into reactions and roles when downloading guilds.
Fixed modification for channels and roles.
Fixed message properties not being readonly.
Other minor bug fixes.

#### Other changes
ChannelProperties has been split into ChannelCreationProperties and ChannelModProperties since you're able to change more when modifying.



# 0.0.1.0
Initial release.
