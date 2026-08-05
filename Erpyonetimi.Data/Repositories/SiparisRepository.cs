using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class SiparisRepository : ISiparisRepository
    {
        private readonly ErpDbContext _context;
        public SiparisRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Siparis siparis)
        {
            _context.Siparisler.Add(siparis);
            _context.SaveChanges();
        }

        public void Delete(Siparis siparis)
        {
            _context.Siparisler.Remove(siparis);
            _context.SaveChanges();
        }

        public List<Siparis> GetAll()
        {
            return _context.Siparisler.Include(s => s.Musteri).ToList();
        }

        public Siparis? GetById(int id)
        {
            return _context.Siparisler.FirstOrDefault(x=>x.Id == id);
        }

        public Siparis? GetByNo(string siparisNo)
        {
            return _context.Siparisler.FirstOrDefault(x=>x.SiparisNo == siparisNo);
        }

        public void Update(Siparis siparis)
        {
            _context.Siparisler.Update(siparis);
            _context.SaveChanges();
        }
    }
}
