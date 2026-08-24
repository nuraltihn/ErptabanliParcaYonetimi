using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services
{
    public class RafService : IRafService
    {
        private readonly IRafRepository _rafRepository;
        public RafService(IRafRepository rafRepository)
        {
            _rafRepository = rafRepository;
        }
        public async Task  AddRafAsync (Raflar raf)
        {
          await   _rafRepository.AddAsync(raf);
        }

        public async Task <List<Raflar>> GetAllAsync()
        {
            return await _rafRepository.GetAllAsync();
        }

        public async Task <Raflar?> GetByIdAsync(int id)
        {
            return await _rafRepository.GetByIdAsync (id);
        }

        public async Task <Raflar?> GetByKodAsync (string rafkodu)
        {
            return await  _rafRepository.GetByKodAsync (rafkodu);
        }

        public async Task  RemoveRafAsync (Raflar raf)
        {
             await _rafRepository.DeleteAsync (raf);
        }

        public async Task UpdateRafAsync (Raflar raf)
        {
           await  _rafRepository.UpdateAsync (raf);
        }
    }
}
