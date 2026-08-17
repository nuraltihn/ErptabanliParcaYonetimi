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

namespace Erpyonetimi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Visibility Databasebaglivisibility=>
            DatabaseHelper.IsConnected
            ? Visibility.Visible
            :Visibility.Collapsed;

        private object _currentView=null;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView= value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sidemenugorunme));
                OnPropertyChanged(nameof(Sidemenugenisligi));
            }
        }
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly TedarikciViewModel _tedarikciViewModel;
        private readonly ParcaViewModel _parcaViewModel;
        private readonly UsersYonetimViewModel _usersYonetimViewModel;
        private readonly KategoriViewModel _kategoriViewModel;
        private readonly StokHareketViewModel _stokHareketViewModel;
        private readonly DepoViewModel _depoViewModel;
        private readonly RafViewModel _rafViewModel;
        private readonly MusteriViewModel _musteriViewModel;
        private readonly SiparisViewModel _siparisViewModel;
        private readonly SiparisDetayViewModel _siparisDetayViewModel;
        public Visibility Sidemenugorunme
        {
            get
            {
                return CurrentView is LoginViewModel? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        public GridLength Sidemenugenisligi
        {
            get
            {
                return CurrentView is LoginViewModel? new GridLength(0)
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
                var kelimeler = adsoyad.Split(' ',StringSplitOptions.RemoveEmptyEntries);
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
      
        public ICommand RafCommand { get; }
        public ICommand StokHCommand { get; }
        public ICommand DepoCommand { get; }
        public ICommand MusteriCommand { get; }
        public ICommand SiparisCommand { get; }
        public ICommand SiparisDetayCommand { get; }
        public ICommand CikisCommand { get; }
        public MainViewModel( DashboardViewModel dashboardViewModel, TedarikciViewModel tedarikciViewModel,
            ParcaViewModel parcaViewModel, UsersYonetimViewModel usersYonetimViewModel, KategoriViewModel kategoriViewModel, StokHareketViewModel stokHareketViewModel,
            DepoViewModel depoViewModel, RafViewModel rafViewModel, MusteriViewModel musteriViewModel, SiparisViewModel siparisViewModel, SiparisDetayViewModel siparisDetayViewModel)
        {
           
            _usersYonetimViewModel = usersYonetimViewModel;
            _dashboardViewModel = dashboardViewModel;
            _tedarikciViewModel= tedarikciViewModel;
            _parcaViewModel = parcaViewModel;
            _kategoriViewModel = kategoriViewModel;
            _stokHareketViewModel = stokHareketViewModel;
            _depoViewModel = depoViewModel;
            _rafViewModel = rafViewModel;
            _musteriViewModel = musteriViewModel;
            _siparisViewModel = siparisViewModel;
            _siparisDetayViewModel = siparisDetayViewModel;
            CurrentView = new LoginViewModel(this);
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
        }

        public void Kullanicigirisyapti(Users users)
        {
            UserSession.CurrentUser = users;
          

            CurrentView = _dashboardViewModel;

           

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
            System.Windows.Application.Current.Shutdown();
        }
        private void OpenSiparisDetay()
        {
            CurrentView = _siparisDetayViewModel;
        }
        

        private void OpenSiparis()
        {
            CurrentView = _siparisViewModel;
        }
      private void OpenMusteri()
        {
            CurrentView = _musteriViewModel;
        }
        private void OpenRaf()
        {
            CurrentView= _rafViewModel;
        }
        private void OpenDepo()
        {
            CurrentView = _depoViewModel;
        }
        private void OpenStok()
        {
            CurrentView = _stokHareketViewModel;
        }
        private void OpenParca()
        {
            CurrentView = _parcaViewModel;
        }
        private void OpenTedarikci()
        {
            CurrentView = _tedarikciViewModel;
        }
        private void OpenDashboard()
        {
            CurrentView = _dashboardViewModel;
        }
        private void OpenKat()
        {
            CurrentView = _kategoriViewModel;
        }
        private void OpenUserPanel()
        {
           
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Erişim yetkiniz yok");
                return;
            }
            CurrentView = new UsersYonetimViewModel();
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

        public Visibility AdminVisibility=>
            UserSession.CurrentUser?.Rol?.RolAdi=="Sistem Yöneticisi"
            ? Visibility.Visible : Visibility.Collapsed;
        public Visibility DepoVisibility =>
            UserSession.CurrentUser?.Rol?.RolAdi=="Sistem Yöneticisi"||
            UserSession.CurrentUser?.Rol?.RolAdi == "Depo Personeli"
            ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SatisVisibility=>
            UserSession.CurrentUser?.Rol?.RolAdi == "Sistem Yöneticisi"||
            UserSession.CurrentUser?.Rol?.RolAdi == "Satış Personeli"
            ? Visibility.Visible : Visibility.Collapsed;



    }
}
