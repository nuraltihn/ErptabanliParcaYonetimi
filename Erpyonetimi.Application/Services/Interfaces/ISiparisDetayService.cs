using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using System.Threading.Tasks;
namespace Erpyonetimi.Application.Services.Interfaces
{
    public interface ISiparisDetayService
    {
        Task <List<SiparisDetaylari>> GetAllAsync  ();
       Task <SiparisDetaylari?> GetByIdAsync (int id);

        Task AddDetayAsync (SiparisDetaylari detay);
        Task  DeleteDetayAsync (SiparisDetaylari detay);
        Task  UpdateDetayAsync (SiparisDetaylari detay);
    }
}
