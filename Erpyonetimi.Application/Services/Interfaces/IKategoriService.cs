using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IKategoriService
    {
        List<Kategori> GetAllKategori();
        void AddKategori(Kategori kategori);
        void UpdateKategori(Kategori kategori);
        void DeleteKategori(int id);
    
    }
}
