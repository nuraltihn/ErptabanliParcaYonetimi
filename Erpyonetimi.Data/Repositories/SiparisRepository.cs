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
    public class SiparisRepository : ISiparisRepository
    {
        private readonly ErpDbContext _context;
        public SiparisRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Siparis siparis)
        {
            _context.Siparisler.Add(siparis);
            _context.SaveChanges();
        }

        public void Delete(Siparis siparis)
        {
            var mevcut = _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                .FirstOrDefault(s => s.Id == siparis.Id);

            if (mevcut == null)
            {
                throw new Exception("Sipariş bulunamadı.");
            }

            if (mevcut.SiparisDetaylari.Any())
            {
                throw new Exception(
                    "Bu sipariş silinemez. Siparişe bağlı parçalar bulunmaktadır.");
            }

            if (mevcut.Durum == "Tamamlandı")
            {
                throw new Exception(
                    "Tamamlanmış siparişler silinemez.");
            }

            _context.Siparisler.Remove(mevcut);
            _context.SaveChanges();
        }

        public List<Siparis> GetAll()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Siparis>();
                return _context.Siparisler.Include(s => s.Musteri)
                .Include(s=>s.SiparisDetaylari)
                .ThenInclude(sd=>sd.Parca)
                .AsNoTracking()
                .ToList();
          
        }

        public Siparis? GetById(int id)
        {
            return _context.Siparisler
                .FirstOrDefault(x=>x.Id == id);
        }

        public Siparis? GetByNo(string siparisNo)
        {
            return _context.Siparisler
                .FirstOrDefault(x=>x.SiparisNo == siparisNo);
        }

        public void Update(Siparis siparis)
        {
            var mevcut = _context.Siparisler.Find(siparis.Id);
            if (mevcut !=null)
            {
                mevcut.SiparisNo = siparis.SiparisNo;
                mevcut.MusteriId = siparis.MusteriId;
                mevcut.SiparisTarihi = siparis.SiparisTarihi;
                mevcut.ToplamTutar = siparis.ToplamTutar;
                mevcut.Durum = siparis.Durum;

                _context.SaveChanges();
            }
        }
    }
}
