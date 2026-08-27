
using System.Collections.Generic;
using System.Linq;
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
        private readonly IDbContextFactory<ErpDbContext> _contextFactory;
        public DashboardService(IDbContextFactory<ErpDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> GetToplamKullaniciAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users.CountAsync();
        }

        public async Task<int> GetToplamParcaAsync()
        {
            using var context  = await _contextFactory.CreateDbContextAsync();
            return await context.Parcalar.CountAsync();
        }

        public async Task<int> GetToplamKategoriAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Kategoriler.CountAsync();
        }

        public async Task<int> GetToplamTedarikciAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Tedarikciler.CountAsync();
        }

        public async Task<int> GetKritikStokSayisiAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Parcalar
                .CountAsync(u => u.MevcutStok < u.MinimumStok);
        }

        public async Task<List<Users>> GetSonKullanicilarAsync(int adet)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
               
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<List<Parca>> GetSonParcalarAsync(int adet)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Parcalar
             
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<List<Siparis>> GetSonSiparislerAsync(int adet)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Siparisler
                
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToListAsync();
        }

        public async Task<int> GetToplamMusteriAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return 0;
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Musteriler.CountAsync();
        }

        public async Task<int> GetToplamSiparisAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Siparisler.CountAsync();
        }
    }
}