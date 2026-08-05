using System;
using System.Collections.Generic;
using System.Text;
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

        public void AddDepo(Depolar depo)
        {
            _depoRepository.Add(depo);
        }

        public void DeleteDepo(Depolar depo)
        {
            _depoRepository.Delete(depo);
        }

        public List<Depolar> GetAll ()
        {
           return  _depoRepository.GetAll();
        }

        public Depolar? GetById(int id)
        {
            return _depoRepository.GetById(id);
        }

        public void UpdateDepo(Depolar depo)
        {
            _depoRepository.Update(depo);
        }
    }
}
