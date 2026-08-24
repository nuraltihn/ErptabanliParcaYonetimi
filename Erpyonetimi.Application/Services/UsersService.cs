using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Helpers;

namespace Erpyonetimi.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task <List<Users>> GetAllUsersAsync ()
        {
            return await _usersRepository.GetAllAsync ();
        }

        public async Task AddUserAsync(Users user)
        {
            user.Sifre = PasswordHelper.HashPassword(user.Sifre);

           await  _usersRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync (Users user)
        {
           await  _usersRepository.UpdateAsync (user);
        }

        public async Task DeleteUserAsync (int id)
        {
            await _usersRepository.DeleteAsync (id);
        }

        public async Task  <Users?> GetByAdSoyadAsync (string adSoyad)
        {
            return await _usersRepository.GetByAdSoyadAsync(adSoyad);
        }
    }
}

