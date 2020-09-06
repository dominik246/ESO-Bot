using Discord;
using Discord.Commands;
using Discord.WebSocket;

using ESOBot.Events;
using ESOBot.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot.Commands
{
    public class ReactMessage : ModuleBase<SocketCommandContext>
    {
        private readonly ReactMessageService _service;
        public ReactMessage(ReactMessageService service)
        {
            _service = service;
        }
        [Command("reactmessage", RunMode = RunMode.Async)]
        public async Task Create()
        {
            var msg = await ReplyAsync("React with the role to assign it by it's name. When you're done react with :white_check_mark:");

            var replyMsg = await _service.ConstructAsync(Context, msg);

            msg = await ReplyAsync(replyMsg.Item2);

            if (replyMsg.Item2 == "Time elapsed. Please type the command again.")
                return;

            await msg.AddReactionsAsync(replyMsg.Item1);
        }
    }
}
