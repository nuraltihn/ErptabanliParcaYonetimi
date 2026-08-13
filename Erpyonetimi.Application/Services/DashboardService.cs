using Erpyonetimi.Context;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Application.Services
{
     public class DashboardService : IDashboardService
    {
        private readonly ErpDbContext _context;
        public DashboardService(ErpDbContext context)
        {
            _context = context;
        }
        public int GetToplamKullanici()
        {
            return _context.Users.Count();
        }
        public int GetToplamParca()
        {
            return _context.Parcalar.Count();
        }
        public int GetToplamKategori()
        {
            return _context.Kategoriler.Count();
        }
        public int GetToplamTedarikci()
        {
            return _context.Tedarikciler.Count();
        }
        public int GetKritikStokSayisi()
        {
            return _context.Parcalar.Count(u => u.MevcutStok < u.MinimumStok);
        }
        public List<Users> GetSonKullanicilar(int adet)
        {
            return _context.Users.OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToList();
        }
        public List<Parca> GetSonParcalar(int adet)
        {
            return _context.Parcalar.OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToList();
        }
        public List<Siparis> GetSonSiparisler(int adet)
        {
            return _context.Siparisler
                .OrderByDescending(u => u.OlusturmaTarih)
                .Take(adet)
                .ToList();
        }
        public int GetToplamMusteri()
        {
            return _context.Musteriler.Count();
        }
        public int GetToplamSiparis()
        {
            return _context.Siparisler.Count();
        }

    }
}
