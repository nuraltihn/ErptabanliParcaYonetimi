using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;


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
            if (!DatabaseHelper.IsConnected)
                return new List<Tedarikci>();
                return _context.Tedarikciler
                .AsNoTracking()
                .ToList();
           
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
            _context.Tedarikciler.Remove(tedarikci);
            _context.SaveChanges();
        }

        public Tedarikci? GetById(int id)
        {
            return _context.Tedarikciler.FirstOrDefault(x => x.Id == id);
        }

        public Tedarikci? GetByKod(string kod)
        {
            return _context.Tedarikciler.FirstOrDefault(x =>
            x.TedarikciKodu == kod);
        }
    }
}