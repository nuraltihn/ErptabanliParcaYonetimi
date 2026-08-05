using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Data.Interfaces
{
    public interface IParcaRepository
    {
        List<Parca> GetAll();
        Parca? GetById(int id);
        Parca? GetByKod(string parcaKodu);
        void Add(Parca parca);
        void Update(Parca parca);
        void Delete(Parca parca);

    }
}
