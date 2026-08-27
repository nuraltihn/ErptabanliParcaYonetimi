using System.Collections.Generic;
using System.Threading.Tasks;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Application.Common;
using System.Linq;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services
{
    public class TedarikciService : ITedarikciService
    {
        private readonly ITedarikciRepository _tedarikciRepository;
        public TedarikciService(ITedarikciRepository tedarikciRepository)
        {
            _tedarikciRepository = tedarikciRepository;
        }
        public async Task<List<Tedarikci>> GetAllTedarikciAsync()
        {
            return await _tedarikciRepository.GetAllAsync();
        }
        public async Task AddTedarikciAsync(Tedarikci tedarikci)
        {
            await _tedarikciRepository.AddAsync(tedarikci);
        }
        public async Task<Tedarikci?> GetByIdAsync(int id)
        {
            return await _tedarikciRepository.GetByIdAsync(id);
        }
        public async Task<ServiceResult> DeleteTedarikciAsync(int id)
        {
            var tedarikci = await _tedarikciRepository.GetByIdWithIliskilerAsync(id);

            if (tedarikci == null)
            {
                return ServiceResult.Basarisiz("Tedarikçi bulunamadı.");
            }

            if (tedarikci.Parcalar.Any(p => p.StokHareketleri.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu tedarikçi silinemez. Tedarikçiye bağlı işlem görmüş parçalar bulunmaktadır.");
            }

            if (tedarikci.Parcalar.Any(p => p.SiparisDetaylari.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu tedarikçi silinemez. Tedarikçiye bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }

            await _tedarikciRepository.DeleteAsync(tedarikci);

            return ServiceResult.Basarili_("Tedarikçi başarıyla silindi.");
        }
        public async Task UpdateTedarikciAsync(Tedarikci tedarikci)
        {
            await _tedarikciRepository.UpdateAsync(tedarikci);
        }
        public async Task<Tedarikci?> GetByKodAsync(string kod)
        {
            return await _tedarikciRepository.GetByKodAsync(kod);
        }
    }
}