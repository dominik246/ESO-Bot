using System.Threading.Tasks;

namespace BotRepository
{
    public interface IDataAccess
    {
        Task CreateAsync();
        Task DeleteAsync();
        Task FindAsync();
        Task GetAsync(int? id);
        Task UpdateAsync();
        Task CreateTable();
    }
}