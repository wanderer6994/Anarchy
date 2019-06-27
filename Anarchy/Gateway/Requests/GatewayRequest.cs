using Newtonsoft.Json;

namespace Discord.Gateway
{
    internal class GatewayRequest<T> where T : new()
    {
        public GatewayRequest(GatewayOpcode opcode)
        {
            Data = new T();
            Opcode = opcode;
        }

        [JsonProperty("op")]
        public GatewayOpcode Opcode { get; set; }

        [JsonProperty("d")]
        public T Data { get; set; }


        public override string ToString()
        {
            return Opcode.ToString();
        }
    }
}