using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot.Services
{
    public class ReactMessageService
    {
        public async Task<(IEmote[], string)> ConstructAsync(SocketCommandContext context, IUserMessage message)
        {
            Emoji emote = new Emoji("✅");
            await message.AddReactionAsync(emote);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var reactionCount = 0;
            IList<IEmote> emotes = new List<IEmote>();
            do
            {
                if (stopwatch.ElapsedMilliseconds > 60 * 1000)
                {
                    stopwatch.Stop();
                    return (emotes.ToArray(), "Time elapsed. Please type the command again.");
                }
                await Task.Delay(1000);
                reactionCount = (await message.GetReactionUsersAsync(emote, 10).FirstAsync()).Count(u => !u.IsBot);
            }
            while (reactionCount == 0);

            var newMessage = await context.Channel.GetMessageAsync(message.Id);

            StringBuilder replyMsg = new StringBuilder($"React to give yourself a role.{Environment.NewLine}");

            foreach (var reaction in newMessage.Reactions)
            {
                if (reaction.Key.Name == "✅")
                {
                    continue;
                }
                replyMsg.Append(reaction.Key).Append(" **:** ").Append(reaction.Key.Name).Append(Environment.NewLine);
                emotes.Add(reaction.Key);
            }
            return (emotes.ToArray(), replyMsg.ToString());
        }
    }
}
