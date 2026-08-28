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
            if (!DatabaseHelper.IsConnected)
                return null;

            return await _context.Kategoriler.FirstOrDefaultAsync(k => k.Id == id);
        }
        public async Task<Kategori?> GetByIdWithParcalarAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;

            return await _context.Kategoriler
                .Include(k => k.Parcalar)
                    .ThenInclude(p => p.StokHareketleri)
                .Include(k => k.Parcalar)
                    .ThenInclude(p => p.SiparisDetaylari)
                .FirstOrDefaultAsync(k => k.Id == id);
        }
        public async Task AddAsync(Kategori kategori)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            await _context.Kategoriler.AddAsync(kategori);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Kategori kategori)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            var eskiKategori = await _context.Kategoriler.FirstOrDefaultAsync(k => k.Id == kategori.Id);
            if (eskiKategori != null)
            {
                eskiKategori.KategoriAdi = kategori.KategoriAdi;
                eskiKategori.Aciklama = kategori.Aciklama;
               await _context.SaveChangesAsync();
            }
            
        }
        public async Task DeleteAsync(Kategori kategori)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            _context.Kategoriler.Remove(kategori);
           await _context.SaveChangesAsync();
        }
    }
}

