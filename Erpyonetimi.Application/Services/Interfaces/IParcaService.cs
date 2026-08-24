using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IParcaService
    {
        Task <List<Parca>> GetAllParcaAsync();
        Task  <Parca?> GetByIdAsync (int id);
        Task  <Parca?> GetByKodAsync (string parcakodu);
        Task   AddParcaAsync (Parca parca);
        Task  UpdateParcaAsync (Parca parca);
        Task  RemoveParcaAsync (Parca parca);

    }
}
