using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Context;
using Erpyonetimi.Data;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.ViewModels;
using Erpyonetimi.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Erpyonetimi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        //public static IServiceProvider ServiceProvider { get; private set; }

        private   IHost _host;
        public App()
        {

            _host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context,config)=> {
            }).
            ConfigureServices((context, services) =>
            {
                ConfigureServices(services,context.Configuration);
            }).Build();

            //var services = new ServiceCollection();
            //ConfigureServices(services);
            //ServiceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {

            services.AddTransient<MainWindow>();
            services.AddTransient<DashboardView>();
            services.AddTransient<TedarikciView>();
            services.AddTransient<LoginView>();
            services.AddTransient<ParcaView>();
            services.AddTransient<AdminPanel>();
            services.AddTransient<UsersYonetimView>();
            services.AddTransient<KategoriView>();
            services.AddTransient<StokHareketView>();
            services.AddTransient<DepoView>();
            services.AddTransient<RafView>();
            services.AddTransient<MusteriView>();

            string sqlConn =
    "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;";

            services.AddDbContext<ErpDbContext>(options =>
            {
                try
                {
                    using var conn = new SqlConnection(sqlConn);
                    conn.Open();

                    options.UseSqlServer(sqlConn);
                }
                catch
                {
                    options.UseSqlite("Data Source=erp.db");
                }
            });
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IParcaRepository, ParcaRepository>();
            services.AddTransient<IParcaService, ParcaService>();
            services.AddTransient<IDepoRepository, DepoRepository>();
            services.AddTransient<IDepoService, DepoService>();
            services.AddTransient<IRafRepository, RafRepository>();
            services.AddTransient<IRafService, RafService>();
            services.AddTransient<IMusteriRepository, MusteriRepository>();
            services.AddTransient<IMusteriService, MusteriService>();
            services.AddTransient<ISiparisDetayRepository, SiparisDetayRepository>();
            services.AddTransient<ISiparisDetayService, SiparisDetayService>();
            services.AddTransient<IStokHareketRepository, StokHareketRepository>();
            services.AddTransient<IStokHareketService, StokHareketService>();
            services.AddTransient<ITedarikciRepository, TedarikciRepository>();
            services.AddTransient<ITedarikciService, TedarikciService>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IKategoriRepository, KategoriRepository>();
            services.AddTransient<IKategoriService, KategoriService>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<TedarikciViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<KategoriViewModel>();
            services.AddTransient<DepoViewModel>();
            services.AddTransient<RafViewModel>();
            services.AddTransient<MusteriViewModel>();
            
            services.AddTransient<ParcaViewModel>();
           
            services.AddTransient<TedarikciViewModel>();
       
            services.AddTransient<UsersYonetimViewModel>();
            services.AddTransient<StokHareketViewModel>();
            
 
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();


            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
            

            //Datalar.Seed();
         
        }
    }

}
