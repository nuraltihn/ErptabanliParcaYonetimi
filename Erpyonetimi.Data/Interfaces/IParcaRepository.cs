using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IParcaRepository
    {
        List<Parca> GetAll();
        Parca? IdAl(int id);

        void Add(Parca parca);
        void Update(Parca parca);
        void Delete(Parca parca);

    }
}
