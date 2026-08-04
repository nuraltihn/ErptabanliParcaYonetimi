using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
namespace Erpyonetimi.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public List<Users> GetAllUsers()
        {
            return _usersRepository.GetAll();
        }

        public void AddUser(Users user)
        {
            _usersRepository.Add(user);
        }
        public void UpdateUser(Users user)
        {
            _usersRepository.Update(user);
        }
        public void DeleteUser(int id)
        {
            _usersRepository.Delete(id);
        }
    }
}
