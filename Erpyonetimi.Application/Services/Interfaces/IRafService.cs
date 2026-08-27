using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
using Erpyonetimi.Application.Common;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface IRafService
    {
        Task <List<Raflar>> GetAllAsync ();
        Task <Raflar?> GetByIdAsync (int id);
        Task  <Raflar?> GetByKodAsync (string rafkodu);
        Task AddRafAsync (Raflar raf);
        Task UpdateRafAsync (Raflar raf);
        Task <ServiceResult> RemoveRafAsync (Raflar raf);
    }
}
