using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IRafService
    {
        List<Raflar> GetAll ();
        Raflar? GetById(int id);
        Raflar? GetByKod(string rafkodu);
        void AddRaf(Raflar raf);
        void UpdateRaf(Raflar raf);
        void RemoveRaf(Raflar raf);
    }
}
