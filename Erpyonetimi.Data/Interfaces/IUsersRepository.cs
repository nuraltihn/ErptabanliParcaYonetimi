using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<Users?> LoginAsync(string kulAd, string sifre);
         Task<List<Users>> GetAllAsync();
            Task AddAsync(Users user);
            Task UpdateAsync(Users user);
            Task DeleteAsync(int id);
        Task<Users?> GetByKulAdAsync(string kulAd);

        Task<Users?> GetByAdSoyadAsync(string adSoyad);
    }
}
