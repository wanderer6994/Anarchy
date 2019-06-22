using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayRequest<T> where T : GatewayData, new()
    {
        public GatewayRequest(GatewayOpcode opcode, string token)
        {
            Data = new T
            {
                Token = token
            };
            Opcode = opcode;
        }

        [JsonProperty("op")]
        public GatewayOpcode Opcode { get; set; }

        [JsonProperty("d")]
        public T Data { get; set; }
    }
}