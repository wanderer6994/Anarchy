/*
 * Basically DiscordClient but with gateway :)
 */

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Discord.Gateway
{
    public class DiscordSocketClient : DiscordClient
    {
        #region events
        public delegate void UserHandler(DiscordSocketClient client, UserEventArgs args);
        public delegate void UserListHandler(DiscordSocketClient client, UserListEventArgs args);
        public delegate void GuildHandler(DiscordSocketClient client, GuildEventArgs args);
        public delegate void ChannelHandler(DiscordSocketClient client, ChannelEventArgs args);
        public delegate void MessageHandler(DiscordSocketClient client, MessageEventArgs args);
        public delegate void RoleHandler(DiscordSocketClient client, RoleEventArgs args);

        public event UserHandler OnLoggedIn;
        public event UserHandler OnLoggedOut;

        public event GuildHandler OnJoinedGuild;
        public event GuildHandler OnGuildUpdated;
        public event GuildHandler OnLeftGuild;
        
        public event ChannelHandler OnChannelCreated;
        public event ChannelHandler OnChannelUpdated;
        public event ChannelHandler OnChannelDeleted;
        
        public event MessageHandler OnMessageReceived;
        public event MessageHandler OnMessageEdited;

        public event RoleHandler OnRoleCreated;
        public event RoleHandler OnRoleUpdated;

        public delegate void MessageDeletedHandler(DiscordSocketClient client, MessageDeletedEventArgs args);
        public event MessageDeletedHandler OnMessageDeleted;

        public event UserListHandler OnGuildMembersReceived;
        #endregion

        internal WebSocket Socket { get; set; }
        internal string CurrentStatus { get; set; }
        internal int? Sequence { get; set; }
        public bool LoggedIn { get; private set; }

        public DiscordSocketClient() : base()
        { }
        public DiscordSocketClient(string token) : base(token)
        {
            Login();
        }
        ~DiscordSocketClient()
        {
            Logout();
        }

        #region log in
        public void Login()
        {
            if (LoggedIn)
                Logout();

            Socket = new WebSocket("wss://gateway.discord.gg/?v=6&encoding=json");
            Socket.OnMessage += SocketDataReceived;
            Socket.OnClose += SocketClosed;
            Socket.Connect();
        }

        public void Login(string token)
        {
            Token = token;

            Login();
        }
        #endregion

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
                            
                            GatewayLogin lgn = JsonConvert.DeserializeObject<GatewayLogin>(payload.Data.ToString());
                            this.User = lgn.User;
                            OnLoggedIn?.Invoke(this, new UserEventArgs(this.User));
                            break;
                        case "GUILD_CREATE":
                            Guild cGuild = JsonConvert.DeserializeObject<Guild>(payload.Data.ToString());
                            cGuild.Client = this;

                            OnJoinedGuild?.Invoke(this, new GuildEventArgs(cGuild));
                            break;
                        case "GUILD_UPDATE":
                            Guild uGuild = JsonConvert.DeserializeObject<Guild>(payload.Data.ToString());
                            uGuild.Client = this;

                            OnGuildUpdated?.Invoke(this, new GuildEventArgs(uGuild));
                            break;
                        case "GUILD_DELETE":
                            Guild dGuild = JsonConvert.DeserializeObject<Guild>(payload.Data.ToString());
                            dGuild.Client = this;

                            OnLeftGuild?.Invoke(this, new GuildEventArgs(dGuild));
                            break;
                        case "CHANNEL_CREATE":
                            Channel cChannel = JsonConvert.DeserializeObject<Channel>(payload.Data.ToString());
                            cChannel.Client = this;

                            OnChannelCreated?.Invoke(this, new ChannelEventArgs(cChannel));
                            break;
                        case "CHANNEL_UPDATE":
                            Channel uChannel = JsonConvert.DeserializeObject<Channel>(payload.Data.ToString());
                            uChannel.Client = this;

                            OnChannelUpdated?.Invoke(this, new ChannelEventArgs(uChannel));
                            break;
                        case "CHANNEL_DELETE":
                            Channel dChannel = JsonConvert.DeserializeObject<Channel>(payload.Data.ToString());
                            dChannel.Client = this;

                            OnChannelDeleted?.Invoke(this, new ChannelEventArgs(dChannel));
                            break;
                        case "GUILD_ROLE_CREATE":
                            GatewayRole cRole = JsonConvert.DeserializeObject<GatewayRole>(payload.Data.ToString());
                            cRole.Role.Client = this;

                            OnRoleCreated?.Invoke(this, new RoleEventArgs(cRole.Role));
                            break;
                        case "GUILD_ROLE_UPDATE":
                            GatewayRole uRole = JsonConvert.DeserializeObject<GatewayRole>(payload.Data.ToString());
                            uRole.Role.Client = this;

                            OnRoleUpdated?.Invoke(this, new RoleEventArgs(uRole.Role));
                            break;
                        case "MESSAGE_CREATE":
                            Message cMsg = JsonConvert.DeserializeObject<Message>(payload.Data.ToString());
                            cMsg.Client = this;

                            OnMessageReceived?.Invoke(this, new MessageEventArgs(cMsg));
                            break;
                        case "MESSAGE_UPDATE":
                            Message uMsg = JsonConvert.DeserializeObject<Message>(payload.Data.ToString());
                            uMsg.Client = this;

                            OnMessageEdited?.Invoke(this, new MessageEventArgs(uMsg));
                            break;
                        case "MESSAGE_DELETE":
                            OnMessageDeleted?.Invoke(this, new MessageDeletedEventArgs(JsonConvert.DeserializeObject<MessageDelete>(payload.Data.ToString())));
                            break;
                        case "GUILD_MEMBERS_CHUNK":
                            List<User> users = new List<User>();
                            foreach (var guildMember in JsonConvert.DeserializeObject<GateywayMemberChunk>(payload.Data.ToString()).Members)
                                users.Add(guildMember.User);

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