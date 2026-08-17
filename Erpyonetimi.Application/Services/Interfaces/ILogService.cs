using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ILogService
    {
        List<Log> GetAll();
        void Add(Log log);
    }
}