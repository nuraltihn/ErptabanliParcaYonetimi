
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using Erpyonetimi.Application.Common;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IKategoriService
    {
        Task <List<Kategori>> GetAllKategoriAsync();
        Task  AddKategoriAsync (Kategori kategori);
        Task <ServiceResult> UpdateKategoriAsync (Kategori kategori);
        Task <ServiceResult> DeleteKategoriAsync (int id);
    
    }
}
