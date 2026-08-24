using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ISiparisService
    {
        Task <List<Siparis>> GetAllAsync ();
        Task <Siparis?> GetByIdAsync (int id);
        Task<Siparis?> GetByNoAsync (string siparisNo);

        Task AddSiparisAsync(Siparis siparis);
        Task UpdateSiparisAsync (Siparis siparis);
        Task  RemoveSiparisAsync (Siparis siparis);
    }
}
