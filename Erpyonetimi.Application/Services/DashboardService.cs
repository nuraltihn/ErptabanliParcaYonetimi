
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Erpyonetimi.Context;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Helpers;

namespace Erpyonetimi.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ErpDbContext _context;
        public DashboardService(ErpDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetToplamKullaniciAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetToplamParcaAsync()
        {
            return await _context.Parcalar.CountAsync();
        }

        public async Task<int> GetToplamKategoriAsync()
        {
            return await _context.Kategoriler.CountAsync();
        }

        public async Task<int> GetToplamTedarikciAsync()
        {
            return await _context.Tedarikciler.CountAsync();
        }

        public async Task<int> GetKritikStokSayisiAsync()
        {
            return await _context.Parcalar
                .CountAsync(u => u.MevcutStok < u.MinimumStok);
        }

        public async Task<List<Users>> GetSonKullanicilarAsync(int adet)
        {
            return await _context.Users
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<List<Parca>> GetSonParcalarAsync(int adet)
        {
            return await _context.Parcalar
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<List<Siparis>> GetSonSiparislerAsync(int adet)
        {
            return await _context.Siparisler
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<int> GetToplamMusteriAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return 0;

            return await _context.Musteriler.CountAsync();
        }

        public async Task<int> GetToplamSiparisAsync()
        {
            return await _context.Siparisler.CountAsync();
        }
    }
}