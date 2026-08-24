using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task <Users?> LoginAsync (string kulAd, string sifre);
    }
}
