using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
namespace Erpyonetimi.Data.Repositories
{
    public class ParcaRepository : IParcaRepository
    {
        private readonly ErpDbContext _context;
        public ParcaRepository(ErpDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Parca parca)
        {
           await _context.Parcalar.AddAsync(parca);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Parca parca)
        {
            _context.Parcalar.Remove(parca);
           await _context.SaveChangesAsync();
        }
        public async Task< List<Parca>> GetAllAsync()
        {
         if(!DatabaseHelper.IsConnected)
                return new List<Parca>();
            return await _context.Parcalar
                 .Include(x => x.Kategori)
                 .Include(x => x.Tedarikci)
                 .Include(x => x.Raf)
                 .ToListAsync();
            
        }

        public async Task<Parca?> GetByIdAsync(int id)
        {
            return await _context.Parcalar.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Parca?> GetByIdWithIliskilerAsync(int id)
        {
            return await _context.Parcalar
                .Include(p => p.StokHareketleri)
                .Include(p => p.SiparisDetaylari)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Parca?> GetByKodAsync(string parcaKodu)
        {
            return await _context.Parcalar.FirstOrDefaultAsync(x => x.ParcaKodu == parcaKodu);
        }

        public async Task UpdateAsync(Parca parca)
        {
            var eskiParca = await _context.Parcalar
                .FirstOrDefaultAsync(x => x.Id == parca.Id);

            if (eskiParca != null)
            {
                eskiParca.ParcaKodu = parca.ParcaKodu;
                eskiParca.ParcAdi = parca.ParcAdi;
                eskiParca.KategoriId = parca.KategoriId;
                eskiParca.TedarikciId = parca.TedarikciId;
                eskiParca.Marka = parca.Marka;
                eskiParca.Model = parca.Model;
                eskiParca.Malzeme = parca.Malzeme;
                eskiParca.Agirlik = parca.Agirlik;
                eskiParca.Uzunluk = parca.Uzunluk;
                eskiParca.Genislik = parca.Genislik;
                eskiParca.Yukseklik = parca.Yukseklik;
                eskiParca.Renk = parca.Renk;
                eskiParca.Birim = parca.Birim;
                eskiParca.AlisFiyat = parca.AlisFiyat;
                eskiParca.SatisFiyat = parca.SatisFiyat;
                eskiParca.MevcutStok = parca.MevcutStok;
                eskiParca.MinimumStok = parca.MinimumStok;
                eskiParca.RafId = parca.RafId;
                eskiParca.Aciklama = parca.Aciklama;

               await _context.SaveChangesAsync();
            }
        }
    }
}
