using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Erpyonetimi.Data.Repositories
{
    public class SiparisDetayRepository : ISiparisDetayRepository
    {
        private readonly ErpDbContext _context;
        public SiparisDetayRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(SiparisDetaylari detay)
        {
            _context.SiparisDetaylari.Add(detay);
            _context.SaveChanges();
        }

        public void Delete(SiparisDetaylari detay)
        {
            var mevcut = _context.SiparisDetaylari.FirstOrDefault(x => x.Id == detay.Id);
            if (mevcut != null)
            {
                _context.SiparisDetaylari.Remove(mevcut);
                _context.SaveChanges();
            }
                
        }

        public List<SiparisDetaylari> GetAll()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<SiparisDetaylari>();
                return _context.SiparisDetaylari
                .Include(x => x.Siparis)
                .Include(x => x.Parca)
                .AsNoTracking()
                .ToList();
          
        }

        public SiparisDetaylari? GetById(int id)
        {
            return _context.SiparisDetaylari
                .Include(x => x.Parca)
                .Include(x => x.Siparis)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(SiparisDetaylari detay)
        {
            var mevcut = _context.SiparisDetaylari.Find(detay.Id);
            if (mevcut != null)
            {
                mevcut.SiparisId = detay.SiparisId;
                mevcut.ParcaId = detay.ParcaId;
                mevcut.Miktar = detay.Miktar;
                mevcut.BirimFiyat = detay.BirimFiyat;
                mevcut.ToplamFiyat = detay.ToplamFiyat;
                _context.SaveChanges();
            }
        }
    }
}
