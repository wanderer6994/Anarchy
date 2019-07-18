using Newtonsoft.Json.Linq;
using WebSocketSharp;

namespace Discord.Gateway
{
    /// <summary>
    /// <see cref="DiscordClient"/> with Gateway support
    /// </summary>
    public class DiscordSocketClient : DiscordClient
    {
        #region events
        public delegate void GuildHandler(DiscordSocketClient client, GuildEventArgs args);
        public delegate void ChannelHandler(DiscordSocketClient client, ChannelEventArgs args);
        public delegate void MessageHandler(DiscordSocketClient client, MessageEventArgs args);
        public delegate void ReactionHandler(DiscordSocketClient client, ReactionEventArgs args);
        public delegate void RoleHandler(DiscordSocketClient client, RoleEventArgs args);
        public delegate void BanUpdateHandler(DiscordSocketClient client, BanUpdateEventArgs args);

        public delegate void LoginHandler(DiscordSocketClient client, LoginEventArgs args);
        public event LoginHandler OnLoggedIn;
        public delegate void LogoutHandler(DiscordSocketClient client, UserEventArgs args);
        public event LogoutHandler OnLoggedOut;

        public event GuildHandler OnJoinedGuild;
        public event GuildHandler OnGuildUpdated;
        public event GuildHandler OnLeftGuild;

        public delegate void GuildMemberHandler(DiscordSocketClient client, GuildMemberEventArgs args);
        public event GuildMemberHandler OnGuildMemberUpdated;
        public delegate void GuildMembersHandler(DiscordSocketClient client, GuildMembersEventArgs args);
        public event GuildMembersHandler OnGuildMembersReceived;

        public delegate void PresenceUpdateHandler(DiscordSocketClient client, PresenceUpdatedEventArgs args);
        public event PresenceUpdateHandler OnUserPresenceUpdated;

        public event RoleHandler OnRoleCreated;
        public event RoleHandler OnRoleUpdated;

        public event ChannelHandler OnChannelCreated;
        public event ChannelHandler OnChannelUpdated;
        public event ChannelHandler OnChannelDeleted;

        public delegate void EmojisUpdatedHandler(DiscordSocketClient client, EmojisUpdatedEventArgs args);
        public event EmojisUpdatedHandler OnEmojisUpdated;

        public event MessageHandler OnMessageReceived;
        public event MessageHandler OnMessageEdited;
        public delegate void MessageDeletedHandler(DiscordSocketClient client, MessageDeletedEventArgs args);
        public event MessageDeletedHandler OnMessageDeleted;

        public event ReactionHandler OnMessageReactionAdded;
        public event ReactionHandler OnMessageReactionRemoved;

        public event BanUpdateHandler OnUserBanned;
        public event BanUpdateHandler OnUserUnbanned;
        #endregion


        internal WebSocket Socket { get; set; }
        internal uint? Sequence { get; set; }
        public bool LoggedIn { get; private set; }


        public DiscordSocketClient() : base() { }
        ~DiscordSocketClient()
        {
            Logout();
        }


        public void Login(string token)
        {
            HttpClient.UpdateFingerprint();
            Token = token;

            Socket = new WebSocket("wss://gateway.discord.gg/?v=6&encoding=json");
            Socket.OnMessage += SocketDataReceived;
            Socket.Connect();
        }


        private void logout(bool resume)
        {
            if (LoggedIn)
            {
                LoggedIn = false;
                Socket.Close();

                if (resume)
                {
                    System.Console.WriteLine("Resuming");

                    Login(Token);
                }
                else
                    OnLoggedOut?.Invoke(this, new UserEventArgs(User));
            }
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


        private void SocketDataReceived(object sender, WebSocketSharp.MessageEventArgs result)
        {
            GatewayResponse payload = result.Data.Deserialize<GatewayResponse>();
            Sequence = payload.Sequence;

            System.Console.WriteLine(payload);
            
            switch (payload.Opcode)
            {
                case GatewayOpcode.Event:
                    switch (payload.Title)
                    {
                        case "READY":
                            LoggedIn = true;
                            Login login = payload.Deserialize<Login>().SetClient(this);
                            this.User = login.User;
                            OnLoggedIn?.Invoke(this, new LoginEventArgs(login));
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
                        case "GUILD_MEMBER_UPDATE":
                            OnGuildMemberUpdated?.Invoke(this, new GuildMemberEventArgs(payload.Deserialize<GuildMember>().SetClient(this)));
                            break;
                        case "GUILD_MEMBERS_CHUNK":
                            OnGuildMembersReceived?.Invoke(this, new GuildMembersEventArgs(payload.Deserialize<GuildMemberList>().Members.SetClientsInList(this)));
                            break;
                        case "PRESENCE_UPDATE":
                            OnUserPresenceUpdated?.Invoke(this, new PresenceUpdatedEventArgs(payload.Deserialize<PresenceUpdate>()));
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
                            OnRoleCreated?.Invoke(this, new RoleEventArgs(payload.Deserialize<RoleContainer>().Role.SetClient(this)));
                            break;
                        case "GUILD_ROLE_UPDATE":
                            OnRoleUpdated?.Invoke(this, new RoleEventArgs(payload.Deserialize<RoleContainer>().Role.SetClient(this)));
                            break;
                        case "GUILD_EMOJIS_UPDATE":
                            OnEmojisUpdated?.Invoke(this, new EmojisUpdatedEventArgs(payload.Deserialize<EmojiContainer>().SetClient(this)));
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
                        case "MESSAGE_REACTION_ADD":
                            OnMessageReactionAdded?.Invoke(this, new ReactionEventArgs(payload.Deserialize<MessageReactionUpdate>().SetClient(this)));
                            break;
                        case "MESSAGE_REACTION_REMOVE":
                            OnMessageReactionRemoved?.Invoke(this, new ReactionEventArgs(payload.Deserialize<MessageReactionUpdate>().SetClient(this)));
                            break;
                        case "GUILD_BAN_ADD":
                            OnUserBanned?.Invoke(this, new BanUpdateEventArgs(payload.Deserialize<BanContainer>().SetClient(this)));
                            break;
                        case "GUILD_BAN_REMOVE":
                            OnUserUnbanned?.Invoke(this, new BanUpdateEventArgs(payload.Deserialize<BanContainer>().SetClient(this)));
                            break;
                    }
                    break;
                case GatewayOpcode.InvalidSession:
                    logout(LoggedIn);
                    break;
                case GatewayOpcode.Connected:
                    this.StartHeartbeatHandlersAsync(payload.Deserialize<JObject>().GetValue("heartbeat_interval").ToObject<uint>());
                    this.LoginToGateway();
                    break;
            }
        }
    }
}