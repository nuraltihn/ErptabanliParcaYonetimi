using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IUsersRepository
    {
        Users? Logindenal(string kulAd, string sifre);
        
            void Add(Users user);
            void Update(Users user);
            void Delete(Users user);
            List<Users> GetAll();
        
    }
}
