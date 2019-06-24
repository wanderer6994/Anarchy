namespace Discord
{
    public class DiscordClient
    {
        internal DiscordHttpClient HttpClient { get; private set; }
        public SuperPropertiesInformation SuperPropertiesInfo { get; private set; }
        public ClientUser User { get; internal set; }

        private string _token;
        public string Token
        {
            get { return _token; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                _token = value;
                HttpClient.Headers.Remove("Authorization");
                HttpClient.Headers.Add("Authorization", _token);

                //this.GetClientUser();
            }
        }

        public string UserAgent
        {
            get { return SuperPropertiesInfo.Properties.UserAgent; }
            set
            {
                HttpClient.Headers.Remove("User-Agent");
                HttpClient.Headers.Add("User-Agent", value);
                SuperPropertiesInfo.Properties.UserAgent = value;
            }
        }
        
        public DiscordClient()
        {
            HttpClient = new DiscordHttpClient(this);
            HttpClient.Headers.Add("X-Fingerprint", this.GetFingerprint());

            SuperPropertiesInfo = new SuperPropertiesInformation();
            SuperPropertiesInfo.OnPropertiesUpdated += OnPropertiesUpdated;
            SuperPropertiesInfo.Base64 = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzc0LjAuMzcyOS4xNjkgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6Ijc0LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6Mzk4MjcsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9";
        }

        public DiscordClient(string token) : this()
        {
            Token = token;
        }
        
        private void OnPropertiesUpdated(object sender, SPUpdatedEventArgs args)
        {
            HttpClient.Headers.Remove("X-Super-Properties");
            HttpClient.Headers.Add("X-Super-Properties", args.PropertiesInfo.Base64);
            UserAgent = args.PropertiesInfo.Properties.UserAgent;
        }
    }
}