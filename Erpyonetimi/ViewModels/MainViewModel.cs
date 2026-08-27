using Erpyonetimi.Commands;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Erpyonetimi.Nav;


namespace Erpyonetimi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Visibility Databasebaglivisibility =>
            DatabaseHelper.IsConnected
            ? Visibility.Collapsed
            : Visibility.Visible;

        private object _currentView = null;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sidemenugorunme));
                OnPropertyChanged(nameof(Sidemenugenisligi));
            }
        }
        
        public Visibility Sidemenugorunme
        {
            get
            {
                return CurrentView is LoginViewModel ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        public GridLength Sidemenugenisligi
        {
            get
            {
                return CurrentView is LoginViewModel ? new GridLength(0)
                    : new GridLength(230);
            }
        }
        public bool UserPanelgor
        {
            get
            {
                return UserSession.IsAdmin;
            }
        }

        public string KullaniciAdiSoyadi
        {
            get
            {
                return UserSession.CurrentUser?.AdSoyad ?? "Kullanıcı";
            }
        }
        public string KullaniciAvatar
        {
            get
            {
                var adsoyad = UserSession.CurrentUser?.AdSoyad;
                if (string.IsNullOrWhiteSpace(adsoyad))
                    return "U";
                var kelimeler = adsoyad.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (kelimeler.Length == 1)
                    return kelimeler[0][0].ToString().ToUpper();
                return $"{kelimeler[0][0]}{kelimeler[^1][0]}".ToUpper();
            }
        }
        public string KullaniciRol
        {
            get
            {
                return UserSession.CurrentUser?.Rol?.RolAdi ?? "Kullanıcı";
            }
        }

        public ICommand UserYonCommand { get; }
        public ICommand DashboardCommand { get; }
        public ICommand TedarikciCommand { get; }
        public ICommand ParcaCommand { get; }
        public ICommand KategoriCommand { get; }

        //private readonly IAuthService _authService;
        public ICommand RafCommand { get; }
        public ICommand StokHCommand { get; }
        public ICommand DepoCommand { get; }
        public ICommand MusteriCommand { get; }
        public ICommand SiparisCommand { get; }
        public ICommand SiparisDetayCommand { get; }
        public ICommand CikisCommand { get; }
        public ICommand LogCommand { get; }
        public ICommand RaporCommand { get; }
        public ICommand YenidenbaglanCommand { get; }

        private readonly INavigationService _navigationService;
        public MainViewModel(Erpyonetimi.Nav.INavigationService navigationService)

        {
            _navigationService = navigationService;
            _navigationService.CurrentViewChanged += NavigationService_CurrentViewChanged;

           
            //var auth = _serviceProvider.GetRequiredService<IAuthService>();
            
            if(UserSession.CurrentUser == null)
            {
                _navigationService.Navigate(Pages.login);
            }
        
            UserYonCommand = new RelayCommand(OpenUserPanel);
            DashboardCommand = new RelayCommand(OpenDashboard);
            TedarikciCommand = new RelayCommand(OpenTedarikci);
            ParcaCommand = new RelayCommand(OpenParca);
            KategoriCommand = new RelayCommand(OpenKat);
            StokHCommand = new RelayCommand(OpenStok);
            DepoCommand = new RelayCommand(OpenDepo);
            RafCommand = new RelayCommand(OpenRaf);
            MusteriCommand = new RelayCommand(OpenMusteri);
            SiparisCommand = new RelayCommand(OpenSiparis);
            SiparisDetayCommand = new RelayCommand(OpenSiparisDetay);
            CikisCommand = new RelayCommand(Cikis);
            YenidenbaglanCommand = new RelayCommand(YenidenBaglan);
            LogCommand = new RelayCommand(Loggir);
            RaporCommand = new RelayCommand(Rapors);
            
        }
        private void NavigationService_CurrentViewChanged(object sender, EventArgs e)
        {
            CurrentView = _navigationService.CurrentView;

            OnPropertyChanged(nameof(UserPanelgor));
            OnPropertyChanged(nameof(UserPanelVisibility));
            OnPropertyChanged(nameof(AdminVisibility));
            OnPropertyChanged(nameof(DepoVisibility));
            OnPropertyChanged(nameof(SatisVisibility));
            OnPropertyChanged(nameof(KullaniciAdiSoyadi));
            OnPropertyChanged(nameof(KullaniciRol));
            OnPropertyChanged(nameof(KullaniciAvatar));

        }
        public void NavigatePages(Pages page)
        {
            _navigationService.Navigate(page);
        }
        public void KullaniciBilgileriniGuncelle()
        {
            
             

            OnPropertyChanged(nameof(UserPanelgor));
            OnPropertyChanged(nameof(UserPanelVisibility));
            OnPropertyChanged(nameof(AdminVisibility));
            OnPropertyChanged(nameof(DepoVisibility));
            OnPropertyChanged(nameof(SatisVisibility));
            OnPropertyChanged(nameof(KullaniciAdiSoyadi));
            OnPropertyChanged(nameof(KullaniciRol));
            OnPropertyChanged(nameof(KullaniciAvatar)); 
            
            
        }
        private void Cikis()
        {
            UserSession.CurrentUser = null;
            //CurrentView = new LoginViewModel(this, _authService);
            OnPropertyChanged(nameof(UserPanelgor));
            OnPropertyChanged(nameof(UserPanelVisibility));
            OnPropertyChanged(nameof(AdminVisibility));
            OnPropertyChanged(nameof(DepoVisibility));
            OnPropertyChanged(nameof(SatisVisibility));
            OnPropertyChanged(nameof(KullaniciAdiSoyadi));
            OnPropertyChanged(nameof(KullaniciRol));
            OnPropertyChanged(nameof(KullaniciAvatar));
            NavigatePages(Pages.login);
        }
        private void YenidenBaglan()
        {
            DatabaseHelper.CheckConnection();
            OnPropertyChanged(nameof(Databasebaglivisibility));
            OnPropertyChanged(nameof(UserPanelVisibility));
            if (DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı başarılı.");
            }
            else
            {
                MessageBox.Show("Veritabanına bağlanılamadı.");
            }
        }
        private void OpenSiparisDetay()
        {
            NavigatePages(Pages.siparisdetaylari);
        }
        private void Rapors()
        {
            NavigatePages(Pages.raporlar);
        }
        private void Loggir()
        {
            NavigatePages(Pages.loglar);
        }
        private void OpenSiparis()
        {
            NavigatePages(Pages.siparisler);
        }
        private void OpenMusteri()
        {
            NavigatePages(Pages.musteriler);
        }
        private void OpenRaf()
        {
            NavigatePages(Pages.raflar);
        }
        private void OpenDepo()
        {
            NavigatePages(Pages.depolar);
        }
        private void OpenStok()
        {
            NavigatePages(Pages.stokhareketleri);
        }
        private void OpenParca()
        {
            NavigatePages(Pages.parcalar);
        }
        private void OpenTedarikci()
        {
            NavigatePages(Pages.tedarikciler);
        }
        private void OpenDashboard()
        {
            NavigatePages(Pages.dashboard);
        }
        private void OpenKat()
        {
            NavigatePages(Pages.kategoriler);
        }
        private void OpenUserPanel()
        {

            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Erişim yetkiniz yok");
                return;
            }
            NavigatePages(Pages.kullanicilar);
        }
        public Visibility UserPanelVisibility
        {
            get
            {
                return UserSession.IsAdmin && DatabaseHelper.IsConnected
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

        }

        public Visibility AdminVisibility =>
            UserSession.CurrentUser?.Rol?.RolAdi == "Sistem Yöneticisi"
            ? Visibility.Visible : Visibility.Collapsed;
        public Visibility DepoVisibility =>
            UserSession.CurrentUser?.Rol?.RolAdi == "Sistem Yöneticisi" ||
            UserSession.CurrentUser?.Rol?.RolAdi == "Depo Personeli"
            ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SatisVisibility =>
            UserSession.CurrentUser?.Rol?.RolAdi == "Sistem Yöneticisi" ||
            UserSession.CurrentUser?.Rol?.RolAdi == "Satış Personeli"
            ? Visibility.Visible : Visibility.Collapsed;

      



        }

    }


