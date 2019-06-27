using System.Collections.Generic;

namespace Discord
{
    public static class ControllableModelExtensions
    {
        /// <summary>
        /// Sets the class's DiscordClient
        /// </summary>
        public static T SetClient<T>(this T @class, DiscordClient client) where T : ControllableModel
        {
            @class.Client = client;

            return @class;
        }

        /// <summary>
        /// Sets a list of class's DiscordClient
        /// </summary>
        public static List<T> SetClientsInList<T>(this List<T> classes, DiscordClient client) where T : ControllableModel
        {
            foreach (var @class in classes)
                @class.Client = client;

            return classes;
        }
    }
}