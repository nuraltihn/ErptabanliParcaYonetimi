using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Data.Interfaces
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAllAsync();
        Task AddAsync(Log log);
    }
}