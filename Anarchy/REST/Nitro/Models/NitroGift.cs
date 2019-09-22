using System;
using Newtonsoft.Json;

namespace Discord
{
    public class NitroGift : Controllable
    {
        [JsonProperty("subscription_plan")]
        public NitroSubscriptionPlan SubscriptionPlan { get; private set; }


        [JsonProperty("application_id")]
        public ulong ApplicationId { get; private set; }


        [JsonProperty("redeemed")]
        public bool Redeemed { get; private set; }


        [JsonProperty("expires_at")]
#pragma warning disable CS0649
        private readonly string _expiresAt;
#pragma warning restore CS0649


        public DateTime ExpiresAt
        {
            get { return DiscordTimestamp.FromString(_expiresAt); }
        }


        [JsonProperty("code")]
        public string Code { get; private set; }


        [JsonProperty("user")]
        public User Gifter { get; private set; }


        [JsonProperty("max_uses")]
        public int MaxUses { get; private set; }


        [JsonProperty("id")]
        public ulong Id { get; private set; }


        [JsonProperty("summary")]
        public string Summary { get; private set; }


        public void Redeem(ulong? channelId = null)
        {
            if (Redeemed)
                return;

            Client.RedeemNitroGift(Code, channelId);
        }


        public override string ToString()
        {
            return SubscriptionPlan.Name;
        }
    }
}
