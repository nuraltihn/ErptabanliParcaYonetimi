using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IMusteriService
    {
        List<Musteri> GetAll();
        Musteri? GetById(int id);
        Musteri? GetByKod(string musteriKodu);
        void AddMusteri(Musteri musteri);
        void UpdateMusteri(Musteri musteri);
        void DeleteMusteri(Musteri musteri);
    }
}
