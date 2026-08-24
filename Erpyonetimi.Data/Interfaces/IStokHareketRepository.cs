using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IStokHareketRepository
    {
        Task<List<StokHareket>> GetAllAsync();
        Task<StokHareket?> GetByIdAsync(int id);

        Task AddAsync(StokHareket stokHareket);
        Task UpdateAsync(StokHareket stokHareket);
        Task DeleteAsync(StokHareket stokHareket);
    }
}
