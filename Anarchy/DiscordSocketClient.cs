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

        public event UserHandler OnLoggedIn;
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

                            this.User = payload.Deserialize<GatewayLogin>().User;
                            OnLoggedIn?.Invoke(this, new UserEventArgs(this.User));
                            break;
                        case "GUILD_CREATE":
                            Guild cGuild = payload.Deserialize<Guild>();
                            cGuild.Client = this;

                            OnJoinedGuild?.Invoke(this, new GuildEventArgs(cGuild));
                            break;
                        case "GUILD_UPDATE":
                            Guild uGuild = payload.Deserialize<Guild>();
                            uGuild.Client = this;

                            OnGuildUpdated?.Invoke(this, new GuildEventArgs(uGuild));
                            break;
                        case "GUILD_DELETE":
                            Guild dGuild = payload.Deserialize<Guild>();
                            dGuild.Client = this;

                            OnLeftGuild?.Invoke(this, new GuildEventArgs(dGuild));
                            break;
                        case "CHANNEL_CREATE":
                            Channel cChannel = payload.Deserialize<Channel>();
                            cChannel.Client = this;

                            OnChannelCreated?.Invoke(this, new ChannelEventArgs(cChannel));
                            break;
                        case "CHANNEL_UPDATE":
                            Channel uChannel = payload.Deserialize<Channel>();
                            uChannel.Client = this;

                            OnChannelUpdated?.Invoke(this, new ChannelEventArgs(uChannel));
                            break;
                        case "CHANNEL_DELETE":
                            Channel dChannel = payload.Deserialize<Channel>();
                            dChannel.Client = this;

                            OnChannelDeleted?.Invoke(this, new ChannelEventArgs(dChannel));
                            break;
                        case "GUILD_ROLE_CREATE":
                            GatewayRole cRole = payload.Deserialize<GatewayRole>();
                            cRole.Role.Client = this;

                            OnRoleCreated?.Invoke(this, new RoleEventArgs(cRole.Role));
                            break;
                        case "GUILD_ROLE_UPDATE":
                            GatewayRole uRole = payload.Deserialize<GatewayRole>();
                            uRole.Role.Client = this;

                            OnRoleUpdated?.Invoke(this, new RoleEventArgs(uRole.Role));
                            break;
                        case "MESSAGE_CREATE":
                            Message cMsg = payload.Deserialize<Message>();
                            cMsg.Client = this;

                            OnMessageReceived?.Invoke(this, new MessageEventArgs(cMsg));
                            break;
                        case "MESSAGE_UPDATE":
                            Message uMsg = payload.Deserialize<Message>();
                            uMsg.Client = this;

                            OnMessageEdited?.Invoke(this, new MessageEventArgs(uMsg));
                            break;
                        case "MESSAGE_DELETE":
                            //it should be noted that evrything but the message id, channel id, and guild id will be null.
                            OnMessageDeleted?.Invoke(this, new MessageEventArgs(JsonConvert.DeserializeObject<Message>(payload.Data.ToString())));
                            break;
                        case "GUILD_MEMBERS_CHUNK":
                            List<User> users = new List<User>();
                            payload.Deserialize<GuildMemberList>().Members.ForEach(member => users.Add(member.User));

                            OnGuildMembersReceived?.Invoke(this, new UserListEventArgs(users));
                            break;
                    }
                    break;
                case GatewayOpcode.InvalidSession:
                    Logout();
                    break;
                case GatewayOpcode.Connected:
                    //keep sending heartbeats every x second so the client's socket don't get closed
                    Task.Run(async () => await this.StartHeartbeatHandlersAsync(JsonConvert.DeserializeObject<GatewayHeartbeat>(payload.Data.ToString()).Interval));

                    this.LoginToGateway();
                    break;
            }
        }
        #endregion
    }
}