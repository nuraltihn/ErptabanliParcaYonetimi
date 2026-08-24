using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface ISiparisDetayRepository
    {
        Task<List<SiparisDetaylari>> GetAllAsync();
        Task<SiparisDetaylari?> GetByIdAsync(int id);

        Task AddAsync(SiparisDetaylari detay);
        Task UpdateAsync(SiparisDetaylari detay);
        Task DeleteAsync(SiparisDetaylari detay);
    }
}
