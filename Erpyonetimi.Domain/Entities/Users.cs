using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Domain.Entities;

public class Users :Baseseyler
{
    public string AdSoyad { get; set; } = string.Empty;
    public string KulAd { get; set; } = string.Empty;
    public string Sifre { get; set; } = string.Empty; 
   
    public int? RolId { get; set; }

    public string? Tel { get; set; }
    public string? Email { get; set; }

    public Roles? Rol { get; set; }
    public ICollection<StokHareket> StokHareketleri { get; set; } = new List<StokHareket>();
}
