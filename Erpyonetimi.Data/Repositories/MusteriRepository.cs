using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Repositories
{
    public class MusteriRepository : IMusteriRepository
    {
        private readonly ErpDbContext _context;
        public MusteriRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Musteri musteri)
        {
            _context.Musteriler.Add(musteri);
            _context.SaveChanges();
        }

        public void Delete(Musteri musteri)
        {
            _context.Musteriler.Remove(musteri);
            _context.SaveChanges();
        }

        public List<Musteri> GetAll()
        {
            return _context.Musteriler.ToList();
        }

        public Musteri? GetById(int id)
        {
            return _context.Musteriler.FirstOrDefault(m=>m.Id == id);
        }

        public Musteri? GetByKod(string musteriKodu)
        {
            return _context.Musteriler.FirstOrDefault(m=>m.MusteriKodu == musteriKodu);
        }

        public void Update(Musteri musteri)
        {
            _context.Musteriler.Update(musteri);
            _context.SaveChanges();
        }
    }
}
