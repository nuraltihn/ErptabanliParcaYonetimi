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
        public async Task AddAsync(Musteri musteri)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            await _context.Musteriler.AddAsync(musteri);
           await  _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Musteri musteri)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            _context.Musteriler.Remove(musteri);
            await _context.SaveChangesAsync();
        }


        public async Task< List<Musteri>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Musteri>();
            return await _context.Musteriler
            .AsNoTracking()
            .ToListAsync();
        }
       

        public async Task< Musteri?> GetByIdAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Musteriler.FirstOrDefaultAsync(m=>m.Id == id);
        }
        public async Task<Musteri?> GetByIdWithIliskilerAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Musteriler
                .Include(m => m.Siparisler)
                .FirstOrDefaultAsync (m => m.Id == id); 
        }

        public async Task<Musteri?> GetByKodAsync(string musteriKodu)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Musteriler.FirstOrDefaultAsync(m=>m.MusteriKodu == musteriKodu);
        }

        public async Task UpdateAsync(Musteri musteri)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            var mevcut = await _context.Musteriler.FirstOrDefaultAsync(x => x.Id == musteri.Id);
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
          await  _context.SaveChangesAsync();
        }
    }
}
