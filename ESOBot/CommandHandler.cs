using Discord;
using Discord.Commands;
using Discord.WebSocket;

using ESOBot.Commands;
using ESOBot.Services;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _service;
        private readonly LogService _logger;
        public SocketUserMessage Message { get; private set; }
        public IResult Result { get; private set; }

        public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider service, LogService logger)
        {
            _client = client;
            _commands = commands;
            _service = service;
            _logger = logger;

            _commands.CommandExecuted += OnCommandExecutedAsync;
        }

        private async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            string commandName = command.IsSpecified ? command.Value.Name : "A command";

            await _logger.Log(new LogMessage(LogSeverity.Info, "CommandExc", $"'{commandName}' was executed " +
                $"at '{DateTime.Now}' by '{context.Message.Author.Username}#{context.Message.Author.Discriminator}' " +
                $"in guild '{context.Guild?.Name ?? "DM channel"}'."));
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _service);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            Message = arg as SocketUserMessage;
            if (Message == null)
                return;

            int argPos = 0;

            if (!(Message.HasStringPrefix(";;", ref argPos) ||
                Message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                Message.Author.IsBot)
            {
                return;
            }

            var context = new SocketCommandContext(_client, Message);

            Result = await _commands.ExecuteAsync(context, argPos, _service);
        }
    }
}
