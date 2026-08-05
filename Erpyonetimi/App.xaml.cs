using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Context;
using Erpyonetimi.Data;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.ViewModels;
using Erpyonetimi.Views;
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

            services.AddSingleton<MainWindow>();
            services.AddTransient<DashboardView>();
            services.AddTransient<TedarikciView>();
            services.AddTransient<LoginView>();
            services.AddTransient<ParcaView>();
            services.AddTransient<AdminPanel>();
            services.AddTransient<UsersYonetimView>();


            services.AddDbContext<ErpDbContext>(options=> options.UseSqlServer("Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;"));
            services.AddSingleton<IDashboardService, DashboardService>();
            services.AddSingleton<IParcaRepository, ParcaRepository>();
            services.AddSingleton<IParcaService, ParcaService>();
            services.AddSingleton<IDepoRepository, DepoRepository>();
            services.AddSingleton<IDepoService, DepoService>();
            services.AddSingleton<IRafRepository, RafRepository>();
            services.AddSingleton<IRafService, RafService>();
            services.AddSingleton<IMusteriRepository, MusteriRepository>();
            services.AddSingleton<IMusteriService, MusteriService>();
            services.AddSingleton<ISiparisDetayRepository, SiparisDetayRepository>();
            services.AddSingleton<ISiparisDetayService, SiparisDetayService>();
            services.AddSingleton<IStokHareketRepository, StokHareketRepository>();
            services.AddSingleton<IStokHareketService, StokHareketService>();
            
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<TedarikciViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<KategoriViewModel>();
            services.AddTransient<KategoriYonetimViewModel>();
            services.AddTransient<ParcaViewModel>();
            services.AddTransient<ParcaYonetimViewModel>();
            services.AddTransient<TedarikciViewModel>();
            services.AddTransient<TedarikciYonetimViewmodel>();
 
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
