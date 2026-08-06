using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ITedarikciService
    {
        List<Tedarikci> GetAllTedarikci();
        void AddTedarikci(Tedarikci tedarikci);
    }
}
