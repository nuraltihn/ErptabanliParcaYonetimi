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
        public readonly ErpDbContext _context;
        public TedarikciRepository(ErpDbContext context)
        {
            _context = context;
        }
        public Tedarikci? KodAl(string tedarikciKodu)
        {
            return _context.Tedarikciler.FirstOrDefault(x => x.TedarikciKodu == tedarikciKodu);

        }
        public List<Tedarikci> TedarikciGetAll()
        {
           return _context.Tedarikciler.ToList();
        }
        public Tedarikci? IdAl(int id)
        {
            return _context.Tedarikciler.FirstOrDefault(x => x.Id == id);
        }
            
        public void Add(Tedarikci tedarikci)
        {
            _context.Tedarikciler.Add(tedarikci);
            _context.SaveChanges();
        }
        public void Update(Tedarikci tedarikci)
        {
            _context.Tedarikciler.Update(tedarikci);
            _context.SaveChanges();
        }
        public void Delete(Tedarikci tedarikci)
        {
            _context.Remove(tedarikci);
            _context.SaveChanges();
        }

        public Tedarikci? KodAl(string tedarikciKodu)
        {
            return _context.Tedarikciler.FirstOrDefault(x => x.TedarikciKodu == tedarikciKodu);
        }
    }
}
