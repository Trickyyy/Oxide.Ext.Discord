﻿using System;
using System.Collections.Generic;
using Oxide.Ext.Discord.WebSockets;
using Oxide.Ext.Discord.RESTObjects;
using Newtonsoft.Json.Linq;

namespace Oxide.Ext.Discord.DiscordObjects
{
    public class Guild
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string splash { get; set; }
        public string owner_id { get; set; }
        public string region { get; set; }
        public string afk_channel_id { get; set; }
        public int afk_timeout { get; set; }
        public bool embed_enabled { get; set; }
        public string embed_channel_id { get; set; }
        public int verification_level { get; set; }
        public int default_message_notifications { get; set; }
        public int explicit_content_filter { get; set; }
        public List<Role> roles { get; set; }
        public List<Emoji> emojis { get; set; }
        public List<string> features { get; set; }
        public int mfa_level { get; set; }
        public string application_id { get; set; }
        public bool widget_enabled { get; set; }
        public string widget_channel_id { get; set; }
        public string joined_at { get; set; }
        public bool large { get; set; }
        public bool unavailable { get; set; }
        public int member_count { get; set; }
        public List<VoiceState> voice_states { get; set; }
        public List<Member> members { get; set; }
        public List<Channel> channels { get; set; }
        public List<Presence> presences { get; set; }

