using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
namespace Erpyonetimi.Application.Services
{
    public class ParcaService : IParcaService
    {
        private readonly IParcaRepository _parcaRepository;
        public ParcaService(IParcaRepository parcaRepository)
        {
            _parcaRepository = parcaRepository;
        }
        public void AddParca(Parca parca)
        {
            _parcaRepository.Add(parca);
        }

        public List<Parca> GetAllParca()
        {
            return _parcaRepository.GetAll();
        }

        public Parca? GetById(int id)
        {
            return _parcaRepository.GetById(id);
        }

        public Parca? GetByKod(string parcakodu)
        {
            return _parcaRepository.GetByKod(parcakodu);
        }

        public void RemoveParca(Parca parca)
        {
            _parcaRepository.Delete(parca);
        }

        public void UpdateParca(Parca parca)
        {
            _parcaRepository.Update(parca);
        }
    }
}
