using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Discord
{
    public class Group : Channel
    {
        [JsonProperty("icon")]
        public string IconId { get; private set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; private set; }

        [JsonProperty("recipients")]
        public List<User> Recipients { get; private set; }


        public Image GetIcon()
        {
            if (IconId == null)
                return null;

            var resp = new HttpClient().GetAsync($"https://cdn.discordapp.com/icons/{Id}/{IconId}.png").Result;

            if (resp.StatusCode == HttpStatusCode.NotFound)
                throw new ImageNotFoundException(IconId);

            return (Bitmap)new ImageConverter().ConvertFrom(resp.Content.ReadAsByteArrayAsync().Result);
        }
    }
}
