using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Erpyonetimi.Data.Interfaces
{
    public interface ITedarikciRepository
    {
        List<Tedarikci> GetAll();
        Tedarikci? GetById(int id);
        Tedarikci? GetByKod(string kod);
        void Add(Tedarikci tedarikci);
        void Update(Tedarikci tedarikci);
        void Delete(Tedarikci tedarikci);
    }
}
