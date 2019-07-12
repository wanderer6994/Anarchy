using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.Webhook
{
    /// <summary>
    /// Options for sending a message through a webhook
    /// </summary>
    internal class WebhookMessageProperties : WebhookProfile
    {
        [JsonProperty("content")]
        public string Content { get; set; }


        [JsonProperty("embeds")]
        private List<Embed> _embeds;
        public Embed Embed
        {
            get
            {
                return _embeds == null || _embeds.Count == 0 ? null : _embeds[0];
            }
            set
            {
                if (value == null)
                    _embeds = null;
                else
                    _embeds = new List<Embed>() { value };
            }
        }
    }
}