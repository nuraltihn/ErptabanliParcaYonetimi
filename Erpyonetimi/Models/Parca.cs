using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Parca :Baseseyler
    {
        public string? ParcaKodu { get; set; }
        public string ParcAdi { get; set; } = string.Empty;
        public int KategoriId { get; set; }
        public int TedarikciId { get; set; }
        public string? Marka { get; set; }
        public string? Model { get; set; }
        public string? Malzeme { get; set; }
        public string? Agirlik { get; set; }
        public double? Uzunluk { get; set; }
        public double? Genislik { get; set; }
        public double? Yukseklik { get; set; }
        public string? Renk { get; set; }
        public string? Birim { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal SatisFiyat { get; set; }
        public int MevcutStok { get; set; }
        public int MinimumStok { get; set; }

        public int? RafId { get; set; }

        public string? Aciklama { get; set; }

        public Kategori? Kategori { get; set; }
        public Tedarikci? Tedarikci { get; set; }
        public Raflar? Raf { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<StokHareket> StokHareketleri { get; set; } = new List<StokHareket>();
        public ICollection<SiparisDetaylari> SiparisDetaylari { get; set; } = new List<SiparisDetaylari>();



    }
}
