using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Depolar : Baseseyler
    {
        public string Depaadi { get; set; } = string.Empty;
        public string Konum { get; set; } = string.Empty;

        public ICollection<Raflar> Raflar { get; set; } = new List<Raflar>();
        public ICollection<StokHareket> StokHareketleri { get; set; } = new List<StokHareket>();

    }
}
