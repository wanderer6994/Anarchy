# Anarchy
Anarchy is an opensource Discord API wrapper, lol


## Read Me in progress just have this example lol.
```
class ESKETTIT
    {
        static List<long> guilds = new List<long>();

        static void Main(string[] args)
        {
            DiscordClient Client = new DiscordClient(args[0]);
            int a = 0;
            Console.WriteLine("Creating Shit...");
            while (a != 100)
            {
                Console.WriteLine("Creating Server " + a);
                GuildCreationProperties GuildProps = new GuildCreationProperties();
                GuildProps.Name = "Test";
                GuildProps.Region = "us-west";  
                Guild guild = Client.CreateGuild(GuildProps);
                guilds.Add(guild.Id);
                a++;
            }
            Console.WriteLine("Set hypesquad to brilliance...");
            Client.SetHypesquad(HypesquadHouse.Brilliance);
            Console.WriteLine("Leaving Shit...");
            foreach (long id in guilds)
            {
                Console.WriteLine("Left " + id);
                Client.LeaveGuild(id);
            }
        }
    }
    ```
