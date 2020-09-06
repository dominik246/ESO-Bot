using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot.Commands
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient _client;
        public Ping(DiscordSocketClient client)
        {
            _client = client;
        }
        [Command("ping")]
        public async Task PingPong()
        {
            await ReplyAsync($"Replying Pong with {_client.Latency}ms latency!");
        }
    }
}
