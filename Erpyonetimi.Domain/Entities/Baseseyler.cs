using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Domain.Entities
{
    public class Baseseyler
    {
        public int Id { get; set; }
        public bool Aktifmi { get; set; } = true;
        public DateTime OlusturmaTarih { get; set; } = DateTime.Now;
    }
}
