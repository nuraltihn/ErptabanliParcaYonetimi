using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Users? Login(string kulAd, string sifre);
    }
}
