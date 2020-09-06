using Discord;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot.Services
{
    public class AddRole
    {
        public async Task AddAsync(SocketGuildUser user, ulong roleId)
        {
            await user.AddRoleAsync(user.Guild.GetRole(roleId));
        }
    }
}
