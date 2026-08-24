using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IDepoRepository
    {
        Task<List<Depolar>> GetAllAsync();
        Task<Depolar?> GetByIdAsync(int id);
        Task<Depolar?> GetByDepoadiAsync (string depoadi);
        Task AddAsync(Depolar depo);
        Task UpdateAsync(Depolar depo);
        Task DeleteAsync(Depolar depo);
    }
}
