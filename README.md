# About
Anarchy is an opensource Discord API wrapper, that focuses on making it easy to make all sorts of discord bots,
as well as modding the wrapper itself.

# Features
Anarchy can pretty much do anything, except voice channel stuff.

### Please be aware tho
Even tho it is able to do all this there might still be bugs, since the api wrapper hasn't been tested through 100% yet. If you find any make sure to open an issue on Github.


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
channel.SendMessage("This was sent from my bot :D");
```

## Creating guilds and channels
```csharp
DiscordClient client = new DiscordClient("your token here");

// Often when creating a new thing in the server it has a properties object that has some settings
Guild newGuild = client.CreateGuild(new GuildCreationProperties() { Name = "cool stuff", Icon = Image.FromFile("icon.png"), Region = "eu-central" });
Channel newChannel = newGuild.CreateChannel(new ChannelProperties() { Name = "my new channel", Type = ChannelType.Text });
```

## Gateway events
```csharp
static void Main(string[] args)
{
   // There are obviously more gateway events, i just picked a few
   DiscordSocketClient client = new DiscordSocketClient("your token here");
   client.OnLoggedIn += Client_OnLoggedIn;
   client.OnLoggedOut += Client_OnLoggedOut;

   client.OnJoinedGuild += Client_OnJoinedGuild;
   client.OnLeftGuild += Client_OnLeftGuild;

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


### Subscribe or i'll eat ur kids
https://youtube.com/iLinked
