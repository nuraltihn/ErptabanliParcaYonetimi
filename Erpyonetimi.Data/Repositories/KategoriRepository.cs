using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
namespace Erpyonetimi.Data.Repositories
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly ErpDbContext _context;
        public KategoriRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task< List<Kategori>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Kategori>();

            return await _context.Kategoriler.ToListAsync();

        }
        public async Task<Kategori?> GetByIdAsync(int id)
        {
            return await _context.Kategoriler.FirstOrDefaultAsync(k => k.Id == id);
        }
        public async Task AddAsync(Kategori kategori)
        {
            await _context.Kategoriler.AddAsync(kategori);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Kategori kategori)
        {
            var eskiKategori = await _context.Kategoriler.FirstOrDefaultAsync(k => k.Id == kategori.Id);
            if (eskiKategori != null)
            {
                eskiKategori.KategoriAdi = kategori.KategoriAdi;
                eskiKategori.Aciklama = kategori.Aciklama;
               await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Kategori bulunamadı.");
            }
        }
        public async Task DeleteAsync(Kategori kategori)
        {
            var dbKategori = await _context.Kategoriler
                .Include(k => k.Parcalar)
                    .ThenInclude(p => p.StokHareketleri)
                .Include(k => k.Parcalar)
                    .ThenInclude(p => p.SiparisDetaylari)
                .FirstOrDefaultAsync(k => k.Id == kategori.Id);

            if (dbKategori == null)
            {
                throw new Exception("Kategori bulunamadı.");
            }
            if (dbKategori.Parcalar.Any(p => p.StokHareketleri.Any()))
            {
                throw new Exception(
                    "Bu kategori silinemez. Kategoriye bağlı işlem görmüş parçalar bulunmaktadır.");
            }
            if (dbKategori.Parcalar.Any(p => p.SiparisDetaylari.Any()))
            {
                throw new Exception(
                    "Bu kategori silinemez. Kategoriye bağlı siparişlerde kullanılan parçalar bulunmaktadır.");
            }

            
            _context.Kategoriler.Remove(dbKategori);

           await _context.SaveChangesAsync();
        }
    }
}

