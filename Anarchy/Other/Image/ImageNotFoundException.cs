using System;
using System.Net.Http;

namespace Discord
{
    public class ImageNotFoundException : Exception
    {
        public string ImageId { get; private set; }

        public ImageNotFoundException(string imageId) : base("Image not found")
        {
            ImageId = imageId;
        }
    }
}
