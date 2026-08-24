using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
namespace Erpyonetimi.Application.Services
{
    public class MusteriService  : IMusteriService
    {
        private readonly IMusteriRepository _musteriRepository;
        public MusteriService(IMusteriRepository musteriRepository)
        {
            _musteriRepository = musteriRepository;
        }

        public async Task<List<Musteri>> GetAllAsync()
        {
            return await  _musteriRepository.GetAllAsync ();
        }

        public async Task  AddMusteriAsync (Musteri musteri)
        {
            await _musteriRepository.AddAsync (musteri);
        }

        public async Task DeleteMusteriAsync (Musteri musteri)
        {
             await _musteriRepository.DeleteAsync (musteri);
        }

        public async Task <Musteri?> GetByIdAsync (int id)
        {
            return await  _musteriRepository.GetByIdAsync (id);
        }

        public async Task <Musteri?> GetByKodAsync (string musteriKodu)
        {
            return await  _musteriRepository.GetByKodAsync (musteriKodu);
        }

        public async Task UpdateMusteriAsync (Musteri musteri)
        {
            await _musteriRepository.UpdateAsync (musteri);
        }
    }
}