using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Erpyonetimi.Data.Repositories 
{
    public class UsersRepository : IUsersRepository
    {
        public readonly ErpDbContext _context;
        public UsersRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task<Users?> LoginAsync(string kulAd, string sifre)
        {
            if (!DatabaseHelper.IsConnected)
                return null;

            return await _context.Users
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => 
                u.KulAd == kulAd && u.Sifre == sifre);
        }
 
        public async Task AddAsync(Users user)
        {
            if (!DatabaseHelper.IsConnected)
                return ;

            await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            if (!DatabaseHelper.IsConnected)
                return;

            var mevcut = await _context.Users.FindAsync(user.Id);
            if (mevcut != null)
            {
                mevcut.AdSoyad = user.AdSoyad;
                mevcut.KulAd = user.KulAd;
                mevcut.Sifre = user.Sifre;
                mevcut.RolId = user.RolId;
                mevcut.Tel = user.Tel;
                mevcut.Email = user.Email;

               await _context.SaveChangesAsync();
            }
            
        }
        public async Task DeleteAsync(int id)
        {
            if (!DatabaseHelper.IsConnected)
                return;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
               
                _context.Users.Remove(user);

               await _context.SaveChangesAsync();
            }
        }
        public async Task< List<Users>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Users>();

                return await _context.Users
                .Include(x => x.Rol)
                 .AsNoTracking()
                .ToListAsync();
            
        }
        public async Task<Users?> GetByKulAdAsync (string kulAd)
        {
            if (!DatabaseHelper.IsConnected)
                return null;

            return await _context.Users
                  .FirstOrDefaultAsync(x => x.KulAd == kulAd);
        }

        public async Task<Users?> GetByAdSoyadAsync(string adSoyad)
        {
            if (!DatabaseHelper.IsConnected)
                return null;

            return await _context.Users.FirstOrDefaultAsync(x => x.AdSoyad == adSoyad);
        }
    }
}
