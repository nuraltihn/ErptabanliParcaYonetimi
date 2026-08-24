using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
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

        public async Task  RemoveSiparisAsync(Siparis siparis)
        {
            await _siparisRepository.DeleteAsync(siparis);
        }

        public async Task  UpdateSiparisAsync (Siparis siparis)
        {
            await _siparisRepository.UpdateAsync(siparis);
        }
    }
}
