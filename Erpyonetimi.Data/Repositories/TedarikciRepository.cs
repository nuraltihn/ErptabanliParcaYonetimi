using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Erpyonetimi.Data.Repositories
{
    public class TedarikciRepository : ITedarikciRepository
    {
        private readonly ErpDbContext _context;

        public TedarikciRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tedarikci>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Tedarikci>();
                return await _context.Tedarikciler
                .AsNoTracking()
                .ToListAsync();
           
        }

        public async Task AddAsync(Tedarikci tedarikci)
        {
            await _context.Tedarikciler.AddAsync(tedarikci);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tedarikci tedarikci)
        {
            var mevcut = await _context.Tedarikciler.FirstOrDefaultAsync(x => x.Id == tedarikci.Id);

            if (mevcut != null)
            {
                mevcut.TedarikciKodu = tedarikci.TedarikciKodu;
                mevcut.FirmaAdi = tedarikci.FirmaAdi;
                mevcut.YetkiliKisi = tedarikci.YetkiliKisi;
                mevcut.Tel = tedarikci.Tel;
                mevcut.Email = tedarikci.Email;
                mevcut.Adres = tedarikci.Adres;
                mevcut.Fax = tedarikci.Fax;
                mevcut.VergiNo = tedarikci.VergiNo;

               await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(Tedarikci tedarikci)
        {
         
          _context.Tedarikciler.Remove(tedarikci);

           await  _context.SaveChangesAsync();
        }

        public async Task<Tedarikci?> GetByIdAsync(int id)
        {
            return await _context.Tedarikciler.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Tedarikci?> GetByIdWithIliskilerAsync(int id)
        {
            return await _context.Tedarikciler
                     .Include(t => t.Parcalar)
                     .ThenInclude(p => p.StokHareketleri)
                     .Include(t => t.Parcalar)
                     .ThenInclude(p => p.SiparisDetaylari)
                     .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<Tedarikci?> GetByKodAsync(string kod)
        {
            return await _context.Tedarikciler.FirstOrDefaultAsync(x =>
            x.TedarikciKodu == kod);
        }
    }
}