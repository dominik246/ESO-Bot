using Discord;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESOBot.Services
{
    public class LogService
    {
        public Task Log(LogMessage log)
        {
            Console.WriteLine(log.Message);
            return Task.CompletedTask;
        }
    }
}
