using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using System.Threading.Tasks;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services
{
    public class SiparisDetayService : ISiparisDetayService
    {

        private readonly ISiparisDetayRepository _siparisDetayRepository;
        public SiparisDetayService( ISiparisDetayRepository siparisDetayRepository)
        {
            _siparisDetayRepository = siparisDetayRepository;
        }
        public async Task AddDetayAsync (SiparisDetaylari detay)
        {
            await _siparisDetayRepository.AddAsync(detay);
        }

        public async Task DeleteDetayAsync (SiparisDetaylari detay)
        {
          await  _siparisDetayRepository.DeleteAsync (detay);
        }

        public async Task<List<SiparisDetaylari>> GetAllAsync  ()
        {
            return await  _siparisDetayRepository.GetAllAsync ();
        }

        public async Task <SiparisDetaylari?> GetByIdAsync (int id)
        {
            return await _siparisDetayRepository.GetByIdAsync (id);
        }

        public async Task UpdateDetayAsync (SiparisDetaylari detay)
        {
           await  _siparisDetayRepository.UpdateAsync (detay);
        }
    }
}
