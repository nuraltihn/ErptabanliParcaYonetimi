using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ITedarikciService
    {
        Task <List<Tedarikci>> GetAllTedarikciAsync();
       Task  <Tedarikci?> GetByIdAsync (int id);
        Task  <Tedarikci?> GetByKodAsync (string kod);
        Task  AddTedarikciAsync (Tedarikci tedarikci);
        Task  DeleteTedarikciAsync (int id);
        Task UpdateTedarikciAsync (Tedarikci tedarikci);
    }
}
