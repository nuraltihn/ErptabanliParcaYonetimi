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
    public class StokHareketRepository : IStokHareketRepository
    {
        private readonly ErpDbContext _context;
        public StokHareketRepository(ErpDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(StokHareket stokHareket)
        {
            if (!DatabaseHelper.IsConnected)
                return;

            await  _context.StokHareketleri.AddAsync(stokHareket);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(StokHareket stokHareket)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            _context.StokHareketleri.Remove(stokHareket);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StokHareket>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<StokHareket>();
                return await _context.StokHareketleri
                .Include(x => x.Parca)
                .Include(x => x.Kullanici)
                .Include(x => x.Depo)
                .ToListAsync();
            
        }

        public async Task<StokHareket?> GetByIdAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.StokHareketleri.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(StokHareket stokHareket)
        {
            if (!DatabaseHelper.IsConnected)
                return;

            var mevcut = await _context.StokHareketleri
                .FirstOrDefaultAsync(x => x.Id == stokHareket.Id);

            if (mevcut != null)
            {
                mevcut.ParcaId = stokHareket.ParcaId;
                mevcut.KullaniciId = stokHareket.KullaniciId;
                mevcut.DepoId = stokHareket.DepoId;
                mevcut.IslemTipi = stokHareket.IslemTipi;
                mevcut.Miktar = stokHareket.Miktar;
                mevcut.Tarih = stokHareket.Tarih;
                mevcut.Aciklama = stokHareket.Aciklama;

               await _context.SaveChangesAsync();
            }
        }
    }
}
