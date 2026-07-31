using Erpyonetimi.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Services
{
     public class DashboardService
    {
        public int Tedarikcisayial()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());

            return db.Tedarikciler.Count();
        }

        public int Parcasayisial()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            return db.Parcalar.Count();
        }

        public int Musterisayial()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            return db.Musteriler.Count();
        }

        public int Siparissayiisial()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            return db.Siparisler.Count();
        }
    }
}
