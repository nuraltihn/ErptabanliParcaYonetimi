using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public List<Log> GetAll()
        {
            return _logRepository.GetAll();
        }
        public void Add(Log log)
        {
            _logRepository.Add(log);
        }

    }
}
