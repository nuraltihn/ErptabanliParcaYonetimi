using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public List<Kategori> GetAllKategori()
        {
            return _kategoriRepository.GetAll();
        }
        public void AddKategori(Kategori kategori)
        {
            _kategoriRepository.Add(kategori);
        }
        public void UpdateKategori(Kategori kategori)
        {
            _kategoriRepository.Update(kategori);
        }
        public void DeleteKategori(int id)
        {
            var kategori = _kategoriRepository.GetById(id);
            if (kategori != null)
            {
                _kategoriRepository.Delete(kategori);
            }
        }
    }
}
