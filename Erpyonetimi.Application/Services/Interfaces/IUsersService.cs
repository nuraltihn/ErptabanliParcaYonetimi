using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IUsersService
    {
        List<Users> GetAllUsers();
        void AddUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(int id);
        Users? GetByAdSoyad(string adSoyad);
    }
}
