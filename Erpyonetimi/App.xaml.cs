using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Context;
using Erpyonetimi.Data;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Nav;
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

        private IHost _host;
        public App()
        {

            _host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
            {
            }).
            ConfigureServices((context, services) =>
            {
                ConfigureServices(services, context.Configuration);
            }).Build();


            //var services = new ServiceCollection();
            //ConfigureServices(services);
            //ServiceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
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
            services.AddTransient<SiparisView>();
            services.AddTransient<SiparisDetayView>();
            services.AddTransient<LogView>();
            services.AddTransient<RaporView>();

            string sqlConn =
    "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;";

            //services.AddDbContext<ErpDbContext>(options =>
            //{
            //    options.UseSqlServer(sqlConn);
            //});
            services.AddDbContextFactory<ErpDbContext>(options =>
            {
                options.UseSqlServer(sqlConn);
            });
            services.AddTransient<ErpDbContext>(sp =>
            {
                var factory = sp.GetRequiredService<IDbContextFactory<ErpDbContext>>();
                return factory.CreateDbContext();
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
            services.AddTransient<ISiparisRepository, SiparisRepository>();
            services.AddTransient<ISiparisService, SiparisService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IRaporRepository, RaporRepository>();
            services.AddTransient<IRaporService, RaporService>();

            services.AddSingleton<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<TedarikciViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<KategoriViewModel>();
            services.AddTransient<DepoViewModel>();
            services.AddTransient<RafViewModel>();
            services.AddTransient<MusteriViewModel>();
            services.AddTransient<SiparisDetayViewModel>();
            services.AddTransient<ParcaViewModel>();
            services.AddTransient<SiparisViewModel>();
            services.AddTransient<RaporViewModel>();
            

            services.AddTransient<UsersYonetimViewModel>();
            services.AddTransient<StokHareketViewModel>();
            services.AddTransient<LogViewModel>();

        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
             
await _host.StartAsync();

                using (var scope = _host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ErpDbContext>();
                    var status = await db.Database.CanConnectAsync();

                    DatabaseHelper.IsConnected = status;
                }
   
                //try
                //{
                //    using var conn = new SqlConnection(
                //        "Server=192.168.5.164;Database=erp;User Id=stajkullanici;Password=ikbal2323!;TrustServerCertificate=True;"
                //    );

                //    conn.Open();

                //    MessageBox.Show("SQL Server bağlantısı başarılı");
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //DatabaseHelper.CheckConnection();
                //if (!DatabaseHelper.IsConnected)
                //{
                //    MessageBox.Show("veritabanına bağlanılamadı.","Veritabanı Bağlantısı",MessageBoxButton.OK,MessageBoxImage.Warning );
                //}
                var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            base.OnStartup(e);

        }






    }
}

