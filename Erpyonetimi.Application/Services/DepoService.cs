
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task  DeleteDepoAsync (Depolar depo)
        {
            await _depoRepository.DeleteAsync (depo);
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
