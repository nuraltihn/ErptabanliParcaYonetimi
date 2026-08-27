using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erpyonetimi.Domain.Entities;
using System.Linq;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Application.Common;
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

        public async Task<ServiceResult> AddParcaAsync(Parca parca)
        {
            var mevcutParca = await _parcaRepository.GetByKodAsync(parca.ParcaKodu);

            if (mevcutParca != null)
            {
                return ServiceResult.Basarisiz(
                    "Bu parça kodu zaten mevcut.");
            }

            await _parcaRepository.AddAsync(parca);

            return ServiceResult.Basarili_("Parça başarıyla eklendi.");
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

        public async Task<ServiceResult> RemoveParcaAsync(Parca parca)
        {
            var dbParca = await _parcaRepository.GetByIdWithIliskilerAsync(parca.Id);

            if (dbParca == null)
            {
                return ServiceResult.Basarisiz("Parça bulunamadı.");
            }

            if (dbParca.StokHareketleri.Any())
            {
                return ServiceResult.Basarisiz(
                    "Bu parça silinemez. Parçaya bağlı stok hareketleri bulunmaktadır.");
            }

            if (dbParca.SiparisDetaylari.Any())
            {
                return ServiceResult.Basarisiz(
                    "Bu parça silinemez. Parçaya bağlı sipariş detayları bulunmaktadır.");
            }

            await _parcaRepository.DeleteAsync(dbParca);

            return ServiceResult.Basarili_("Parça başarıyla silindi.");
        }

        public async Task UpdateParcaAsync(Parca parca)
        {
            await _parcaRepository.UpdateAsync(parca);
        }
    }
}