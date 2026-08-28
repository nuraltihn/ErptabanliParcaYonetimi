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
        public async Task AddAsync(SiparisDetaylari detay)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            await  _context.SiparisDetaylari.AddAsync(detay);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SiparisDetaylari detay)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            var mevcut = await _context.SiparisDetaylari.FirstOrDefaultAsync(x => x.Id == detay.Id);
            if (mevcut != null)
            {
                _context.SiparisDetaylari.Remove(mevcut);
               await _context.SaveChangesAsync();
            }
                
        }

        public async Task<List<SiparisDetaylari>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<SiparisDetaylari>();
                return await _context.SiparisDetaylari
                .Include(x => x.Siparis)
                .Include(x => x.Parca)
                .AsNoTracking()
                .ToListAsync();
          
        }

        public async Task< SiparisDetaylari?> GetByIdAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.SiparisDetaylari
                .Include(x => x.Parca)
                .Include(x => x.Siparis)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(SiparisDetaylari detay)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            var mevcut = await _context.SiparisDetaylari.FindAsync(detay.Id);
            if (mevcut != null)
            {
                mevcut.SiparisId = detay.SiparisId;
                mevcut.ParcaId = detay.ParcaId;
                mevcut.Miktar = detay.Miktar;
                mevcut.BirimFiyat = detay.BirimFiyat;
                mevcut.ToplamFiyat = detay.ToplamFiyat;
                await _context.SaveChangesAsync();
            }
        }
    }
}
