using Erpyonetimi.Context;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Services
{
    public class AuthService
    {
        public Users? Login(string kulAd, string sifre)
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            string hash = PasswordHelper.HashPassword(sifre);

            return db.Users
                .Include(x => x.Rol)
                .FirstOrDefault(x =>
                x.KulAd == kulAd &&
                x.Sifre == hash);
        }
    }
}
