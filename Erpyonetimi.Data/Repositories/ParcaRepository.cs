using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
namespace Erpyonetimi.Data.Repositories
{
    public class ParcaRepository : IParcaRepository
    {
        private readonly ErpDbContext _context;
        public ParcaRepository(ErpDbContext context)
        {
            _context = context;
        }
        public void Add(Parca parca)
        {
            _context.Parcalar.Add(parca);
            _context.SaveChanges();
        }

        public void Delete(Parca parca)
        {
            var dbParca = _context.Parcalar
                .Include(p => p.StokHareketleri)
                .Include(p => p.SiparisDetaylari)
                .FirstOrDefault(p => p.Id == parca.Id);

            if (dbParca == null)
            {
                throw new Exception("Parça bulunamadı.");
            }

            if (dbParca.StokHareketleri.Any())
            {
                throw new Exception(
                    "Bu parça silinemez. Parçaya bağlı stok hareketleri bulunmaktadır.");
            }

            if (dbParca.SiparisDetaylari.Any())
            {
                throw new Exception(
                    "Bu parça silinemez. Parçaya bağlı sipariş detayları bulunmaktadır.");
            }
            _context.Parcalar.Remove(dbParca);
            _context.SaveChanges();
        }
        public List<Parca> GetAll()
        {
         if(!DatabaseHelper.IsConnected)
                return new List<Parca>();
            return _context.Parcalar
                 .Include(x => x.Kategori)
                 .Include(x => x.Tedarikci)
                 .Include(x => x.Raf)
                 .ToList();
            
        }

        public Parca? GetById(int id)
        {
            return _context.Parcalar.FirstOrDefault(x => x.Id == id);
        }

        public Parca? GetByKod(string parcaKodu)
        {
            return _context.Parcalar.FirstOrDefault(x => x.ParcaKodu == parcaKodu);
        }

        public void Update(Parca parca)
        {
            var eskiParca = _context.Parcalar
                .FirstOrDefault(x => x.Id == parca.Id);

            if (eskiParca != null)
            {
                eskiParca.ParcaKodu = parca.ParcaKodu;
                eskiParca.ParcAdi = parca.ParcAdi;
                eskiParca.KategoriId = parca.KategoriId;
                eskiParca.TedarikciId = parca.TedarikciId;
                eskiParca.Marka = parca.Marka;
                eskiParca.Model = parca.Model;
                eskiParca.Malzeme = parca.Malzeme;
                eskiParca.Agirlik = parca.Agirlik;
                eskiParca.Uzunluk = parca.Uzunluk;
                eskiParca.Genislik = parca.Genislik;
                eskiParca.Yukseklik = parca.Yukseklik;
                eskiParca.Renk = parca.Renk;
                eskiParca.Birim = parca.Birim;
                eskiParca.AlisFiyat = parca.AlisFiyat;
                eskiParca.SatisFiyat = parca.SatisFiyat;
                eskiParca.MevcutStok = parca.MevcutStok;
                eskiParca.MinimumStok = parca.MinimumStok;
                eskiParca.RafId = parca.RafId;
                eskiParca.Aciklama = parca.Aciklama;

                _context.SaveChanges();
            }
        }
    }
}
