using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    /// <summary>
    /// Discord client that only supports HTTP
    /// </summary>
    public class DiscordClient
    {
        internal DiscordHttpClient HttpClient { get; private set; }
        public SPInformation SuperPropertiesInfo { get; private set; }
        public ClientUser User { get; internal set; }
        public string Token
        {
            get
            {
#pragma warning disable IDE0018
                IEnumerable<string> values;
#pragma warning restore IDE0018
                if (!HttpClient.Headers.TryGetValues("Authorization", out values))
                    return null;
                else
                    return values.ElementAt(0);
            }
            set
            {
                string previousToken = Token;

                HttpClient.Headers.Remove("Authorization");
                HttpClient.Headers.Add("Authorization", value);

                try
                {
                    this.GetClientUser();
                }
                catch
                {
                    HttpClient.Headers.Remove("Authorization");
                    if (previousToken != null)
                        HttpClient.Headers.Add("Authorization", previousToken);

                    throw;
                }
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
            HttpClient.UpdateFingerprint();

            SuperPropertiesInfo = new SPInformation();
            SuperPropertiesInfo.OnPropertiesUpdated += OnPropertiesUpdated;
            SuperPropertiesInfo.Base64 = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzc0LjAuMzcyOS4xNjkgU2FmYXJpLzUzNy4zNiIsImJyb3dzZXJfdmVyc2lvbiI6Ijc0LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6NDE5MzMsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9";
        }

        public DiscordClient(string token) : this()
        {
            Token = token;
        }
        

        private void OnPropertiesUpdated(object sender, SPUpdatedEventArgs args)
        {
            HttpClient.Headers.Remove("X-Super-Properties");
            HttpClient.Headers.Add("X-Super-Properties", args.Properties.Base64);
            UserAgent = args.Properties.Properties.UserAgent;
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}