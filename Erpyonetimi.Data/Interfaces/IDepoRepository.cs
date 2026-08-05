using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IDepoRepository
    {
        List<Depolar> GetAll();
        Depolar? GetById(int id);
        void Add(Depolar depo);
        void Update(Depolar depo);
        void Delete(Depolar depo);
    }
}
