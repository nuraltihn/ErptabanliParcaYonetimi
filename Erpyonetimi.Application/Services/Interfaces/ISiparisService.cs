using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ISiparisService
    {
        List<Siparis> GetAllSiparis();
        Siparis? GetById(int id);
        Siparis? GetByNo(string siparisNo);

        void AddSiparis(Siparis siparis);
        void UpdateSiparis(Siparis siparis);
        void RemoveSiparis(Siparis siparis);
    }
}
