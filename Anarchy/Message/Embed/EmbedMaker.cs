using System.Drawing;

namespace Discord
{
    public class EmbedMaker
    {
        private Embed _embed { get; set; }


        public string Title
        {
            get { return _embed.Title; }
            set { _embed.Title = value; }
        }


        public string Description
        {
            get { return _embed.Description; }
            set { _embed.Description = value; }
        }


        public Color Color
        {
            get { return _embed.Color; }
            set { _embed.Color = value; }
        }


        public string Url
        {
            get { return _embed.Url; }
            set { _embed.Url = value; }
        }


        public void AddField(string name, string content, bool inline = false)
        {
            _embed.Fields.Add(new EmbedField() { Name = name, Content = content, Inline = inline });
        }


        public string Thumbnail
        {
            get { return _embed.Thumbnail.Url; }
            set { _embed.Thumbnail.Url = value; }
        }


        public string Image
        {
            get { return _embed.Image.Url; }
            set { _embed.Image.Url = value; }
        }


        public EmbedFooter Footer
        {
            get { return _embed.Footer; }
        }


        public EmbedAuthor Author
        {
            get { return _embed.Author; }
        }


        public EmbedMaker()
        {
            _embed = new Embed();
        }


        public static implicit operator Embed(EmbedMaker instance)
        {
            return instance._embed;
        }


        public override string ToString()
        {
            return _embed.ToString();
        }
    }
}
