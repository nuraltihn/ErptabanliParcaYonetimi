using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Tedarikci : Baseseyler
    {
        public string TedarikciKodu { get; set; } = string.Empty;
        public string FirmaAdi { get; set; } = string.Empty;
        public string? YetkiliKisi { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
        public string? Fax { get; set; }
        public string? VergiNo { get; set; }

        public ICollection<Parca> Parcalar { get; set; } = new List<Parca>();
    }
}
