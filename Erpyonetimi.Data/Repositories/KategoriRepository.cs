using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;
namespace Erpyonetimi.Data.Repositories
{
    public class KategoriRepository : IKategoriRepository
    {
        private readonly ErpDbContext _context;
        public KategoriRepository(ErpDbContext context)
        {
            _context = context;
        }

        public List<Kategori> GetAll()
        {
            return _context.Kategoriler.ToList();
        }
        public Kategori? GetById(int id)
        {
            return _context.Kategoriler.FirstOrDefault(k => k.Id == id);
        }
        public void Add(Kategori kategori)
        {
            _context.Kategoriler.Add(kategori);
            _context.SaveChanges();
        }

        public void Update(Kategori kategori)
        {
            var eskiKategori = _context.Kategoriler.FirstOrDefault(k => k.Id == kategori.Id);
            if (eskiKategori != null)
            {
                eskiKategori.KategoriAdi = kategori.KategoriAdi;
                eskiKategori.Aciklama = kategori.Aciklama;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Kategori bulunamadı.");
            }
        }
        public void Delete(Kategori kategori)
        {
            var dbKategori = _context.Kategoriler
     .Include(k => k.Parcalar)
     .FirstOrDefault(k => k.Id == kategori.Id);

            if (dbKategori == null)
            {
                throw new Exception("Kategori bulunamadı.");
            }

            if (dbKategori.Parcalar.Any())
            {
                throw new Exception("Bu kategori kullanımda.");
            }

            _context.Kategoriler.Remove(dbKategori);
            _context.SaveChanges();
        }
    }
}

