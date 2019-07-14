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
        public string ImageBase64 { get; private set; }

        private Image _image;
        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                    ImageBase64 = null;
                else
                {
                    ImageType type;

                    if (ImageFormat.Jpeg.Equals(value.RawFormat))
                        type = ImageType.Jpeg;
                    else if (ImageFormat.Png.Equals(value.RawFormat))
                        type = ImageType.Png;
                    else if (ImageFormat.Gif.Equals(value.RawFormat))
                        type = ImageType.Gif;
                    else return;

                    ImageBase64 = $"data:image/{type.ToString().ToLower()};base64," + 
                            Convert.ToBase64String((byte[])new ImageConverter().ConvertTo(value, typeof(byte[])));
                    _image = value;
                }
            }
        }
    }
}