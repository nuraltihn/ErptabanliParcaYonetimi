using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Data.Interfaces
{
    public interface ILogRepository
    {
        Task<List<Log>> GetAllAsync();
        Task<List<Log>> GetByDateRangeAsync(
            DateTime baslangicTarihi,
            DateTime bitisTarihi);
        Task AddAsync(Log log);
    }
}