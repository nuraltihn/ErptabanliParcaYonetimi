using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Common;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IDepoService
    {
        Task <List<Depolar>> GetAllAsync();
        Task <Depolar?> GetByIdAsync(int id);
        Task  <Depolar?> GetByDepoadiAsync  (string depoadi);

        Task  AddDepoAsync (Depolar depo);
        Task UpdateDepoAsync (Depolar depo);
        Task <ServiceResult> DeleteDepoAsync (Depolar depo);
    }
}
