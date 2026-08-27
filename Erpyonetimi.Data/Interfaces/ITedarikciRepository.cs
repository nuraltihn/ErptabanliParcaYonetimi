using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Erpyonetimi.Data.Interfaces
{
    public interface ITedarikciRepository
    {
        Task<List<Tedarikci>> GetAllAsync();
        Task<Tedarikci?> GetByIdAsync(int id);
        Task<Tedarikci?> GetByIdWithIliskilerAsync(int id);
        Task<Tedarikci?> GetByKodAsync(string kod);
        Task AddAsync(Tedarikci tedarikci);
        Task UpdateAsync(Tedarikci tedarikci);
        Task DeleteAsync(Tedarikci tedarikci);
    }
}
