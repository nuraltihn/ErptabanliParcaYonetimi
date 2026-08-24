using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IMusteriService
    {
        Task<List<Musteri>> GetAllAsync();
        Task<Musteri?> GetByIdAsync (int id);
        Task<Musteri?> GetByKodAsync (string musteriKodu);
         Task AddMusteriAsync(Musteri musteri);
         Task UpdateMusteriAsync (Musteri musteri);
         Task DeleteMusteriAsync (Musteri musteri);
    }
}
