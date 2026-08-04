using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services
{
    public class TedarikciService
    {
        private readonly ITedarikciRepository _tedarikciRepository;
        public TedarikciService(ITedarikciRepository tedarikciRepository)
        {
            _tedarikciRepository = tedarikciRepository;
        }

        public List<Tedarikci> GetAllTedarikci()
        {
            return _tedarikciRepository.TedarikciGetAll();
        }

        public void AddTedarikci(Tedarikci tedarikci)
        {
            _tedarikciRepository.Add(tedarikci);
        }

        public void UpdateTedarikci(Tedarikci tedarikci)
        {
            _tedarikciRepository.Update(tedarikci);
        }
        public void DeleteTedarikci(Tedarikci tedarikci)
        {

        }
    }
}
