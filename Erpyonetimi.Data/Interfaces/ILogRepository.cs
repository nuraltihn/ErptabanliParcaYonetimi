using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Data.Interfaces
{
    public interface ILogRepository
    {
        List<Log> GetAll();
        void Add(Log log);
    }
}