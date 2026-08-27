using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Context;
using Microsoft.EntityFrameworkCore;

namespace Erpyonetimi.Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ErpDbContext _context;

        public LogRepository(ErpDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Log log)
        {
            if(log.KullaniciId.HasValue)
            {
                var kullanici = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == log.KullaniciId.Value);

                if (kullanici!= null)
                {
                    log.KullaniciAdSoyad = kullanici.AdSoyad;
                }
            }
           await _context.Loglar.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Log>> GetAllAsync()
        {
            return await _context.Loglar
                .AsNoTracking()
                .Include(x => x.Kullanici)
                .OrderByDescending(x => x.Tarih)
                .ToListAsync();
        }
    }
}