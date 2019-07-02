using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace Discord
{
    public class Embed
    {
        public Embed()
        {
            _type = "rich";
            Fields = new List<EmbedField>();
            Thumbnail = new EmbedImage();
            Image = new EmbedImage();
            Footer = new EmbedFooter();
            Author = new EmbedAuthor();
        }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("type")]
        private readonly string _type;

        [JsonProperty("color")]
        private int _color;
        public Color Color
        {
            get { return Color.FromArgb(_color); }
            set { _color = Color.FromArgb(0, value.R, value.G, value.B).ToArgb(); }
        }

        [JsonProperty("url")]
        public string Url { get; set; }


        [JsonProperty("fields")]
        public List<EmbedField> Fields { get; internal set; }

        //I don't know how videos in embeds work yet
        [JsonProperty("video")]
        public EmbedVideo Video { get; private set; }

        [JsonProperty("thumbnail")]
        public EmbedImage Thumbnail { get; internal set; }

        [JsonProperty("image")]
        public EmbedImage Image { get; internal set; }

        [JsonProperty("footer")]
        public EmbedFooter Footer { get; internal set; }

        [JsonProperty("author")]
        public EmbedAuthor Author { get; internal set; }


        public override string ToString()
        {
            return Title;
        }
    }
}
