using System.Collections.Generic;

namespace Discord
{
    public static class ControllableModelExtensions
    {
        public static T SetClient<T>(this T @class, DiscordClient client) where T : ControllableModel
        {
            @class.Client = client;

            return @class;
        }


        public static List<T> SetClientsInList<T>(this List<T> classes, DiscordClient client) where T : ControllableModel
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }
    }
}