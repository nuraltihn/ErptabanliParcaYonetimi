using System.Collections.Generic;
using System.Threading.Tasks;
using Erpyonetimi.Application.Services.Interfaces;
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
        public async Task DeleteTedarikciAsync(int id)
        {
            var tedarikci = await _tedarikciRepository.GetByIdAsync(id);

            if (tedarikci != null)
            {
                await _tedarikciRepository.DeleteAsync(tedarikci);
            }
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