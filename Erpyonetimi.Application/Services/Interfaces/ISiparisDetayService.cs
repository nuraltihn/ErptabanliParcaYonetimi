using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ISiparisDetayService
    {
        List<SiparisDetaylari> GetAll ();
        SiparisDetaylari? GetById(int id);

        void AddDetay(SiparisDetaylari detay);
        void DeleteDetay(SiparisDetaylari detay);
        void UpdateDetay(SiparisDetaylari detay);
    }
}
