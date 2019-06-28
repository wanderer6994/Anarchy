## 0.1.0.0 [BETA]
(Please keep in mind that these are only the major/important changes, there has been a lot more than written here)

#### Additions
Added a much quicker way of getting guild members (tho since it's using the gateway it only supports DiscordSocketClient).
Added a permission calculator (which has currently not been incorporated into the managable models, but you can still use PermissionCalculator).
Added audit log functionality.
Update() methods for all managable Discord models have been added, allowing you to update the local information whenever you want.
Completed list of gateway opcodes.

#### Changes
DiscordWebhookClient has been depricated. Hook (webhook object that depends on a DiscordClient) now holds the data as well as basically being the new DiscordWebhookClient.
Improved object code organization.
Reorganized files and folders.

#### Bug fixes
Fixed Properties classes setting properties that are not set (or 'null').



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
