using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services
{
    public class StokHareketService : IStokHareketService
    {
        private readonly IStokHareketRepository _stokHareketRepository;
        public StokHareketService(IStokHareketRepository stokHareketRepository)
        {
            _stokHareketRepository = stokHareketRepository;
        }
        public void AddStokHareket(StokHareket stokHareket)
        {
            _stokHareketRepository.Add(stokHareket);
        }

        public List<StokHareket> GetAll ()
        {
           return _stokHareketRepository.GetAll();
        }

        public StokHareket? GetById(int id)
        {
            return _stokHareketRepository.GetById(id);
        }

        public void RemoveStokHareket(StokHareket stokHareket)
        {
            _stokHareketRepository.Delete(stokHareket);
        }

        public void UpdateStokHareket(StokHareket stokHareket)
        {
            _stokHareketRepository.Update(stokHareket);
        }
    }
}
