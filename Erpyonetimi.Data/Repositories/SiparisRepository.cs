using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Erpyonetimi.Data.Repositories
{
    public class SiparisRepository : ISiparisRepository
    {
        private readonly ErpDbContext _context;
        public SiparisRepository(ErpDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Siparis siparis)
        {
            await _context.Siparisler.AddAsync(siparis);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync (Siparis siparis)
        {
            var mevcut = await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                .FirstOrDefaultAsync(s => s.Id == siparis.Id);

            if (mevcut == null)
            {
                throw new Exception("Sipariş bulunamadı.");
            }

            if (mevcut.SiparisDetaylari.Any())
            {
                throw new Exception(
                    "Bu sipariş silinemez. Siparişe bağlı parçalar bulunmaktadır.");
            }

            if (mevcut.Durum == "Tamamlandı")
            {
                throw new Exception(
                    "Tamamlanmış siparişler silinemez.");
            }

            _context.Siparisler.Remove(mevcut);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Siparis>> GetAllAsync()
        {
            if (!DatabaseHelper.IsConnected)
                return new List<Siparis>();
                return await _context.Siparisler.Include(s => s.Musteri)
                .Include(s=>s.SiparisDetaylari)
                .ThenInclude(sd=>sd.Parca)
                .AsNoTracking()
                .ToListAsync();
          
        }

        public async Task<Siparis?> GetByIdAsync(int id)
        {
            return await _context.Siparisler
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Siparis?> GetByNoAsync(string siparisNo)
        {
            return await _context.Siparisler
                .FirstOrDefaultAsync(x=>x.SiparisNo == siparisNo);
        }

        public async Task UpdateAsync(Siparis siparis)
        {
            var mevcut = await _context.Siparisler.FindAsync(siparis.Id);
            if (mevcut !=null)
            {
                mevcut.SiparisNo = siparis.SiparisNo;
                mevcut.MusteriId = siparis.MusteriId;
                mevcut.SiparisTarihi = siparis.SiparisTarihi;
                mevcut.ToplamTutar = siparis.ToplamTutar;
                mevcut.Durum = siparis.Durum;

                await _context.SaveChangesAsync();
            }
        }
    }
}
