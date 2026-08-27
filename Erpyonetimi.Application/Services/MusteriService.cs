using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Application.Common;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
namespace Erpyonetimi.Application.Services
{
    public class MusteriService  : IMusteriService
    {
        private readonly IMusteriRepository _musteriRepository;
        public MusteriService(IMusteriRepository musteriRepository)
        {
            _musteriRepository = musteriRepository;
        }

        public async Task<List<Musteri>> GetAllAsync()
        {
            return await  _musteriRepository.GetAllAsync ();
        }

        public async Task  AddMusteriAsync (Musteri musteri)
        {
            await _musteriRepository.AddAsync (musteri);
        }

        public async Task<ServiceResult> DeleteMusteriAsync(Musteri musteri)
        {
            var dbMusteri = await _musteriRepository.GetByIdWithIliskilerAsync (musteri.Id);
            if (dbMusteri == null)
            {
                return ServiceResult.Basarisiz("Müşteri bulunamadı.");
            }
            if(dbMusteri.Siparisler.Any())
            {
                return ServiceResult.Basarisiz(
                    "Bu müşteri silinemez. Müşteriye bağlı siparişler bulunmaktadır");
            }
            await _musteriRepository.DeleteAsync(dbMusteri);
            return ServiceResult.Basarili_("Müşteri başarıyla silindi");
        }

        public async Task <Musteri?> GetByIdAsync (int id)
        {
            return await  _musteriRepository.GetByIdAsync (id);
        }

        public async Task <Musteri?> GetByKodAsync (string musteriKodu)
        {
            return await  _musteriRepository.GetByKodAsync (musteriKodu);
        }

        public async Task UpdateMusteriAsync (Musteri musteri)
        {
            await _musteriRepository.UpdateAsync (musteri);
        }
    }
}