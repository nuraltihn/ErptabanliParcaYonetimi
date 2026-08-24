using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services
{
    public class StokHareketService : IStokHareketService
    {
        private readonly IStokHareketRepository _stokHareketRepository;
        public StokHareketService(IStokHareketRepository stokHareketRepository)
        {
            _stokHareketRepository = stokHareketRepository;
        }
        public async Task AddStokHareketAsync (StokHareket stokHareket)
        {
            await _stokHareketRepository.AddAsync (stokHareket);
        }

        public async Task <List<StokHareket>> GetAllAsync ()
        {
           return await _stokHareketRepository.GetAllAsync ();
        }

        public async Task <StokHareket?> GetByIdAsync(int id)
        {
            return await _stokHareketRepository.GetByIdAsync (id);
        }

        public async Task RemoveStokHareketAsync (StokHareket stokHareket)
        {
           await _stokHareketRepository.DeleteAsync (stokHareket);
        }

        public async Task  UpdateStokHareketAsync (StokHareket stokHareket)
        {
           await _stokHareketRepository.UpdateAsync (stokHareket);
        }
    }
}
