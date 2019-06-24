using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    public class Embed
    {
        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("color")]
        public int Color { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }



        [JsonProperty("fields")]
        public List<EmbedField> Fields { get; private set; }

        [JsonProperty("video")]
        public EmbedVideo Video { get; private set; }

        [JsonProperty("thumbnail")]
        public EmbedImage Thumbnail { get; private set; }

        [JsonProperty("image")]
        public EmbedImage Image { get; private set; }

        [JsonProperty("footer")]
        public EmbedFooter Footer { get; private set; }

        [JsonProperty("author")]
        public EmbedAuthor Author { get; private set; }
    }
}
