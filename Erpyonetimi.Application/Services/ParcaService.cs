using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;

namespace Erpyonetimi.Application.Services
{
    public class ParcaService : IParcaService
    {
        private readonly IParcaRepository _parcaRepository;

        public ParcaService(IParcaRepository parcaRepository)
        {
            _parcaRepository = parcaRepository;
        }

        public async Task AddParcaAsync(Parca parca)
        {
            var parcalar = await _parcaRepository.GetAllAsync();

            var mevcutParca = parcalar
                .FirstOrDefault(x => x.ParcaKodu == parca.ParcaKodu);

            if (mevcutParca != null)
            {
                throw new InvalidOperationException(
                    "Bu parça kodu zaten mevcut.");
            }

            await _parcaRepository.AddAsync(parca);
        }

        public async Task<List<Parca>> GetAllParcaAsync()
        {
            return await _parcaRepository.GetAllAsync();
        }

        public async Task<Parca?> GetByIdAsync(int id)
        {
            return await _parcaRepository.GetByIdAsync(id);
        }

        public async Task<Parca?> GetByKodAsync(string parcakodu)
        {
            return await _parcaRepository.GetByKodAsync(parcakodu);
        }

        public async Task RemoveParcaAsync(Parca parca)
        {
            await _parcaRepository.DeleteAsync(parca);
        }

        public async Task UpdateParcaAsync(Parca parca)
        {
            await _parcaRepository.UpdateAsync(parca);
        }
    }
}