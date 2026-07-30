using Erpyonetimi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Erpyonetimi.Context
{
    public class ErpDbContext :DbContext
    {

        public ErpDbContext(DbContextOptions<ErpDbContext> options) : base(options) { }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<Kategori> Kategoriler => Set<Kategori>();
        public DbSet<Tedarikci> Tedarikciler => Set<Tedarikci>();
        public DbSet<Depolar> Depolar => Set<Depolar>();
        public DbSet<Raflar> Raflar => Set<Raflar>();
        public DbSet<Parca> Parcalar => Set<Parca>();
        public DbSet<Musteri> Musteriler => Set<Musteri>();
        public DbSet<Siparis> Siparisler => Set<Siparis>();
        public DbSet<SiparisDetaylari> SiparisDetaylari => Set<SiparisDetaylari>();
        public DbSet<StokHareket> StokHareketleri => Set<StokHareket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Users>().HasIndex(u => u.KulAd).IsUnique();
            modelBuilder.Entity<Tedarikci>().HasIndex(t => t.TedarikciKodu).IsUnique();
            modelBuilder.Entity<Musteri>().HasIndex(m => m.MusteriKodu).IsUnique();
            modelBuilder.Entity<Siparis>().HasIndex(s => s.SiparisNo).IsUnique();
            modelBuilder.Entity<Parca>().HasIndex(p => p.ParcaKodu).IsUnique();

            modelBuilder.Entity<Parca>().Property(p => p.AlisFiyat).HasPrecision(18, 2);
            modelBuilder.Entity<Parca>().Property(p => p.SatisFiyat).HasPrecision(18, 2);
            modelBuilder.Entity<Siparis>().Property(s => s.ToplamTutar).HasPrecision(18, 2);
            modelBuilder.Entity<SiparisDetaylari>().Property(d => d.BirimFiyat).HasPrecision(18, 2);
            modelBuilder.Entity<SiparisDetaylari>().Property(d => d.ToplamFiyat).HasPrecision(18, 2);

          
            modelBuilder.Entity<Users>().Property(u => u.KulAd).HasMaxLength(50);
            modelBuilder.Entity<Users>().Property(u => u.AdSoyad).HasMaxLength(100);
            modelBuilder.Entity<Parca>().Property(p => p.ParcaKodu).HasMaxLength(30);


            modelBuilder.Entity<Users>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Kullanicilar)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Raflar>()
                .HasOne(r => r.Depo)
                .WithMany(d => d.Raflar)
                .HasForeignKey(r => r.DepoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parca>()
                .HasOne(p => p.Kategori)
                .WithMany(k => k.Parcalar)
                .HasForeignKey(p => p.KategoriId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parca>()
                .HasOne(p => p.Tedarikci)
                .WithMany(t => t.Parcalar)
                .HasForeignKey(p => p.TedarikciId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Parca>()
                .HasOne(p => p.Raf)
                .WithMany(r => r.Parcalar)
                .HasForeignKey(p => p.RafId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StokHareket>()
                .HasOne(sh => sh.Parca)
                .WithMany(p => p.StokHareketleri)
                .HasForeignKey(sh => sh.ParcaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StokHareket>()
                .HasOne(sh => sh.Kullanici)
                .WithMany(u => u.StokHareketleri)
                .HasForeignKey(sh => sh.KullaniciId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StokHareket>()
                .HasOne(sh => sh.Depo)
                .WithMany(d => d.StokHareketleri)
                .HasForeignKey(sh => sh.DepoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Siparis>()
                .HasOne(s => s.Musteri)
                .WithMany(m => m.Siparisler)
                .HasForeignKey(s => s.MusteriId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SiparisDetaylari>()
                .HasOne(sd => sd.Siparis)
                .WithMany(s => s.SiparisDetaylari)
                .HasForeignKey(sd => sd.SiparisId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SiparisDetaylari>()
                .HasOne(sd => sd.Parca)
                .WithMany(p => p.SiparisDetaylari)
                .HasForeignKey(sd => sd.ParcaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
