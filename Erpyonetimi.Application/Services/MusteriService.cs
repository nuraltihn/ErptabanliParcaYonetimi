using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services
{
    public class MusteriService : IMusteriService
    {
        private readonly IMusteriRepository _musteriRepository;
        public MusteriService(IMusteriRepository musteriRepository)
        {
            _musteriRepository = musteriRepository;
        }
        public void AddMusteri(Musteri musteri)
        {
            _musteriRepository.Add(musteri);
        }

        public void DeleteMusteri(Musteri musteri)
        {
            _musteriRepository.Delete(musteri);
        }

        public List<Musteri> GetAllMusteri()
        {
            return _musteriRepository.GetAll();
        }

        public Musteri? GetById(int id)
        {
            return _musteriRepository.GetById(id);
        }

        public Musteri? GetByKod(string musteriKodu)
        {
            return _musteriRepository.GetByKod(musteriKodu);
        }

        public void UpdateMusteri(Musteri musteri)
        {
            _musteriRepository.Update(musteri);
        }
    }
}
