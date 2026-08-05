using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IRafRepository
    {
        List<Raflar> GetAll();
        Raflar? GetById(int id);
        Raflar? GetByKod(string rafkodu);
        void Add(Raflar raf);
        void Update(Raflar raf);
        void Delete(Raflar raf);
    }
}
