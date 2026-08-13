using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Erpyonetimi.Data.Repositories
{
    public class RafRepository : IRafRepository
    {
        private readonly ErpDbContext _context;
        public RafRepository(ErpDbContext context)
        {
            _context=context;
        }

        public void Add(Raflar raf)
        {
            _context.Raflar.Add(raf);
            _context.SaveChanges();
        }

        public void Delete(Raflar raf)
        {
            _context.Raflar.Remove(raf);
            _context.SaveChanges();
        }

        public List<Raflar> GetAll()
        {
            return _context.Raflar.Include(r => r.Depo).ToList();
        }

        public Raflar? GetById(int id)
        {
            return _context.Raflar.FirstOrDefault(r => r.Id == id);
        }

        public Raflar? GetByKod(string rafkodu)
        {
            return _context.Raflar.FirstOrDefault(r => r.RafKodu == rafkodu);
        }

        public void Update(Raflar raf)
        {
            var mevcut = _context.Raflar
                .FirstOrDefault(x => x.Id == raf.Id);
            if (mevcut != null)
            {
                mevcut.DepoId = raf.DepoId;
                mevcut.RafKodu = raf.RafKodu;
                _context.SaveChanges();
            }
        }
    }
}
