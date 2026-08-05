using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IDepoService
    {
        List<Depolar> GetAll ();
        Depolar? GetById(int id);

        void AddDepo(Depolar depo);
        void UpdateDepo(Depolar depo);
        void DeleteDepo(Depolar depo);
    }
}
