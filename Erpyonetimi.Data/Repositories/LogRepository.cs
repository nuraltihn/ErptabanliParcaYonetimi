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

        public void Add(Log log)
        {
            if(log.KullaniciId.HasValue)
            {
                var kullanici = _context.Users
                    .FirstOrDefault(x => x.Id == log.KullaniciId.Value);

                if (kullanici!= null)
                {
                    log.KullaniciAdSoyad = kullanici.AdSoyad;
                }
            }
            _context.Loglar.Add(log);
            _context.SaveChanges();
        }

        public List<Log> GetAll()
        {
            return _context.Loglar
                .Include(x => x.Kullanici)
                .OrderByDescending(x => x.Tarih)
                .ToList();
        }
    }
}