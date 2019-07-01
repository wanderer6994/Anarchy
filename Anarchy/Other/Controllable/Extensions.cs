using System.Collections.Generic;

namespace Discord
{
    public static class ControllableExtensions
    {
        public static T SetClient<T>(this T @class, DiscordClient client) where T : Controllable
        {
            @class.Client = client;

            return @class;
        }


        public static List<T> SetClientsInList<T>(this List<T> classes, DiscordClient client) where T : Controllable
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }

        public static IReadOnlyList<T> SetClientsInList<T>(this IReadOnlyList<T> classes, DiscordClient client) where T : Controllable
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }
    }
}