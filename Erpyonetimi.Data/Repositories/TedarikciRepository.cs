using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Erpyonetimi.Data.Repositories
{
    public class TedarikciRepository : ITedarikciRepository
    {
        private readonly ErpDbContext _context;

        public TedarikciRepository(ErpDbContext context)
        {
            _context = context;
        }

        public List<Tedarikci> GetAll()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Tedarikci>();
                return _context.Tedarikciler
                .AsNoTracking()
                .ToList();
           
        }

        public void Add(Tedarikci tedarikci)
        {
            _context.Tedarikciler.Add(tedarikci);
            _context.SaveChanges();
        }

        public void Update(Tedarikci tedarikci)
        {
            var mevcut = _context.Tedarikciler.FirstOrDefault(x => x.Id == tedarikci.Id);

            if (mevcut != null)
            {
                mevcut.TedarikciKodu = tedarikci.TedarikciKodu;
                mevcut.FirmaAdi = tedarikci.FirmaAdi;
                mevcut.YetkiliKisi = tedarikci.YetkiliKisi;
                mevcut.Tel = tedarikci.Tel;
                mevcut.Email = tedarikci.Email;
                mevcut.Adres = tedarikci.Adres;
                mevcut.Fax = tedarikci.Fax;
                mevcut.VergiNo = tedarikci.VergiNo;

                _context.SaveChanges();
            }
        }
        public void Delete(Tedarikci tedarikci)
        {
            var dbTedarikci = _context.Tedarikciler
                .Include(t => t.Parcalar)
                .ThenInclude(p=> p.StokHareketleri)
                .Include(t=>t.Parcalar)
                .ThenInclude(p =>p.SiparisDetaylari)
               .FirstOrDefault(t => t.Id == tedarikci.Id);

            if(dbTedarikci == null)
            {
                throw new Exception("Tedarikci bulunamadı.");
            }

            if(dbTedarikci.Parcalar.Any(p => p.StokHareketleri.Any()))
            {
                throw new Exception ("Bu tedarikçi silinemez. Tedarikçiye bağlı işlem görmüş parçalar bulunmaktadır.");
            }

            if (dbTedarikci.Parcalar.Any(p => p.SiparisDetaylari.Any()))
            {
                throw new Exception(
                    "Bu tedarikçi silinemez. Tedarikçiye bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }
            _context.Tedarikciler.Remove(tedarikci);

            _context.SaveChanges();
        }

        public Tedarikci? GetById(int id)
        {
            return _context.Tedarikciler.FirstOrDefault(x => x.Id == id);
        }

        public Tedarikci? GetByKod(string kod)
        {
            return _context.Tedarikciler.FirstOrDefault(x =>
            x.TedarikciKodu == kod);
        }
    }
}