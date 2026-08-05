using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface ISiparisRepository
    {
        List<Siparis> GetAll();
        Siparis? GetById(int id);
        Siparis? GetByNo(string siparisNo);

        void Add(Siparis siparis);
        void Update(Siparis siparis);
        void Delete(Siparis siparis);
    }
}
