using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Erpyonetimi.Context;
using Erpyonetimi.Models;
using Erpyonetimi.Helpers;
namespace Erpyonetimi.Data
{
    public static class Datalar
    {
        public static void Seed()
        {
            SeedRoller();
            SeedKategoriler();
            SeedDepolar();
            SeedRaflar();
            SeedUserlar();

        }
        private static void SeedUserlar()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());

            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new Users
                    {
                        AdSoyad="İkbal Fırat",
                        KulAd="admin",
                        Sifre=PasswordHelper.HashPassword("12345"),
                        RolId=1
                        
                    },
                    new Users
                    {
                        AdSoyad="fehmi Bamsı",
                        KulAd="Satis",
                        Sifre=PasswordHelper.HashPassword("54321"),
                        RolId=3
                        
                    },
                    new Users
                    {
                        AdSoyad="hayati Hayatsız",
                        KulAd="Personel",
                        Sifre=PasswordHelper.HashPassword("12321"),
                        RolId=2
                    } );
                db.SaveChanges();
            }
        }
        private static void SeedRoller()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());

            if (!db.Roles.Any())
            {
                db.Roles.AddRange(
                    new Roles
                    {
                        RolAdi = "Sistem Yöneticisi"
                    },

                    new Roles
                    {
                        RolAdi="Depo Personeli"
                    },

                    new Roles
                    {
                        RolAdi="Satış Personeli"
                    }
                    
                    );
                db.SaveChanges();
            }
        }


        private static void SeedKategoriler() { 
            var factory = new ErpDbContextFactory();

            using var db = factory.CreateDbContext(Array.Empty<string>());

            if (!db.Kategoriler.Any())
            {
                db.Kategoriler.AddRange(

                    new Kategori
                    {
                        KategoriAdi = "Mekanik"
                    },

                    new Kategori
                    {
                        KategoriAdi = "Elektrik"
                    },
                    new Kategori
                    {
                        KategoriAdi = "Hidrolik"
                    },
                    
                    new Kategori
                    {
                        KategoriAdi="Pnömatik"
                    },

                    new Kategori
                    {
                        KategoriAdi="Bağlantı Elemanları"
                    },
                    new Kategori
                    {
                        KategoriAdi="Rulmanlar"
                    }
                    
                    );

                db.SaveChanges();
        }    }

        private static void SeedRaflar()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());

            if (!db.Raflar.Any())
            {
                db.Raflar.AddRange(
                    new Raflar
                    {
                        DepoId=1,
                        RafKodu = "A-01"
                    },
                    new Raflar
                    {
                        DepoId = 1,
                        RafKodu="A-02"
                    },
                    new Raflar
                    {
                        DepoId=1,
                        RafKodu="A-03"
                    },
                    
                    new Raflar
                    {   DepoId=2,
                        RafKodu="B-01"
                    },
                    new Raflar
                    {   DepoId=2,
                        RafKodu="B-02"
                    });
                db.SaveChanges();
            }
        }
        private static void SeedDepolar()
        {
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            if (!db.Depolar.Any())
            {
                db.Depolar.AddRange(

                    new Depolar
                    {
                        Depaadi = "Ana Depo",
                        Konum="Kat1"
                      


                    },
                    new Depolar
                    {
                        Depaadi="Üretim Deposu",
                        Konum="Üretim Tesisi"
                        
                    }

                    );
                db.SaveChanges();
            }
        }
    }
}
