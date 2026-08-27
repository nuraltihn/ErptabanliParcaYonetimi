using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IMusteriRepository
    {
        Task<List<Musteri>> GetAllAsync();
        Task<Musteri?> GetByIdAsync(int id);

        Task<Musteri?> GetByIdWithIliskilerAsync(int id);
        Task<Musteri?> GetByKodAsync(string musteriKodu);

        Task AddAsync(Musteri musteri);
        Task UpdateAsync(Musteri musteri);
        Task DeleteAsync(Musteri musteri);
    }
}