        public static void CreateGuild(DiscordClient client, string name, string region, string icon, int verificationLevel, int defaultMessageNotifications, List<Role> roles, List<Channel> channels, Action<Guild> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "region", region },
                { "icon", icon },
                { "verification_level", verificationLevel },
                { "default_message_notifications", defaultMessageNotifications },
                { "roles", roles },
                { "channels", channels }
            };
            client.REST.DoRequest<Guild>($"/guilds", "POST", jsonObj, (returnValue) =>
            {
                callback?.Invoke(returnValue as Guild);
            });
        }

        public static void GetGuild(DiscordClient client, string guildID, Action<Guild> callback = null)
        {
            client.REST.DoRequest<Guild>($"/guilds/{guildID}", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as Guild);
            });
        }

        public void ModifyGuild(DiscordClient client, Action<Guild> callback = null)
        {
            client.REST.DoRequest<Guild>($"/guilds/{id}", "PATCH", this, (returnValue) =>
            {
                callback?.Invoke(returnValue as Guild);
            });
        }

        public void DeleteGuild(DiscordClient client, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}", "DELETE", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void GetGuildChannels(DiscordClient client, Action<List<Channel>> callback = null)
        {
            client.REST.DoRequest<List<Channel>>($"/guilds/{id}/channels", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<Channel>);
            });
        }

        public void CreateGuildChannel(DiscordClient client, string name, string type, int bitrate, int userLimit, List<Overwrite> permissionOverwrites, Action<Channel> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "name", name },
                { "type", type },
                { "bitrate", bitrate },
                { "user_limit", userLimit },
                { "permission_overwrites", permissionOverwrites }
            };
            client.REST.DoRequest<Channel>($"/guilds/{id}/channes", "POST", jsonObj, (returnValue) =>
            {
                callback?.Invoke(returnValue as Channel);
            });
        }

        public void ModifyGuildChannelPositions(DiscordClient client, List<ObjectPosition> positions, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/channels", "PATCH", positions, () =>
            {
                callback?.Invoke();
            });
        }

        public void GetGuildMember(DiscordClient client, string userID, Action<GuildMember> callback = null)
        {
            client.REST.DoRequest<GuildMember>($"/guilds/{id}/members/{userID}", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as GuildMember);
            });
        }

        public void ListGuildMembers(DiscordClient client, Action<List<GuildMember>> callback = null)
        {
            client.REST.DoRequest<List<GuildMember>>($"/guilds/{id}/members", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<GuildMember>);
            });
        }

        public void AddGuildMember(DiscordClient client, string userID, string accessToken, string nick, List<Role> roles, bool mute, bool deaf, Action<GuildMember> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "access_token", accessToken },
                { "nick", nick },
                { "roles", roles },
                { "mute", mute },
                { "deaf", deaf }
            };
            client.REST.DoRequest<GuildMember>($"/guilds/{id}/members/{userID}", "PUT", jsonObj, (returnValue) =>
            {
                callback?.Invoke(returnValue as GuildMember);
            });
        }

        public void ModifyGuildMemeber(DiscordClient client, string userID, string nick, List<Role> roles, bool mute, bool deaf, string channelId, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick },
                { "roles", roles },
                { "mute", mute },
                { "deaf", deaf },
                { "channel_id", channelId }
            };
            client.REST.DoRequest($"/guilds/{id}/members/{userID}", "PATCH", jsonObj, () =>
            {
                callback?.Invoke();
            });
        }

        public void ModifyCurrentUsersNick(DiscordClient client, string nick, Action<string> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "nick", nick }
            };
            client.REST.DoRequest<string>($"/guilds/{id}/members/@me/nick", "PATCH", jsonObj, (returnValue) =>
            {
                callback?.Invoke(returnValue as string);
            });
        }

        public void AddGuildMemberRole(DiscordClient client, string userID, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}/roles/{roleID}", "PUT", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void RemoveGuildMemberRole(DiscordClient client, string userID, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}/{roleID}", "DELETE", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void RemoveGuildMember(DiscordClient client, string userID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/members/{userID}", "DELETE", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void GetGuildBans(DiscordClient client, Action<List<Ban>> callback = null)
        {
            client.REST.DoRequest<List<Ban>>($"/guilds/{id}/bans", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<Ban>);
            });
        }

        public void CreateGuildBan(DiscordClient client, string userID, int deleteMessageDays, Action callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "delete-message-days", deleteMessageDays }
            };
            client.REST.DoRequest($"/guilds/{id}/bans/{userID}", "PUT", jsonObj, () =>
            {
                callback?.Invoke();
            });
        }

        public void RemoveGuildBan(DiscordClient client, string userID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/bans/{userID}", "DELETE", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void GetGuildRoles(DiscordClient client, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest<List<Role>>($"/guilds/{id}/roles", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<Role>);
            });
        }

        public void CreateGuildRole(DiscordClient client, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest<Role>($"/guilds/{id}/roles", "POST", role, (returnValue) =>
            {
                callback?.Invoke(returnValue as Role);
            });
        }

        public void ModifyGuildRolePositions(DiscordClient client, List<ObjectPosition> positions, Action<List<Role>> callback = null)
        {
            client.REST.DoRequest<List<Role>>($"/guilds/{id}/roles", "PATCH", positions, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<Role>);
            });
        }

        public void ModifyGuildRole(DiscordClient client, string roleID, Role role, Action<Role> callback = null)
        {
            client.REST.DoRequest<Role>($"/guilds/{id}/roles/{roleID}", "PATCH", role, (returnValue) =>
            {
                callback?.Invoke(returnValue as Role);
            });
        }

        public void DeleteGuildRole(DiscordClient client, string roleID, Action callback = null)
        {
            client.REST.DoRequest($"/guilds/{id}/roles/{roleID}", "DELETE", null, () =>
            {
                callback?.Invoke();
            });
        }

        public void GetGuildPruneCount(DiscordClient client, int days, Action<int> callback = null)
        {
            client.REST.DoRequest<JObject>($"/guilds/{id}/prune?days={days}", "GET", null, (returnValue) =>
            {
                callback?.Invoke((int)(returnValue as JObject).GetValue("pruned").ToObject((typeof(int))));
            });
        }

        public void BeginGuildPrune(DiscordClient client, int days, Action<int> callback = null)
        {
            var jsonObj = new Dictionary<string, object>()
            {
                { "days", days }
            };
            client.REST.DoRequest<JObject>($"/guilds/{id}/prune", "POST", jsonObj, (returnValue) =>
            {
                callback?.Invoke((int)(returnValue as JObject).GetValue("pruned").ToObject(typeof(int)));
            });
        }

        public void GetGuildVoiceRegions(DiscordClient client, Action<List<VoiceRegion>> callback = null)
        {
            client.REST.DoRequest<List<VoiceRegion>>($"/guilds/{id}/regions", "GET", null, (returnValue) =>
            {
                callback?.Invoke(returnValue as List<VoiceRegion>);
            });
        }

        //public void GetGuildInvites()
    }
}