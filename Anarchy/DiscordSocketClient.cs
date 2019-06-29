using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Discord.Gateway
{
    public class DiscordSocketClient : DiscordClient
    {
        #region events
        public delegate void UserHandler(DiscordSocketClient client, UserEventArgs args);
        public delegate void GuildHandler(DiscordSocketClient client, GuildEventArgs args);
        public delegate void ChannelHandler(DiscordSocketClient client, ChannelEventArgs args);
        public delegate void MessageHandler(DiscordSocketClient client, MessageEventArgs args);
        public delegate void RoleHandler(DiscordSocketClient client, RoleEventArgs args);

        public delegate void LoggedInHandler(DiscordSocketClient client, GatewayLoginEventArgs args);
        public event LoggedInHandler OnLoggedIn;
        public event UserHandler OnLoggedOut;

        public event GuildHandler OnJoinedGuild;
        public event GuildHandler OnGuildUpdated;
        public event GuildHandler OnLeftGuild;

        public delegate void UserListHandler(DiscordSocketClient client, UserListEventArgs args);
        public event UserListHandler OnGuildMembersReceived;

        public event RoleHandler OnRoleCreated;
        public event RoleHandler OnRoleUpdated;

        public event ChannelHandler OnChannelCreated;
        public event ChannelHandler OnChannelUpdated;
        public event ChannelHandler OnChannelDeleted;
        
        public event MessageHandler OnMessageReceived;
        public event MessageHandler OnMessageEdited;
        public event MessageHandler OnMessageDeleted;
        #endregion

        internal WebSocket Socket { get; set; }
        internal string CurrentStatus { get; set; }
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

        #region socket events
        private void SocketClosed(object sender, CloseEventArgs e)
        {
            //idk how to resume so we just make it log in again
            if (LoggedIn)
                this.LoginToGateway();
        }

        private void SocketDataReceived(object sender, WebSocketSharp.MessageEventArgs result)
        {
            var payload = JsonConvert.DeserializeObject<GatewayResponse>(result.Data);
            Sequence = payload.Sequence;

            switch (payload.Opcode)
            {
                case GatewayOpcode.Event:
                    switch (payload.Title)
                    {
                        case "READY":
                            LoggedIn = true;
                            GatewayLogin login = payload.Deserialize<GatewayLogin>().SetClient(this);
                            this.User = login.User;
                            OnLoggedIn?.Invoke(this, new GatewayLoginEventArgs(login));
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
                        case "CHANNEL_CREATE":
                            OnChannelCreated?.Invoke(this, new ChannelEventArgs(payload.Deserialize<Channel>().SetClient(this)));
                            break;
                        case "CHANNEL_UPDATE":
                            OnChannelUpdated?.Invoke(this, new ChannelEventArgs(payload.Deserialize<Channel>().SetClient(this)));
                            break;
                        case "CHANNEL_DELETE":
                            OnChannelDeleted?.Invoke(this, new ChannelEventArgs(payload.Deserialize<Channel>().SetClient(this)));
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
                            //it should be noted that evrything but the message id, channel id, and guild id will be null.
                            OnMessageDeleted?.Invoke(this, new MessageEventArgs(payload.Deserialize<Message>().SetClient(this)));
                            break;
                        case "GUILD_MEMBERS_CHUNK":
                            List<User> users = new List<User>();
                            foreach (var member in payload.Deserialize<GuildMemberList>().Members)
                                users.Add(member.User.SetClient(this));

                            OnGuildMembersReceived?.Invoke(this, new UserListEventArgs(users));
                            break;
                    }
                    break;
                case GatewayOpcode.InvalidSession:
                    Logout();
                    break;
                case GatewayOpcode.Connected:
                    //keep sending heartbeats every x second so the client's socket don't get closed
                    Task.Run(async () => await this.StartHeartbeatHandlersAsync(payload.Deserialize<GatewayHeartbeat>().Interval));

                    this.LoginToGateway();
                    break;
            }
        }
        #endregion
    }
}