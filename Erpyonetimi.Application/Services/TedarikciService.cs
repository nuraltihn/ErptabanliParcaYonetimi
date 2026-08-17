using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Erpyonetimi.Application.Services
{
    public class TedarikciService : ITedarikciService
    {
        private readonly ITedarikciRepository _tedarikciRepository;

        public TedarikciService(ITedarikciRepository tedarikciRepository)
        {
            _tedarikciRepository = tedarikciRepository;
        }

        public List<Tedarikci> GetAllTedarikci()
        {
            return _tedarikciRepository.GetAll();
        }

        public void AddTedarikci(Tedarikci tedarikci)
        {
            _tedarikciRepository.Add(tedarikci);
        }

        public Tedarikci? GetById(int id)
        {
            return _tedarikciRepository.GetById(id);
        }

        public void DeleteTedarikci(int id)
        {
            var tedarikci = _tedarikciRepository.GetById(id);
            if (tedarikci != null)
            {
                _tedarikciRepository.Delete(tedarikci);
            }
        }

        public void UpdateTedarikci(Tedarikci tedarikci)
        {
            _tedarikciRepository.Update(tedarikci);
        }

        public Tedarikci? GetByKod(string kod)
        {
            return _tedarikciRepository.GetByKod(kod);
        }
    }
}