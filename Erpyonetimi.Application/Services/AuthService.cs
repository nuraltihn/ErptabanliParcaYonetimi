using Erpyonetimi.Context;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Helpers;
using Erpyonetimi.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Data.Interfaces;
namespace Erpyonetimi.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        public AuthService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public Users? Login(string kulAd, string sifre)
        {
            string hash = PasswordHelper.HashPassword(sifre);
            return _usersRepository.Logindenal(kulAd, hash);
        }
    }
}
