using System.Collections.Generic;

namespace Discord
{
    //More bad names
    public static class ClientClassExtensions
    {
        public static T SetClient<T>(this T @class, DiscordClient client) where T : ClientClassBase
        {
            @class.Client = client;

            return @class;
        }

        public static List<T> SetClientsInList<T>(this List<T> classes, DiscordClient client) where T : ClientClassBase
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }
    }
}
