using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class SiparisDetayRepository : ISiparisDetayRepository
    {
        private readonly ErpDbContext _context;
        public SiparisDetayRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(SiparisDetaylari detay)
        {
            _context.SiparisDetaylari.Add(detay);
            _context.SaveChanges();
        }

        public void Delete(SiparisDetaylari detay)
        {
            _context.SiparisDetaylari.Remove(detay);
            _context.SaveChanges();
        }

        public List<SiparisDetaylari> GetAll()
        {
            return _context.SiparisDetaylari
                .Include(x => x.Siparis)
                .Include(x => x.Parca)
                .ToList();
        }

        public SiparisDetaylari? GetById(int id)
        {
            return _context.SiparisDetaylari.FirstOrDefault(x => x.Id == id);
        }

        public void Update(SiparisDetaylari detay)
        {
            _context.SiparisDetaylari.Update(detay);
            _context.SaveChanges();
        }
    }
}
