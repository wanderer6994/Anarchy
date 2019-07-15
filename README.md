# About
Anarchy is an opensource Discord API wrapper that focuses on making bot programming easy.<br>
Since the start it has been our goal to make it simple yet effective, so that people can easily modify it to their needs, and not have huge performance losses.<br>

Oh and also: if you're using a bot token make sure to prefix the token with 'Bot '.<br><br>


# Examples

## Logging in
```csharp
// The DiscordClient is the most basic client there is. The gateway is NOT available for this client
DiscordClient client = new DiscordClient();
client.Token = "your token here" //Tokens are evaluated whenever they are put in here. It'll trigger a DiscordHttpException if it's invalid

// Same as DiscordClient, but it has gateway support (to use this you need to include Discord.Gateway)
DiscordSocketClient socketClient = new DiscordSocketClient();
socketClient.Login("your token here"); //This is passed to the Token property, meaning that a DiscordHttpException will also be triggered here if the token is invalid 
```

## Joining/leaving a server
```csharp
DiscordClient client = new DiscordClient("your token here");

PartialInvite invite = client.JoinGuild("fortnite");
client.LeaveGuild(invite.Guild.Id);
```

## Sending a message
```csharp
DiscordClient client = new DiscordClient("your token here");

TextChannel channel = client.GetTextChannel(420);
channel.TriggerTyping(); //This is optional
channel.SendMessage("Hello, World");
```

## Creating guilds and channels
```csharp
DiscordClient client = new DiscordClient("your token here");

Guild newGuild = client.CreateGuild(new GuildCreationProperties() { Name = "cool stuff", Icon = Image.FromFile("icon.png"), Region = "eu-central" });
GuildChannel newChannel = newGuild.CreateChannel(new ChannelCreationProperties() { Name = "my new channel" });
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

private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
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

## Downloading members in a server
```csharp
static void Main()
{
   // DiscordClient can also be used to do this, but it is incredibly slow compared to the gateway method
   DiscordSocketClient client = new DiscordSocketClient();
   client.OnLoggedIn += Client_OnLoggedIn;
   client.Login("your token here");
}

private static async void Client_OnLoggedIn(DiscordSocketClient client, UserEventArgs args)
{
   Task.Run(() =>
   {
       IReadOnlyList<User> users = client.GetAllGuildMembers(420);
       foreach (var user in users)
       {
           // will print username#discriminator
           Console.Writeline(user.ToString());
       }
   });
}
```

## Sending embeds
```csharp
DiscordClient client = new DiscordClient("your token here");

Channel channel = client.GetChannel(420);

//you can also set a bunch of images. i just haven't done that here
EmbedMaker embed = new EmbedMaker();
embed.Title = "this is an embed";
embed.TitleUrl = "https://github.com/iLinked1337/Anarchy";
embed.Description = "sent from Anarchy";
embed.Color = Color.FromArgb(0, 104, 204);
embed.AddField("Anarchy", "is a Discord API wrapper");
embed.Footer.Text = "Made by iLinked";
embed.Author.Name = "iLinked";
embed.Author.Url = "https://youtube.com/iLinked";

channel.SendMessage(new MessageProperties() { Content = "hey look it's an embed!", Embed = embed });
```

Example projects can be found in 'Example projects'


### Subscribe or i'll eat ur kids
https://youtube.com/iLinked
