using System.Collections.Generic;

namespace Discord
{
    public static class ClientMemberExtensions
    {
        public static T SetClient<T>(this T @class, DiscordClient client) where T : ClientMember
        {
            @class.Client = client;

            return @class;
        }

        public static List<T> SetClientsInList<T>(this List<T> classes, DiscordClient client) where T : ClientMember
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }
    }
}