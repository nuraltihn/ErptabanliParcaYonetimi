using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IMusteriRepository
    {
        List<Musteri> GetAll();
        Musteri? GetById(int id);
        Musteri? GetByKod(string musteriKodu);

        void Add(Musteri musteri);
        void Update(Musteri musteri);
        void Delete(Musteri musteri);
    }
}
