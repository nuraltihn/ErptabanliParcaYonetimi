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
    public class RaporRepository : IRaporRepository
    {
        private readonly ErpDbContext _context;
        public RaporRepository(ErpDbContext context)
        {
            _context = context;
        }
        public async Task<List<Parca>> GetKritikStokAsync()
        {
         if(!DatabaseHelper.IsConnected)

                    return new List<Parca>();

            return await _context.Parcalar
                   .Include(x => x.Kategori).Include(x => x.Tedarikci)
                   .Include(x => x.Raf).Where(x => x.MevcutStok <= x.MinimumStok)
                   .AsNoTracking()
                   .ToListAsync();

        }

        public async Task<List<Siparis>> GetSiparisAsync()
        {
           if(!DatabaseHelper.IsConnected)
                return new List<Siparis>();
            return await _context.Siparisler
                 .Include(x => x.Musteri)
                 .Include(x => x.SiparisDetaylari)
                 .ThenInclude(x => x.Parca)
                 .AsNoTracking()
                 .OrderByDescending(x => x.SiparisTarihi)
                 .ToListAsync();
        }

        public async Task<List<Parca>> GetStokDurumAsync()
        {
              if(!DatabaseHelper.IsConnected)
                return new List<Parca>();
            return await _context.Parcalar
              .Include(x => x.Kategori).Include(x => x.Tedarikci).Include(x => x.Raf)
              .AsNoTracking()
              .ToListAsync();
        }

        public async Task<List<StokHareket>> GetStokHareketleriAsync()
        {
            if(!DatabaseHelper.IsConnected)
                return new List<StokHareket>();

            return await _context.StokHareketleri
                .Include(x => x.Parca).Include(x => x.Kullanici)
                .Include(x => x.Depo)
                .AsNoTracking()
                .OrderByDescending(x => x.Tarih)
                .ToListAsync();
        }
    }
}
