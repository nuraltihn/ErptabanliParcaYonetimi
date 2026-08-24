using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IStokHareketService
    {
        Task <List<StokHareket>> GetAllAsync ();
        Task <StokHareket?> GetByIdAsync (int id);

        Task AddStokHareketAsync (StokHareket stokHareket);
        Task UpdateStokHareketAsync (StokHareket stokHareket);
        Task RemoveStokHareketAsync (StokHareket stokHareket);
    }
}
