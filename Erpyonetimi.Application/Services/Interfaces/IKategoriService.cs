
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IKategoriService
    {
        Task <List<Kategori>> GetAllKategoriAsync();
        Task  AddKategoriAsync (Kategori kategori);
        Task  UpdateKategoriAsync (Kategori kategori);
        Task  DeleteKategoriAsync (int id);
    
    }
}
