using System;
using System.Collections.Generic;
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

        public void AddSiparis(Siparis siparis)
        {
            _siparisRepository.Add(siparis);
        }

        public List<Siparis> GetAll ()
        {
            return _siparisRepository.GetAll();
        }

        public Siparis? GetById(int id)
        {
            return _siparisRepository.GetById(id);
        }

        public Siparis? GetByNo(string siparisNo)
        {
            return _siparisRepository.GetByNo(siparisNo);
        }

        public void RemoveSiparis(Siparis siparis)
        {
            _siparisRepository.Delete(siparis);
        }

        public void UpdateSiparis(Siparis siparis)
        {
            _siparisRepository.Update(siparis);
        }
    }
}
