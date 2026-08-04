using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IKategoriRepository
    {
        Kategori? Isimal (string name);
        List<Kategori> GetAll ();
        void Add (Kategori kategori);
        void Update (Kategori kategori);
        void Delete (Kategori kategori);

    }
}
