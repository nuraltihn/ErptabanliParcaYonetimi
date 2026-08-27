using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Erpyonetimi.Application.Common;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services
{
    public class SiparisService : ISiparisService
    {
        private readonly ISiparisRepository _siparisRepository;
        public SiparisService(ISiparisRepository siparisRepository)
        {
           _siparisRepository = siparisRepository; 
        }

        public async Task  AddSiparisAsync(Siparis siparis)
        {
            await _siparisRepository.AddAsync(siparis);
        }

        public async Task  <List<Siparis>> GetAllAsync ()
        {
            return await _siparisRepository.GetAllAsync();
        }

        public async Task<Siparis?> GetByIdAsync (int id)
        {
            return await _siparisRepository.GetByIdAsync (id);
        }

        public async Task <Siparis?> GetByNoAsync (string siparisNo)
        {
            return await _siparisRepository.GetByNoAsync(siparisNo);
        }

        public async Task<ServiceResult> RemoveSiparisAsync(Siparis siparis)
        {
            var dbSiparis = await _siparisRepository.GetByIdWithIliskilerAsync(siparis.Id);
            if (dbSiparis == null)
            {
                return ServiceResult.Basarisiz("Siparis bulunamadı.");
            }
            if (dbSiparis.SiparisDetaylari.Any())
            {
                return ServiceResult.Basarisiz(
                    "Bu sipariş silinemez.Siparişe bağlı parçalar bulunmaktadır.");
            }
            await _siparisRepository.DeleteAsync(dbSiparis);
            return ServiceResult.Basarili_("Sipariş başaryıyla silindi.");
        }
        public async Task  UpdateSiparisAsync (Siparis siparis)
        {
            await _siparisRepository.UpdateAsync(siparis);
        }
    }
}
