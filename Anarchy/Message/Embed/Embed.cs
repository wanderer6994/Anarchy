using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord
{
    //Sets SHOULD be private but we also have to be able to create them
    public class Embed
    {
        public Embed()
        {
            Type = "rich";
            Fields = new List<EmbedField>();
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("color")]
        public int Color { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }


        [JsonProperty("fields")]
        public List<EmbedField> Fields { get; set; }

        //I really don't know how videos in embeds work, but it'd be great to find out
        [JsonProperty("video")]
        public EmbedVideo Video { get; private set; }

        [JsonProperty("thumbnail")]
        public EmbedImage Thumbnail { get; set; }

        [JsonProperty("image")]
        public EmbedImage Image { get; set; }

        [JsonProperty("footer")]
        public EmbedFooter Footer { get; set; }

        [JsonProperty("author")]
        public EmbedAuthor Author { get; set; }


        public override string ToString()
        {
            return Title;
        }
    }
}
