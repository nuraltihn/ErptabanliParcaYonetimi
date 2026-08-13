using Erpyonetimi.Context;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Erpyonetimi.Helpers;
namespace Erpyonetimi.Data.Repositories 
{
    public class UsersRepository : IUsersRepository
    {
        public readonly ErpDbContext _context;
        public UsersRepository(ErpDbContext context)
        {
            _context = context;
        }

        public Users? Logindenal(string kulAd, string sifre)
        {
            return _context.Users
                .Include(u => u.Rol)
                .FirstOrDefault(u => 
                u.KulAd == kulAd && u.Sifre == sifre);
        }

        public void Add(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(Users user)
        {
            var mevcut = _context.Users.Find(user.Id);
            if (mevcut != null)
            {
                mevcut.AdSoyad = user.AdSoyad;
                mevcut.KulAd = user.KulAd;
                mevcut.Sifre = user.Sifre;
                mevcut.RolId = user.RolId
;
                _context.SaveChanges();
            }
            
        }
        public void Delete(int id)
        {
            var user= _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public List<Users> GetAll()
        {
            return _context.Users
                .Include(x => x.Rol)
                 .AsNoTracking()
                .ToList();
        }

    }
}
