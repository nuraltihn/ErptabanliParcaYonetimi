using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class StokHareketRepository : IStokHareketRepository
    {
        private readonly ErpDbContext _context;
        public StokHareketRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(StokHareket stokHareket)
        {
            _context.StokHareketleri.Add(stokHareket);
            _context.SaveChanges();
        }

        public void Delete(StokHareket stokHareket)
        {
            _context.StokHareketleri.Remove(stokHareket);
            _context.SaveChanges();
        }

        public List<StokHareket> GetAll()
        {
            return _context.StokHareketleri
                .Include(x => x.Parca)
                .Include(x => x.Kullanici)
                .Include(x => x.Depo)
                .ToList();
        }

        public StokHareket? GetById(int id)
        {
            return _context.StokHareketleri.FirstOrDefault(x => x.Id == id);
        }

        public void Update(StokHareket stokHareket)
        {
            var mevcut = _context.StokHareketleri
                .FirstOrDefault(x => x.Id == stokHareket.Id);

            if (mevcut != null)
            {
                mevcut.ParcaId = stokHareket.ParcaId;
                mevcut.KullaniciId = stokHareket.KullaniciId;
                mevcut.DepoId = stokHareket.DepoId;
                mevcut.IslemTipi = stokHareket.IslemTipi;
                mevcut.Miktar = stokHareket.Miktar;
                mevcut.Tarih = stokHareket.Tarih;
                mevcut.Aciklama = stokHareket.Aciklama;

                _context.SaveChanges();
            }
        }
    }
}
