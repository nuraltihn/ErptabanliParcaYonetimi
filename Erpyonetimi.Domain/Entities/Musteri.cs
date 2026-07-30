using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Domain.Entities;

    public class Musteri : Baseseyler
{
    public string MusteriKodu { get; set; } = string.Empty;
    public string FirmaAdi { get; set; } = string.Empty;
    public string YetkiliKisi { get; set; } = string.Empty;
    public string? Ad { get; set; }
    public string? Soyad { get; set; }
    public string Adres { get; set; } = string.Empty;
    public string? Sehir { get; set; }
    public string? Tel { get; set; }
    public string? Email { get; set; }
    public string? VergiNo { get; set; }
    public string? Fax { get; set; }

    public ICollection<Siparis> Siparisler { get; set; } = new List<Siparis>();
}
