using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface ISiparisRepository
    {
        Task<List<Siparis>> GetAllAsync();
        Task<Siparis?> GetByIdAsync(int id);
        Task<Siparis?> GetByNoAsync(string siparisNo);

        Task AddAsync(Siparis siparis);
        Task UpdateAsync(Siparis siparis);
        Task DeleteAsync(Siparis siparis);
    }
}
