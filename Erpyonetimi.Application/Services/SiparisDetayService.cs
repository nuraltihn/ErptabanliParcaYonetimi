using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
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
        public void AddDetay(SiparisDetaylari detay)
        {
            _siparisDetayRepository.Add(detay);
        }

        public void DeleteDetay(SiparisDetaylari detay)
        {
            _siparisDetayRepository.Delete(detay);
        }

        public List<SiparisDetaylari> GetAll ()
        {
            return _siparisDetayRepository.GetAll();
        }

        public SiparisDetaylari? GetById(int id)
        {
            return _siparisDetayRepository.GetById(id);
        }

        public void UpdateDetay(SiparisDetaylari detay)
        {
            _siparisDetayRepository.Update(detay);
        }
    }
}
