using System;
using System.Collections.Generic;
using System.Text;
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
        public void AddRaf(Raflar raf)
        {
            _rafRepository.Add(raf);
        }

        public List<Raflar> GetAll ()
        {
            return _rafRepository.GetAll();
        }

        public Raflar? GetById(int id)
        {
            return _rafRepository.GetById(id);
        }

        public Raflar? GetByKod(string rafkodu)
        {
            return _rafRepository.GetByKod(rafkodu);
        }

        public void RemoveRaf(Raflar raf)
        {
            _rafRepository.Delete(raf);
        }

        public void UpdateRaf(Raflar raf)
        {
            _rafRepository.Update(raf);
        }
    }
}
