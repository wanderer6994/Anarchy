using System;

namespace Discord
{
    internal static class DiscordTimestamp
    {
        /// <summary>
        /// Converts Discord timestamps to a <see cref="DateTime"/>
        /// </summary>
        /// <returns>The converted <see cref="DateTime"/></returns>
        public static DateTime FromString(string timestamp)
        {
            string[] info = timestamp.Split('T');

            string[] date = info[0].Split('-');
            string[] time = info[1].Split(':');

            return new DateTime(int.Parse(date[0]),
                                int.Parse(date[1]),
                                int.Parse(date[2]),
                                int.Parse(time[0]),
                                int.Parse(time[1]),
                                int.Parse(time[2].Split('.')[0]),
                                int.Parse(time[3]));
        }
    }
}
