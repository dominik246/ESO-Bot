using BotRepository.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace BotRepository
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<GuildTableModel> GuildTables { get; set; }
        public string TableName { get; set; } = DateTime.Now.Ticks.ToString();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GuildTableModel>(entity => {
                entity.ToTable(TableName);
            });
        }
    }
}
