using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data;
using Erpyonetimi.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDashboardService, DashboardService>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<DashboardViewModel>();

            services.AddTransient<MainWindow>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            

            Datalar.Seed();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
