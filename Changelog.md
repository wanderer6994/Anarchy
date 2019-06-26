## 0.1.0.0 [BETA]
#### Additions
Added much quicker ways of getting guild members (tho since it's using the gateway it only supports DiscordSocketClient).
Added stuff to make it easier to populate classes with a specific client.
Added a permission calculator, which currently has not been incorporated into the objects, but you can still use the calculator: PermissionCalculator.
Completed list of gateway opcodes.

#### Changes
DiscordWebhookClient has been depricated. Hook (webhook object that depends on a DiscordClient) now holds the data as well as basically being the new DiscordWebhookClient.
Improved guild management.
Other minor code changes.
Reorganized files and folders.

#### Deletions
I've had to remove AddChannelPermissionOverwrite() for now because of it not being able to get channels for unknown reasons.



## 0.0.1.1
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



## 0.0.1.0
Initial release.
