using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class DepoRepository : IDepoRepository
    {
        private readonly ErpDbContext _context;
        public DepoRepository(ErpDbContext context)

        {
            _context = context;
        }

        public async Task AddAsync(Depolar depo)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            await _context.Depolar.AddAsync(depo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Depolar depo)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            _context.Depolar.Remove(depo);
           await _context.SaveChangesAsync();
        }

        public async Task<List<Depolar>> GetAllAsync()
        { if(!DatabaseHelper.IsConnected)

                return new List<Depolar>();
            return await _context.Depolar.ToListAsync();  
        }
        public async Task<Depolar?> GetByDepoadiAsync(string depoadi)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Depolar.FirstOrDefaultAsync(x=>x.Depaadi==depoadi);
        }

        public async Task<Depolar?> GetByIdAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Depolar.FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<Depolar?> GetByIdWithIliskilerAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return null;
            return await _context.Depolar
                .Include(d => d.Raflar)
                .ThenInclude(r => r.Parcalar)
                .ThenInclude(p => p.StokHareketleri)
                .Include(d => d.Raflar)
                .ThenInclude(r => r.Parcalar)
                .ThenInclude(p => p.SiparisDetaylari)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task UpdateAsync(Depolar depo)
        {
            if (!DatabaseHelper.IsConnected)
                return;
            var guncelleme =await _context.Depolar.FirstOrDefaultAsync(x=>x.Id == depo.Id);
            if (guncelleme != null) 
            {
                guncelleme.Depaadi = depo.Depaadi;
                guncelleme.Konum = depo.Konum;
               await _context.SaveChangesAsync();
            }
        }
    }
}
