namespace Discord.Gateway
{
    //idk if there are any opcodes between 3 and 9. discord isn't telling us at least ¯\_(ツ)_/¯
    internal enum GatewayOpcode
    {
        Event,
        Heartbeat,
        Login,
        StatusChange,
        InvalidSession = 9,
        Connected
    }
}
