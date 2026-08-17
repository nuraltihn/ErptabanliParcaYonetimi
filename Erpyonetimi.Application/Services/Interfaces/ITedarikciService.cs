using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ITedarikciService
    {
        List<Tedarikci> GetAllTedarikci();
        Tedarikci? GetById(int id);
        Tedarikci? GetByKod(string kod);
        void AddTedarikci(Tedarikci tedarikci);
        void DeleteTedarikci(int id);
        void UpdateTedarikci(Tedarikci tedarikci);
    }
}
