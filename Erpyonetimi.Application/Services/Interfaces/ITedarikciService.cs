using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ITedarikciService
    {
        List<Tedarikci> TedarikciGetAll();
        Tedarikci? IdAl(int id);
        Tedarikci? KodAl(string tedarikciKodu);
        void AddTedarikci(Tedarikci tedarikci);
        void RemoveTedarikci(Tedarikci tedarikci);
        void UpdateTedarikci(Tedarikci tedarikci);
    }
}
