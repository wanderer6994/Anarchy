using Newtonsoft.Json;

namespace Discord.Gateway
{
    //so far i have not been able to use actual images
    public class ActivityAssets
    {
        [JsonProperty("large_image")]
        public string ImageId { get; set; }

        [JsonProperty("large_text")]
        public string ImageText { get; set; }

        [JsonProperty("small_image")]
        public string IconId { get; set; }

        [JsonProperty("small_text")]
        public string IconText { get; set; }
    }
}
