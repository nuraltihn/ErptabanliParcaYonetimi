using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Repositories
{
    public class DepoRepository : IDepoRepository
    {
        private readonly ErpDbContext _context;
        public DepoRepository(ErpDbContext context)
        {
            _context = context;
        }

        public void Add(Depolar depo)
        {
            _context.Depolar.Add(depo);
            _context.SaveChanges();
        }

        public void Delete(Depolar depo)
        {
            _context.Depolar.Remove(depo);
            _context.SaveChanges();
        }

        public List<Depolar> GetAll()
        {
            return _context.Depolar.ToList();
        }

        public Depolar? GetById(int id)
        {
            return _context.Depolar.FirstOrDefault(d => d.Id == id);
        }

        public void Update(Depolar depo)
        {
            _context.Depolar.Update(depo);
            _context.SaveChanges();
        }
    }
}
