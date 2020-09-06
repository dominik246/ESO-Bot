using BotRepository.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotRepository
{
    public class DataAccess : IDataAccess
    {
        private ServiceDbContext _db;
        public DataAccess(ServiceDbContext db)
        {
            _db = db;
        }

        public async Task FindAsync()
        {

        }

        public async Task GetAsync(int? id)
        {

        }

        public async Task CreateAsync()
        {

        }

        public async Task DeleteAsync()
        {

        }

        public async Task UpdateAsync()
        {

        }

        public async Task CreateTable(GuildTableModel model)
        {
            string sql = $"create table {model.Channel}";
            await _db.Database.ExecuteSqlRawAsync(sql);
            _db.TableName = model.Channel.ToString();
            _db.Model.
        }
    }
}
