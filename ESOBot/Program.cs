﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;

using ESOBot.Events;
using ESOBot.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

namespace ESOBot
{
    class Program
    {
        private DiscordSocketClient _client;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                ExclusiveBulkDelete = true,
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 10
            });
            var service = BuildServiceProvider();

            _client.Log += service.GetService<LogService>().Log;

            try
            {
                await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
                await _client.StartAsync();

                await service.GetRequiredService<CommandHandler>().InstallCommandsAsync();
                _client.UserJoined += service.GetRequiredService<UserJoinedEvent>().OnUserJoin;
                _client.ReactionAdded += service.GetRequiredService<UserReactedEvent>().OnReaction;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await Task.Delay(-1);
            }
        }

        private IServiceProvider BuildServiceProvider()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>()
            .AddSingleton<LogService>()
            .AddSingleton<EmbedService>()
            .AddSingleton<UserJoinedEvent>()
            .AddSingleton<UserReactedEvent>()
            .AddSingleton<AddRole>()
            .AddSingleton<ReactMessageService>()

            //.AddScoped<IDataAccess, DataAccess>()

            .BuildServiceProvider();
    }
}