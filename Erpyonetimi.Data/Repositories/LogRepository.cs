using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Context;
using Microsoft.EntityFrameworkCore;

namespace Erpyonetimi.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IDbContextFactory<ErpDbContext> _contextFactory;
        public LogRepository(IDbContextFactory<ErpDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(Log log)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            if (log.KullaniciId.HasValue)
            {
                var kullanici = await context.Users
                    .FirstOrDefaultAsync(x => x.Id == log.KullaniciId.Value);

                if (kullanici != null)
                {
                    log.KullaniciAdSoyad = kullanici.AdSoyad;
                }
            }

            await context.Loglar.AddAsync(log);
            await context.SaveChangesAsync();
        }

        public async Task<List<Log>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Loglar
                .AsNoTracking()
                .Include(x => x.Kullanici)
                .OrderByDescending(x => x.Tarih)
                .ToListAsync();
        }

        public async Task<List<Log>> GetByDateRangeAsync(
            DateTime baslangicTarihi,
            DateTime bitisTarihi)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            return await context.Loglar
                .AsNoTracking()
                .Where(x => x.Tarih >= baslangicTarihi &&
                            x.Tarih < bitisTarihi.AddDays(1))
                .OrderByDescending(x => x.Tarih)
                .ToListAsync();
        }
           
        }
    }
