using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IParcaRepository
    {
        Task<List<Parca>> GetAllAsync();
        Task<Parca?> GetByIdAsync(int id);
        Task<Parca?> GetByKodAsync(string parcaKodu);
        Task AddAsync(Parca parca);
        Task UpdateAsync(Parca parca);
        Task DeleteAsync(Parca parca);

    }
}
