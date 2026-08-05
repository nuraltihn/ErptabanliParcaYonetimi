using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IStokHareketRepository
    {
        List<StokHareket> GetAll();
        StokHareket? GetById(int id);

        void Add(StokHareket stokHareket);
        void Update(StokHareket stokHareket);
        void Delete(StokHareket stokHareket);
    }
}
