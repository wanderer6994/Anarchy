using Leaf.xNet;
using System.Collections.Generic;
using System.Linq;

namespace Discord
{
    /// <summary>
    /// Discord client that only supports HTTP
    /// </summary>
    public class DiscordClient
    {
        public DiscordHttpClient HttpClient { get; private set; }
        public SPInformation SuperPropertiesInfo { get; private set; }
        public ClientUser User { get; internal set; }

        private string _token;
        public string Token
        {
            get
            {
                return _token;
            }
            set
            {
                string previousToken = Token;

                _token = value;

                try
                {
                    this.GetClientUser();
                }
                catch
                {
                    if (previousToken != null)
                        _token = previousToken;

                    throw;
                }
            }
        }

        public string UserAgent
        {
            get { return SuperPropertiesInfo.Properties.UserAgent; }
            set
            {
                HttpClient.UserAgent = value;
                SuperPropertiesInfo.Properties.UserAgent = value;
            }
        }

        public DiscordClient(bool antiTrack = true)
        {
            HttpClient = new DiscordHttpClient(this);

            SuperPropertiesInfo = new SPInformation();
            SuperPropertiesInfo.OnPropertiesUpdated += OnPropertiesUpdated;

            if (antiTrack)
            {
                SuperPropertiesInfo.Base64 = "eyJvcyI6IkxpbnV4IiwiYnJvd3NlciI6IkZpcmVmb3giLCJkZXZpY2UiOiIiLCJicm93c2VyX3VzZXJfYWdlbnQiOiJNb3ppbGxhLzUuMCAoWDExOyBMaW51eCB4ODZfNjQ7IHJ2OjY5LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvNjkuMCIsImJyb3dzZXJfdmVyc2lvbiI6IjY5LjAiLCJvc192ZXJzaW9uIjoiIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjQ4NDcwLCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==";

                HttpClient.UpdateFingerprint();
            }
        }

        public DiscordClient(string token, bool antiTrack = true) : this(antiTrack)
        {
            Token = token;
        }

        public DiscordClient(string proxy, ProxyType proxyType, bool antiTrack = true) : this(antiTrack)
        {
            HttpClient.SetProxy(proxyType, proxy);
            HttpClient.UpdateFingerprint();
        }


        public DiscordClient(string token, string proxy, ProxyType proxyType, bool antiTrack = true) : this(proxy, proxyType, antiTrack)
        {
            Token = token;
        }
        

        private void OnPropertiesUpdated(object sender, SPUpdatedEventArgs args)
        {
            HttpClient.SuperProperties = args.Properties.Base64;
            UserAgent = args.Properties.Properties.UserAgent;
        }


        public override string ToString()
        {
            return User.ToString();
        }
    }
}