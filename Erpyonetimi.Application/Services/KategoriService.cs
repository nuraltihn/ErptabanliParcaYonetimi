using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Erpyonetimi.Application.Common;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services
{
    public class KategoriService : IKategoriService
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriService(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public async Task <List<Kategori>> GetAllKategoriAsync()
        {
            return await _kategoriRepository.GetAllAsync();
        }
        public async Task  AddKategoriAsync(Kategori kategori)
        {
            await _kategoriRepository.AddAsync (kategori);
        }

        public async Task<ServiceResult> UpdateKategoriAsync(Kategori kategori)
        {
            var mevcutKategori = await _kategoriRepository.GetByIdAsync(kategori.Id);

            if (mevcutKategori == null)
            {
                return ServiceResult.Basarisiz("Kategori bulunamadı.");
            }

            await _kategoriRepository.UpdateAsync(kategori);

            return ServiceResult.Basarili_("Kategori başarıyla güncellendi.");
        }
        public async Task<ServiceResult> DeleteKategoriAsync(int id)
        {
            var kategori = await _kategoriRepository.GetByIdWithParcalarAsync(id);

            if (kategori == null)
            {
                return ServiceResult.Basarisiz("Kategori bulunamadı.");
            }

            if (kategori.Parcalar.Any(p => p.StokHareketleri.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu kategori silinemez. Kategoriye bağlı işlem görmüş parçalar bulunmaktadır.");
            }

            if (kategori.Parcalar.Any(p => p.SiparisDetaylari.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu kategori silinemez. Kategoriye bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }

            await _kategoriRepository.DeleteAsync(kategori);

            return ServiceResult.Basarili_("Kategori başarıyla silindi.");
        }
    }
}
