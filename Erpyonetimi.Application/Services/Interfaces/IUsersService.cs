using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task <List<Users>> GetAllUsersAsync();
        Task AddUserAsync (Users user);
        Task UpdateUserAsync (Users user);
        Task  DeleteUseAsync (int id);
       Task <Users?> GetByAdSoyadAsync (string adSoyad);
    }
}
