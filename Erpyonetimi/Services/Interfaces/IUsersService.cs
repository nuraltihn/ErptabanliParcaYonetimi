using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Services.Interfaces
{
    public interface IUsersService
    {
        List<Users> GetAll();
        void AddUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(int id);
    }
}
