using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ITedarikciService
    {
        List<Tedarikci> GetAll ();
        Tedarikci? GetById(int id);
        Tedarikci? GetByKod(string tedarikciKodu);
        void AddTedarikci(Tedarikci tedarikci);
        void RemoveTedarikci(Tedarikci tedarikci);
        void UpdateTedarikci(Tedarikci tedarikci);
    }
}
