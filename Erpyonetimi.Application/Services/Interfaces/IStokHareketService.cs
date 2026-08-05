using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IStokHareketService
    {
        List<StokHareket> GetAll ();
        StokHareket? GetById(int id);

        void AddStokHareket(StokHareket stokHareket);
        void UpdateStokHareket(StokHareket stokHareket);
        void RemoveStokHareket(StokHareket stokHareket);
    }
}
