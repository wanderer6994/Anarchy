using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Discord
{
    /// <summary>
    /// Used for easily converting <see cref="System.Drawing.Image"/>s to Discord's own Base64 type format
    /// </summary>
    public class DiscordImage
    {
        public string Base64 { get; private set; }

        private Image _image;
        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                    Base64 = null;
                else
                {
                    string type;

                    if (ImageFormat.Jpeg.Equals(value.RawFormat))
                        type = "jpeg";
                    else if (ImageFormat.Png.Equals(value.RawFormat))
                        type = "png";
                    else if (ImageFormat.Gif.Equals(value.RawFormat))
                        type = "gif";
                    else return;

                    Base64 = $"data:image/{type};base64," + 
                            Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(value, typeof(byte[])));
                    _image = value;
                }
            }
        }


        public static implicit operator string(DiscordImage instance)
        {
            return instance.Base64;
        }
    }
}