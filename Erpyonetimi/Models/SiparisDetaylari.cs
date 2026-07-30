using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Models
{
    public class SiparisDetaylari: Baseseyler
    {

        public int SiparisId { get; set; }
        public int ParcaId { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal ToplamFiyat { get; set; }


        public Siparis? Siparis { get; set; }
        public Parca? Parca { get; set; }
    }
}
