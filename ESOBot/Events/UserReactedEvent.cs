using Discord;
using Discord.WebSocket;

using ESOBot.Services;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESOBot.Events
{
    public class UserReactedEvent
    {
        private readonly AddRole _addRole;
        private readonly DiscordSocketClient _client;
        public UserReactedEvent(AddRole addRole, DiscordSocketClient client)
        {
            _addRole = addRole;
            _client = client;
        }
        public async Task OnReaction(Cacheable<IUserMessage, ulong> msg, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var entity = await msg.GetOrDownloadAsync();
            if (!entity.Author.IsBot)
                return;

            if (!entity.Content.Contains("react", StringComparison.OrdinalIgnoreCase))
                return;

            var emote = reaction.Emote;

            if ((await entity.GetReactionUsersAsync(emote, 10).FirstAsync()).Count(u => u.IsBot) == 0)
                return;

            var userId = reaction.UserId;

            if (_client.GetUser(userId).IsBot)
                return;

            try
            {
                var test = reaction.User.Value as SocketGuildUser;

                //var socketGuildUser = (SocketGuildUser)_client.GetUser(userId);
                var count = test.Guild.Roles.Count(r => string.Equals(r.Name, emote.Name, StringComparison.OrdinalIgnoreCase));
                if (count == 0)
                    return;

                await _addRole.AddAsync(test, test.Guild.Roles.First(r => string.Equals(r.Name, emote.Name, StringComparison.OrdinalIgnoreCase)).Id);
                await entity.RemoveReactionAsync(emote, userId);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
