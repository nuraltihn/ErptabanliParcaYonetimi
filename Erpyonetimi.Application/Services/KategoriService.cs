using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services
{
    public class KategoriService : IKategoriService
    {
        private readonly IKategoriRepository _kategoriRepository;
        public KategoriService(IKategoriRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }

        public async Task <List<Kategori>> GetAllKategoriAsync()
        {
            return await _kategoriRepository.GetAllAsync();
        }
        public async Task  AddKategoriAsync(Kategori kategori)
        {
          await  _kategoriRepository.AddAsync (kategori);
        }
        public async Task UpdateKategoriAsync (Kategori kategori)
        {
          await   _kategoriRepository.UpdateAsync (kategori);
        }
        public async Task  DeleteKategoriAsync (int id)
        {
            var kategori = await _kategoriRepository.GetByIdAsync(id);

            if (kategori != null)
            {
              await  _kategoriRepository.DeleteAsync(kategori);
            }
        }
    }
}
