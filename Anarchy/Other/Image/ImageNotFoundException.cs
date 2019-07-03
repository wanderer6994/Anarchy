using System;

namespace Discord
{
    public class ImageNotFoundException : Exception
    {
        public string ImageId { get; private set; }

        public ImageNotFoundException(string imageId) : base("Image not found")
        {
            ImageId = imageId;
        }


        public override string ToString()
        {
            return ImageId;
        }
    }
}
