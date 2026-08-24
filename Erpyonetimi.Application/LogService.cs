using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public async Task  <List<Log>> GetAllAsync()
        {
            return await _logRepository.GetAllAsync();
        }
        public async Task AddAsync(Log log)
        {
          await  _logRepository.AddAsync(log);
        }
    }
}
