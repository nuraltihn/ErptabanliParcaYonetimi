using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IKategoriRepository
    {Task<List<Kategori>> GetAllAsync ();
        Task<Kategori?> GetByIdAsync(int id);
        Task AddAsync (Kategori kategori);
        Task UpdateAsync (Kategori kategori);
        Task DeleteAsync (Kategori kategori);

    }
}
