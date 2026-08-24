using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ILogService
    {
       Task <List<Log>> GetAllAsync  ();
       Task  AddAsync (Log log);
    }
}