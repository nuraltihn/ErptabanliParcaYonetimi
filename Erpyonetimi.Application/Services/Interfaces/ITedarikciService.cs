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
        Tedarikci? GetById(int id);
        void AddTedarikci(Tedarikci tedarikci);
        void DeleteTedarikci(int id);
        void UpdateTedarikci(Tedarikci tedarikci);
    }
}
