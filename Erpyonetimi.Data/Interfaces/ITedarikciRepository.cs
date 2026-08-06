using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Erpyonetimi.Data.Interfaces
{
    public interface ITedarikciRepository
    {
        List<Tedarikci> GetAll();
        void Add(Tedarikci tedarikci);
    }
}
