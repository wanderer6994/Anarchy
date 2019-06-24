## 0.0.1.2
#### Additions
Added new methods for getting members much quicker (since it's using the gateway it only supports DiscordSocketClient).
Completed list of gateway opcodes.

#### Other changes
Reorganized files and folders.
Other minor code changes.



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
