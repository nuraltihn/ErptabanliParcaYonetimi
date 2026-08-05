using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface ISiparisDetayRepository
    {
        List<SiparisDetaylari> GetAll();
        SiparisDetaylari? GetById(int id);

        void Add(SiparisDetaylari detay);
        void Update(SiparisDetaylari detay);
        void Delete(SiparisDetaylari detay);
    }
}
