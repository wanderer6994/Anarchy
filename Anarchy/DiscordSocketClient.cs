using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebSocketSharp;

namespace Discord.Gateway
{
    public class DiscordSocketClient : DiscordClient
    {
        #region events
        public delegate void GuildHandler(DiscordSocketClient client, GuildEventArgs args);
        public delegate void ChannelHandler(DiscordSocketClient client, ChannelEventArgs args);
        public delegate void MessageHandler(DiscordSocketClient client, MessageEventArgs args);
        public delegate void RoleHandler(DiscordSocketClient client, RoleEventArgs args);

        public delegate void LoggedInHandler(DiscordSocketClient client, GatewayLoginEventArgs args);
        public event LoggedInHandler OnLoggedIn;
        public delegate void LoggedOut(DiscordSocketClient client, UserEventArgs args);
        public event LoggedOut OnLoggedOut;

        public event GuildHandler OnJoinedGuild;
        public event GuildHandler OnGuildUpdated;
        public event GuildHandler OnLeftGuild;

        public delegate void GuildMembersHandler(DiscordSocketClient client, GuildMembersEventArgs args);
        public event GuildMembersHandler OnGuildMembersReceived;

        public event RoleHandler OnRoleCreated;
        public event RoleHandler OnRoleUpdated;

        public event ChannelHandler OnChannelCreated;
        public event ChannelHandler OnChannelUpdated;
        public event ChannelHandler OnChannelDeleted;
        
        public event MessageHandler OnMessageReceived;
        public event MessageHandler OnMessageEdited;
        public delegate void MessageDeletedHandler(DiscordSocketClient client, MessageDeletedEventArgs args);
        public event MessageDeletedHandler OnMessageDeleted;
        #endregion

        internal WebSocket Socket { get; set; }
        internal int? Sequence { get; set; }
        public bool LoggedIn { get; private set; }


        public DiscordSocketClient() : base() { }
        ~DiscordSocketClient()
        {
            Logout();
        }


        public void Login(string token)
        {
            Logout();

            HttpClient.UpdateFingerprint();

            Token = token;

            Socket = new WebSocket("wss://gateway.discord.gg/?v=6&encoding=json");
            Socket.OnMessage += SocketDataReceived;
            Socket.OnClose += SocketClosed;
            Socket.Connect();
        }


        public void Logout()
        {
            if (LoggedIn)
            {
                LoggedIn = false;
                Socket.Close();

                OnLoggedOut?.Invoke(this, new UserEventArgs(User));
            }
        }


        private void SocketClosed(object sender, CloseEventArgs e)
        {
            if (LoggedIn) this.LoginToGateway();
        }


        private void SocketDataReceived(object sender, WebSocketSharp.MessageEventArgs result)
        {
            var payload = JsonConvert.DeserializeObject<GatewayResponse>(result.Data);
            Sequence = payload.Sequence;

            System.Console.WriteLine($"{payload.Opcode} | {payload.Title}");

            switch (payload.Opcode)
            {
                case GatewayOpcode.Event:
                    switch (payload.Title)
                    {
                        case "READY":
                            LoggedIn = true;
                            GatewayLogin login = payload.Deserialize<GatewayLogin>().SetClient(this);
                            this.User = login.User;
                            OnLoggedIn?.Invoke(this, new GatewayLoginEventArgs(payload.Deserialize<GatewayLogin>().SetClient(this)));
                            break;
                        case "GUILD_CREATE":
                            OnJoinedGuild?.Invoke(this, new GuildEventArgs(payload.Deserialize<Guild>().SetClient(this)));
                            break;
                        case "GUILD_UPDATE":
                            OnGuildUpdated?.Invoke(this, new GuildEventArgs(payload.Deserialize<Guild>().SetClient(this)));
                            break;
                        case "GUILD_DELETE":
                            OnLeftGuild?.Invoke(this, new GuildEventArgs(payload.Deserialize<Guild>().SetClient(this)));
                            break;
                        case "GUILD_MEMBERS_CHUNK":
                            GuildMemberList list = payload.Deserialize<GuildMemberList>().SetClient(this);
                            foreach (var member in list.Members) member.GuildId = list.GuildId;

                            OnGuildMembersReceived?.Invoke(this, new GuildMembersEventArgs(list.Members));
                            break;
                        case "CHANNEL_CREATE":
                            OnChannelCreated?.Invoke(this, new ChannelEventArgs(payload.Deserialize<GuildChannel>().SetClient(this)));
                            break;
                        case "CHANNEL_UPDATE":
                            OnChannelUpdated?.Invoke(this, new ChannelEventArgs(payload.Deserialize<GuildChannel>().SetClient(this)));
                            break;
                        case "CHANNEL_DELETE":
                            OnChannelDeleted?.Invoke(this, new ChannelEventArgs(payload.Deserialize<GuildChannel>().SetClient(this)));
                            break;
                        case "GUILD_ROLE_CREATE":
                            OnRoleCreated?.Invoke(this, new RoleEventArgs(payload.Deserialize<GatewayRole>().Role.SetClient(this)));
                            break;
                        case "GUILD_ROLE_UPDATE":
                            OnRoleUpdated?.Invoke(this, new RoleEventArgs(payload.Deserialize<GatewayRole>().Role.SetClient(this)));
                            break;
                        case "MESSAGE_CREATE":
                            OnMessageReceived?.Invoke(this, new MessageEventArgs(payload.Deserialize<Message>().SetClient(this)));
                            break;
                        case "MESSAGE_UPDATE":
                            OnMessageEdited?.Invoke(this, new MessageEventArgs(payload.Deserialize<Message>().SetClient(this)));
                            break;
                        case "MESSAGE_DELETE":
                            OnMessageDeleted?.Invoke(this, new MessageDeletedEventArgs(payload.Deserialize<DeletedMessage>()));
                            break;
                    }
                    break;
                case GatewayOpcode.InvalidSession:
                    Logout();
                    break;
                case GatewayOpcode.Connected:
                    this.StartHeartbeatHandlersAsync(payload.Deserialize<GatewayHeartbeat>().Interval);

                    this.LoginToGateway();
                    break;
            }
        }
    }
}