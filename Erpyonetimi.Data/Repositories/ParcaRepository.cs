using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Erpyonetimi.Context;
namespace Erpyonetimi.Data.Repositories
{
    public class ParcaRepository : IParcaRepository
    {
        private readonly ErpDbContext _context;
        public ParcaRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Parca parca)
        {
            _context.Parcalar.Add(parca);
            _context.SaveChanges();
        }

        public void Delete(Parca parca)
        {
            _context.Parcalar.Remove(parca);
            _context.SaveChanges();
        }

        public List<Parca> GetAll()
        {
            return _context.Parcalar
                 .Include(x => x.Kategori)
                 .Include(x => x.Tedarikci).ToList();
        }

        public Parca? GetById(int id)
        {
            return _context.Parcalar.FirstOrDefault(x => x.Id == id);
        }

        public Parca? GetByKod(string parcaKodu)
        {
            return _context.Parcalar.FirstOrDefault(x => x.ParcaKodu == parcaKodu);
        }

        public void Update(Parca parca)
        {
            _context.Parcalar.Update(parca);
            _context.SaveChanges();
        }
    }
}
