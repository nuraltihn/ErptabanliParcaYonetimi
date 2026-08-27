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
    public class RafRepository : IRafRepository
    {
        private readonly ErpDbContext _context;
        public RafRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Raflar raf)
        {
            await _context.Raflar.AddAsync(raf);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Raflar raf)
        {
            _context.Raflar.Remove(raf);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Raflar>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Raflar>();
            return await _context.Raflar.Include(r => r.Depo).ToListAsync();

        }

        public async Task<Raflar?> GetByIdAsync(int id)
        {
            return await _context.Raflar.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Raflar?> GetByIdWithParcalarAsync(int id)
        {
            return await _context.Raflar
                .Include(r => r.Parcalar)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Raflar?> GetByKodAsync(string rafkodu)
        {
            return await _context.Raflar.FirstOrDefaultAsync(r => r.RafKodu == rafkodu);
        }

        public async Task UpdateAsync(Raflar raf)
        {
            var mevcut = await _context.Raflar
                .FirstOrDefaultAsync(x => x.Id == raf.Id);
            if (mevcut != null)
            {
                mevcut.DepoId = raf.DepoId;
                mevcut.RafKodu = raf.RafKodu;
                await _context.SaveChangesAsync();
            }
        }
    }
}
