using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Raflar : Baseseyler
    {
        public int DepoId { get; set; }
        public string RafKodu { get; set; } = string.Empty;
        public Depolar? Depo { get; set; }
        public ICollection<Parca> Parcalar { get; set; } = new List<Parca>();
    }
}
