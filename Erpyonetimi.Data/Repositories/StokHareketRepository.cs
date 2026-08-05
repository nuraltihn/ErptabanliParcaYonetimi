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
            _context.StokHareketleri.Update(stokHareket);
            _context.SaveChanges();
        }
    }
}
