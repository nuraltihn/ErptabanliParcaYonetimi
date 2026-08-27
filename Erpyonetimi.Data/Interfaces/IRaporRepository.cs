using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
namespace Erpyonetimi.Data.Interfaces
{
    public interface IRaporRepository
    {
        Task<List<Parca>> GetStokDurumAsync();
        Task<List<Parca>> GetKritikStokAsync();
        Task<List<StokHareket>> GetStokHareketleriAsync();
        Task<List<Siparis>> GetSiparisAsync();


    }
}
