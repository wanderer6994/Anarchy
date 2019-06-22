using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace Discord.Webhook
{
    public class DiscordWebhookClient
    {
        private readonly HttpClient _client;

        public bool Valid { get; private set; }

        private string _hookUrl;
        public string WebhookUrl
        {
            get { return _hookUrl; }
            set
            {
                _hookUrl = value;

                try
                {
                    this.Validate();
                }
                catch (InvalidWebhookException)
                {
                    Valid = false;
                    throw;
                }

                Webhook hook = GetWebhook();

                if (string.IsNullOrEmpty(Name))
                    Name = hook.Name;
                if (string.IsNullOrEmpty(Avatar))
                    Avatar = $"https://cdn.discordapp.com/avatars/{hook.Id}/{hook.Avatar}.jpg";

                Valid = true;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new InvalidWebhookNameException(this, value);

                _name = value;
            }
        }

        private string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set
            {
                if (value == "")
                    value = null;

                _avatar = value;
            }
        }

        public DiscordWebhookClient()
        {
            _client = new HttpClient();
        }

        public DiscordWebhookClient(string webhookUrl) : this()
        {
            WebhookUrl = webhookUrl;
        }

        public Webhook GetWebhook()
        {
            if (!Valid)
                throw new InvalidWebhookException(this, WebhookUrl);

            var resp = _client.GetAsync(WebhookUrl).Result;

            if (resp.StatusCode != HttpStatusCode.OK)
                throw new InvalidWebhookException(this, WebhookUrl);

            return JsonConvert.DeserializeObject<Webhook>(resp.Content.ReadAsStringAsync().Result);
        }

        public void Validate()
        {
            if (_client.GetAsync(WebhookUrl).Result.StatusCode != HttpStatusCode.OK)
                throw new InvalidWebhookException(this, WebhookUrl);
        }

        public bool SendMessage(string message)
        {
            if (!Valid)
                return false;

            WebhookMessage msg = new WebhookMessage
            {
                Content = message,
                Username = Name,
                Avatar = Avatar
            };

            return _client.PostAsync(WebhookUrl, new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json")).Result.StatusCode == HttpStatusCode.NoContent;
        }
    }
}