using Leaf.xNet;
using System;
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
        public SPInformation SuperProperties { get; private set; }
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
                catch (DiscordHttpException)
                {
                    if (previousToken != null)
                        _token = previousToken;

                    throw new InvalidTokenException(value);
                }
                catch
                {
                    throw;
                }
            }
        }

        public string UserAgent
        {
            get { return SuperProperties.Properties.UserAgent; }
            set
            {
                HttpClient.UserAgent = value;
                SuperProperties.Properties.UserAgent = value;
            }
        }

        public DiscordClient(bool antiTrack = true)
        {
            HttpClient = new DiscordHttpClient(this);

            SuperProperties = new SPInformation();
            SuperProperties.OnPropertiesUpdated += (sender, args) => 
            {
                HttpClient.SuperProperties = args.Properties.Base64;
                UserAgent = args.Properties.Properties.UserAgent;
            };

            if (antiTrack)
            {
                SuperProperties.Base64 = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiRGlzY29yZCBDbGllbnQiLCJyZWxlYXNlX2NoYW5uZWwiOiJzdGFibGUiLCJjbGllbnRfdmVyc2lvbiI6IjAuMC4zMDUiLCJvc192ZXJzaW9uIjoiMTAuMC4xODM2MiIsIm9zX2FyY2giOiJ4NjQiLCJjbGllbnRfYnVpbGRfbnVtYmVyIjo1MDIzNSwiY2xpZW50X2V2ZW50X3NvdXJjZSI6bnVsbH0=";

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
        }


        public DiscordClient(string token, string proxy, ProxyType proxyType, bool antiTrack = true) : this(proxy, proxyType, antiTrack)
        {
            Token = token;
        }
        

        public override string ToString()
        {
            return User.ToString();
        }
    }
}