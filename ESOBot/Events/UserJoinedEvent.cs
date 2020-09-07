using Discord.WebSocket;

using ESOBot.Services;

using System.Threading.Tasks;

namespace ESOBot.Events
{
    public class UserJoinedEvent
    {
        private readonly AddRole _addRole;
        public UserJoinedEvent(AddRole addRole)
        {
            _addRole = addRole;
        }

        public async Task OnUserJoin(SocketGuildUser user)
        {
            await _addRole.AddAsync(user, 750482071750967356);
            await _addRole.AddAsync(user, 752231713974911017);
        }
    }
}
