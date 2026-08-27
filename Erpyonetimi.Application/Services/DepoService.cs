
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Erpyonetimi.Application.Common;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services
{
    public class DepoService : IDepoService
    {
        private readonly IDepoRepository _depoRepository;
        public DepoService(IDepoRepository depoRepository)
        {
            _depoRepository = depoRepository;
        }

        public async Task AddDepoAsync (Depolar depo)
        {
           await  _depoRepository.AddAsync(depo);
        }

       public async Task<ServiceResult> DeleteDepoAsync(Depolar depo)
        {
            var dbDepo = await _depoRepository.GetByIdWithIliskilerAsync(depo.Id);
            if(dbDepo == null)
            {
                return ServiceResult.Basarisiz("Depo bulunamadı.");
            }
            var parcalar = dbDepo.Raflar
                .SelectMany(r => r.Parcalar);

            if (parcalar.Any(p => p.SiparisDetaylari.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu depo silinemez.Depoya bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }
            if (parcalar.Any(p => p.StokHareketleri.Any()))
            {
                return ServiceResult.Basarisiz(
                    "Bu depo silinemez. Depoya bağlı işlem görmüş parçalar bulunmaktadır.");
            }

            if (parcalar.Any())
            {
                return ServiceResult.Basarisiz(
                    "Bu depo silinemez. Depoya bağlı parçalar bulunmaktadır.");
            }

            await _depoRepository.DeleteAsync(dbDepo);

            return ServiceResult.Basarili_("Depo başarıyla silindi.");

        }

        public async Task <List<Depolar>> GetAllAsync ()
        {
           return await  _depoRepository.GetAllAsync ();
        }

        public async Task <Depolar?> GetByDepoadiAsync (string depoadi)
        {
            return await  _depoRepository.GetByDepoadiAsync (depoadi);
        }

        public async Task<Depolar?> GetByIdAsync (int id)
        {
            return await  _depoRepository.GetByIdAsync (id);
        }

        public async Task UpdateDepoAsync (Depolar depo)
        {
           await _depoRepository.UpdateAsync (depo);
        }
    }
}
