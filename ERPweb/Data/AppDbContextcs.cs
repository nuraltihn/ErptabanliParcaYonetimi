using Microsoft.EntityFrameworkCore;
using System.Data;
using ERPweb.Data;
namespace ERPweb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet <Depo> Depolar { get; set; }
        //public DbSet<Kategori> Kategoriler { get; set; }
        //public DbSet<Müşteri> Müşteriler { get; set; }
        //public DbSet<Parça> Parçalar{ get; set; }
        //public DbSet<Raf> Raflar { get; set; }
        //public DbSet<Siparis> Siparisler { get; set; }
        //public DbSet<SiparisDetay> SiparisDetaylari { get; set; }
        //public DbSet<StokHareket> StokHareketleri { get; set; }
        //public DbSet<Tedarikci> Tedarikciler { get; set; }



    }
}