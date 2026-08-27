using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services
{
    public class RaporService :IRaporService
    {
        private readonly IRaporRepository _raporRepository;
        public RaporService(IRaporRepository raporRepository)
        {
            _raporRepository = raporRepository;
        }

        public async Task<List<Parca>> GetStokDurumuAsync()
        {
            return await _raporRepository.GetStokDurumAsync();
        }

        public async Task<List<Parca>> GetKritikStokAsync()
        {
            return await _raporRepository.GetKritikStokAsync();
        }

        public async Task<List<StokHareket>> GetStokHareketleriAsync()
        {
            return await _raporRepository.GetStokHareketleriAsync();
        }

        public async Task<List<Siparis>> GetSiparislerAsync()
        {
            return await _raporRepository.GetSiparisAsync();
        }
    }
}
