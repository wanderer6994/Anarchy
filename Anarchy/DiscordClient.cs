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
                SuperPropertiesInfo.Base64 = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRmlyZWZveCIsImRldmljZSI6IiIsImJyb3dzZXJfdXNlcl9hZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQ7IHJ2OjY5LjApIEdlY2tvLzIwMTAwMTAxIEZpcmVmb3gvNjkuMCIsImJyb3dzZXJfdmVyc2lvbiI6IjY5LjAiLCJvc192ZXJzaW9uIjoiMTAiLCJyZWZlcnJlciI6IiIsInJlZmVycmluZ19kb21haW4iOiIiLCJyZWZlcnJlcl9jdXJyZW50IjoiIiwicmVmZXJyaW5nX2RvbWFpbl9jdXJyZW50IjoiIiwicmVsZWFzZV9jaGFubmVsIjoic3RhYmxlIiwiY2xpZW50X2J1aWxkX251bWJlciI6NDc2MzAsImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9";

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