using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
using Erpyonetimi.Application.Common;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IParcaService
    {
        Task <List<Parca>> GetAllParcaAsync();
        Task  <Parca?> GetByIdAsync (int id);
        Task  <Parca?> GetByKodAsync (string parcakodu);
        Task <ServiceResult> AddParcaAsync (Parca parca);
        Task  UpdateParcaAsync (Parca parca);
        Task  <ServiceResult> RemoveParcaAsync (Parca parca);

    }
}
