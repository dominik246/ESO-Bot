using Discord;
using Discord.Commands;

using ESOBot.Services;

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ESOBot.Commands
{
    public class CreateEmbed : ModuleBase<SocketCommandContext>
    {
        private readonly EmbedService _service;
        private readonly LogService _log;
        public CreateEmbed(EmbedService service, LogService log)
        {
            _service = service;
            _log = log;
        }
        [Command("embed", RunMode = RunMode.Async)]
        public async Task Create([Remainder] string rawJsonInput = "")
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(rawJsonInput);
            MemoryStream stream = new MemoryStream(byteArray);

            try
            {
                JsonDocument json = await JsonDocument.ParseAsync(stream);
                var embed = await _service.BuildAsync(json);
                await ReplyAsync(embed: embed);
                await stream.DisposeAsync();
                await Task.Run(() => json.Dispose());
            }
            catch (Exception ex)
            {
                await _log.Log(new LogMessage(LogSeverity.Error, "input", ex.Message, ex));
            }
        }
    }
}
