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
    public class MusteriRepository : IMusteriRepository
    {
        private readonly ErpDbContext _context;
        public MusteriRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Musteri musteri)
        {
            _context.Musteriler.Add(musteri);
            _context.SaveChanges();
        }

        public void Delete(Musteri musteri)
        {
            var dbMusteri = _context.Musteriler
                .Include(m => m.Siparisler)
                .FirstOrDefault(m => m.Id == musteri.Id);

            if (dbMusteri == null)
            {
                throw new Exception("Müşteri bulunamadı.");
            }

            if (dbMusteri.Siparisler.Any())
            {
                throw new Exception(
                    "Bu müşteri silinemez. Müşteriye bağlı siparişler bulunmaktadır.");
            }

            _context.Musteriler.Remove(dbMusteri);
            _context.SaveChanges();
        }

        public List<Musteri> GetAll()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Musteri>();
            return _context.Musteriler
            .AsNoTracking()
            .ToList();
        }
       

        public Musteri? GetById(int id)
        {
            return _context.Musteriler.FirstOrDefault(m=>m.Id == id);
        }

        public Musteri? GetByKod(string musteriKodu)
        {
            return _context.Musteriler.FirstOrDefault(m=>m.MusteriKodu == musteriKodu);
        }

        public void Update(Musteri musteri)
        {
            var mevcut = _context.Musteriler.FirstOrDefault(x => x.Id == musteri.Id);
            if (mevcut == null)
                return;

            mevcut.MusteriKodu = musteri.MusteriKodu;
            mevcut.FirmaAdi = musteri.FirmaAdi;
            mevcut.YetkiliKisi = musteri.YetkiliKisi;
            mevcut.Ad = musteri.Ad;
            mevcut.Soyad = musteri.Soyad;
            mevcut.Adres = musteri.Adres;
            mevcut.Sehir = musteri.Sehir;
            mevcut.Tel = musteri.Tel;
            mevcut.Email = musteri.Email;
            mevcut.VergiNo = musteri.VergiNo;
            mevcut.Fax = musteri.Fax;
            _context.SaveChanges();
        }
    }
}
