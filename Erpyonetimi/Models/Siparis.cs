using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class Siparis:Baseseyler
    {
        
        public string SiparisNo { get; set; } = string.Empty;
        public int MusteriId { get; set; }
        public DateTime SiparisTarihi { get; set; } = DateTime.Now;
        public decimal ToplamTutar { get; set; }
        public string? Durum { get; set; } 

        public Musteri? Musteri { get; set; }
        public ICollection<SiparisDetaylari> SiparisDetaylari { get; set; } = new List<SiparisDetaylari>();
    }
}
