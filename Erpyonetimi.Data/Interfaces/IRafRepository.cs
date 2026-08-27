using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IRafRepository
    {
        Task<List<Raflar>> GetAllAsync();
        Task<Raflar?> GetByIdAsync(int id);
        Task<Raflar?> GetByKodAsync(string rafkodu);

        Task<Raflar?> GetByIdWithParcalarAsync(int id);

        Task AddAsync(Raflar raf);
        Task UpdateAsync(Raflar raf);
        Task DeleteAsync(Raflar raf);
    }
}
