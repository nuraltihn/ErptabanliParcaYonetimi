using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Domain.Entities;

public class StokHareket : Baseseyler
{
    public int ParcaId { get; set; }
    public int? KullaniciId { get; set; }
    public int DepoId { get; set; }
    public string IslemTipi { get; set; } = string.Empty;
    public int Miktar { get; set; }
    public DateTime Tarih { get; set; } = DateTime.Now;
    public string? Aciklama { get; set; }
    public Parca? Parca { get; set; }
    public Users? Kullanici { get; set; }
    public Depolar? Depo { get; set; }

}
