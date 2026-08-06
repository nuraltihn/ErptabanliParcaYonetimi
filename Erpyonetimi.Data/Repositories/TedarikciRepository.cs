using Erpyonetimi.Context;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Erpyonetimi.Data.Repositories;


namespace Erpyonetimi.Data.Repositories
{
    public class TedarikciRepository : ITedarikciRepository
    {
        private readonly ErpDbContext _context;

        public TedarikciRepository(ErpDbContext context)
        {
            _context = context;
        }

        public List<Tedarikci> GetAll()
        {
            return _context.Tedarikciler.ToList();
        }

        public void Add(Tedarikci tedarikci)
        {
            _context.Tedarikciler.Add(tedarikci);
            _context.SaveChanges();
        }
    }
}