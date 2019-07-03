# About
Anarchy is an opensource Discord API wrapper that focuses on making bot programming easy.<br>
Since the start it has been our goal to make it simple, so that people can easily modify it to their needs.<br>
At the moment we don't have any bugtesters but me, so the wrapper will most likely contain a few bugs here and there bugs.<br>

Oh and also: if you're using a bot token make sure to prefix the token with 'Bot '.<br>


# Examples

## Logging in
```csharp
// The DiscordClient is the most basic client there is. The gateway is NOT available for this client
DiscordClient client = new DiscordClient();
client.Token = "your token here" //Tokens are evaluated whenever they are put in here. It'll trigger an AccessDeniedException if it's invalid

// Same as DiscordClient, but it has gateway support (to use this you need to include Discord.Gateway)
DiscordSocketClient socketClient = new DiscordSocketClient();
socketClient.Login("your token here"); //This is passed to the Token property, meaning that an AccessDeniedException will also be triggered here if the token is invalid 
```

## Joining/leaving a server
```csharp
DiscordClient client = new DiscordClient("your token here");
client.JoinGuild("fortnite"); //We're just gonna use Fortnite as an example
client.LeaveGuild(322850917248663552);
```

## Sending a message
```csharp
DiscordClient client = new DiscordClient("your token here");
Channel channel = client.GetChannel(449751593483632642);
channel.TriggerTyping(); //This is optional, but good for when you need the cooldown of sending a channel message (if there is one)
channel.SendMessage("Hello, World");
```

## Creating guilds and channels
```csharp
DiscordClient client = new DiscordClient("your token here");

// Often when creating a new thing in the server it has a properties object that has some settings
Guild newGuild = client.CreateGuild(new GuildCreationProperties() { Name = "cool stuff", Icon = Image.FromFile("icon.png"), Region = "eu-central" });
Channel newChannel = newGuild.CreateChannel(new ChannelProperties() { Name = "my new channel", Type = ChannelType.Text });
```

## Using gateway events
```csharp
static void Main(string[] args)
{
   // There are obviously more gateway events, i just picked a few
   DiscordSocketClient client = new DiscordSocketClient();
   client.OnLoggedIn += Client_OnLoggedIn;
   client.OnLoggedOut += Client_OnLoggedOut;
   client.OnJoinedGuild += Client_OnJoinedGuild;
   client.OnLeftGuild += Client_OnLeftGuild;
   client.Login("your token here");

   Thread.Sleep(-1);
}

private static void Client_OnLoggedIn(DiscordSocketClient client, UserEventArgs args)
{
   Console.WriteLine($"Logged into {args.User}");
}

private static void Client_OnLoggedOut(DiscordSocketClient client, UserEventArgs args)
{
   Console.WriteLine($"Logged out of {args.User}");
}

private static void Client_OnJoinedGuild(DiscordSocketClient client, GuildEventArgs args)
{
   Console.WriteLine($"Joined guild: {args.Guild}");
}

private static void Client_OnLeftGuild(DiscordSocketClient client, GuildEventArgs args)
{
   Console.WriteLine($"Left guild: {args.Guild}");
}
```

## Downloading all members in a server
```csharp
private static List<User> Users = new List<User>();

static void Main()
{
   // DiscordClient can also be used to do this, but it is incredibly slow compared to the gateway method
   DiscordSocketClient client = new DiscordSocketClient();
   client.OnLoggedIn += Client_OnLoggedIn;
   client.Login("your token here");
}

private static void Client_OnLoggedIn(DiscordSocketClient client, UserEventArgs args)
{
   // Not running this async will result in a permenant freeze of the gateway
   Task.Run(() => Users = client.GetAllGuildMembers(420));
}
```
Example projects can be found in 'Example projects'


### Subscribe or i'll eat ur kids
https://youtube.com/iLinked
