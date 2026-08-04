using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface ITedarikciRepository
    {
        List<Tedarikci> GetAll();
        Tedarikci? IdAl(int id);
        Tedarikci? Kodal(string tedarikciKodu);
        void Add(Tedarikci tedarikci);
        void Update(Tedarikci tedarikci);
        void Delete(Tedarikci tedarikci);
    }
}
