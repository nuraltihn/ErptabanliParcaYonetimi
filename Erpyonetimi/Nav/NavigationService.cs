using Erpyonetimi.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.ViewModels;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
namespace Erpyonetimi.Nav
{

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private object _currentView;
        public object CurrentView => _currentView;
        public event EventHandler CurrentViewChanged;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Navigate(Pages page)
        {
            switch (page)
            {
                case Pages.dashboard:
                    _currentView = _serviceProvider
                        .GetRequiredService<DashboardViewModel>();
                    break;

            case Pages.musteriler:
                    _currentView = _serviceProvider
                        .GetRequiredService<MusteriViewModel>();
                    break;

                case Pages.siparisler:
                    _currentView = _serviceProvider
                        .GetRequiredService<SiparisViewModel>();
                    break;

                case Pages.tedarikciler:
                    _currentView = _serviceProvider
                        .GetRequiredService<TedarikciViewModel>();
                    break;

                case Pages.kullanicilar:
                    _currentView = _serviceProvider
                        .GetRequiredService<UsersYonetimViewModel>();
                    break;

                case Pages.parcalar:
                    _currentView = _serviceProvider
                        .GetRequiredService<ParcaViewModel>();
                    break;

                case Pages.depolar:
                    _currentView = _serviceProvider
                        .GetRequiredService<DepoViewModel>();
                    break;

                case Pages.siparisdetaylari:
                    _currentView = _serviceProvider
                        .GetRequiredService<SiparisDetayViewModel>();
                    break;
                case Pages.raflar:
                    _currentView = _serviceProvider
                        .GetRequiredService<RafViewModel>();
                    break;
                case Pages.kategoriler:
                    _currentView = _serviceProvider
                        .GetRequiredService<KategoriViewModel>();
                    break;
                case Pages.stokhareketleri:
                    _currentView = _serviceProvider
                        .GetRequiredService<StokHareketViewModel>();
                    break;

                         case Pages.login:
                    _currentView = _serviceProvider
                        .GetRequiredService<LoginViewModel>();
                    break;
                case Pages.loglar:
                    _currentView = _serviceProvider.GetRequiredService<LogViewModel>();
                    break;

                case Pages.raporlar:
                    _currentView = _serviceProvider.GetRequiredService<RaporViewModel>();
                    break;

            }
            CurrentViewChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
