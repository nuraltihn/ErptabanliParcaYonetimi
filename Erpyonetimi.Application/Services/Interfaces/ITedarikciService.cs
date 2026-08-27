using Erpyonetimi.Application.Common;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using Erpyonetimi.Application.Common;
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
        Task <ServiceResult> DeleteTedarikciAsync (int id);
        Task UpdateTedarikciAsync (Tedarikci tedarikci);
    }
}
