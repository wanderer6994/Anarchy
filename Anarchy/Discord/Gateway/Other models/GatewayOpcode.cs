namespace Discord.Gateway
{
    internal enum GatewayOpcode
    {
        Event,
        Heartbeat,
        Identify,
        StatusChange,
        VoiceStateUpdate,
        Resume,
        Reconnect,
        RequestGuildMembers,
        InvalidSession,
        Connected,
        HeartbeatAck
    }
}