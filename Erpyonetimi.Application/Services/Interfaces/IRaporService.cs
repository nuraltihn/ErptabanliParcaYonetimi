using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IRaporService
    {
        Task<List<Parca>> GetStokDurumuAsync();
        Task<List<Parca>> GetKritikStokAsync();
        Task<List<StokHareket>> GetStokHareketleriAsync();
        Task<List<Siparis>> GetSiparislerAsync();
    }
}
