using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class DepoRepository : IDepoRepository
    {
        private readonly ErpDbContext _context;
        public DepoRepository(ErpDbContext context)
        {
            _context = context;
        }

        public void Add(Depolar depo)
        {
            _context.Depolar.Add(depo);
            _context.SaveChanges();
        }

        public void Delete(Depolar depo)
        {
            var dbDepo = _context.Depolar
                .Include(d => d.Raflar)
                .ThenInclude(r => r.Parcalar)
                .ThenInclude(p => p.StokHareketleri)
                .Include(d => d.Raflar)
                .ThenInclude(r => r.Parcalar)
                .ThenInclude(p => p.SiparisDetaylari)
                .FirstOrDefault(d => d.Id == depo.Id);

            if (dbDepo == null)
            {
                throw new Exception("Depo bulunamadı.");
            }

            if (dbDepo.Raflar
                .SelectMany(r => r.Parcalar)
                .Any(p => p.SiparisDetaylari.Any()))
            {
                throw new Exception(
                    "Bu depo silinemez. Depoya bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }

            if (dbDepo.Raflar
                .SelectMany(r => r.Parcalar)
                .Any(p => p.StokHareketleri.Any()))
            {
                throw new Exception(
                    "Bu depo silinemez. Depoya bağlı işlem görmüş parçalar bulunmaktadır.");
            }

            if (dbDepo.Raflar
                .SelectMany(r => r.Parcalar)
                .Any())
            {
                throw new Exception(
                    "Bu depo silinemez. Depoya bağlı parçalar bulunmaktadır.");
            }

            _context.Depolar.Remove(dbDepo);
            _context.SaveChanges();
        }

        public List<Depolar> GetAll()
        { if(!DatabaseHelper.IsConnected)
                return new List<Depolar>();


            return _context.Depolar.ToList();
           
        }

        public Depolar? GetByDepoadi(string depoadi)
        {
            return _context.Depolar.FirstOrDefault(x=>x.Depaadi==depoadi);
        }

        public Depolar? GetById(int id)
        {
            return _context.Depolar.FirstOrDefault(d => d.Id == id);
        }

        public void Update(Depolar depo)
        {
            var guncelleme =_context.Depolar.FirstOrDefault(x=>x.Id == depo.Id);
            if (guncelleme != null) 
            {
                guncelleme.Depaadi = depo.Depaadi;
                guncelleme.Konum = depo.Konum;
                _context.SaveChanges();
            }
        }
    }
}
