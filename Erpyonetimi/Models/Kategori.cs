using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Kategori :Baseseyler
    {
        public string KategoriAdi { get; set; } = string.Empty;
        public string? Aciklama { get; set; }

        public ICollection<Parca> Parcalar { get; set; } = new List<Parca>();
    }
}
