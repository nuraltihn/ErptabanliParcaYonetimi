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

      
        public ICommand UserYonCommand { get; }
        public ICommand DashboardCommand { get; }
        public ICommand TedarikciCommand { get; }
        public ICommand ParcaCommand { get; }
        public ICommand KategoriCommand { get; }
      
        public MainViewModel( DashboardViewModel dashboardViewModel, TedarikciViewModel tedarikciViewModel,
            ParcaViewModel parcaViewModel, UsersYonetimViewModel usersYonetimViewModel, KategoriViewModel kategoriViewModel)
        {
            _usersYonetimViewModel = usersYonetimViewModel;
            _dashboardViewModel = dashboardViewModel;
            _tedarikciViewModel= tedarikciViewModel;
            _parcaViewModel = parcaViewModel;
            _kategoriViewModel = kategoriViewModel;
            CurrentView = new LoginViewModel(this);
            UserYonCommand = new RelayCommand(OpenUserPanel);
            DashboardCommand = new RelayCommand(OpenDashboard);
            TedarikciCommand = new RelayCommand(OpenTedarikci);
            ParcaCommand = new RelayCommand(OpenParca);
            KategoriCommand = new RelayCommand(OpenKat);

        }

        public void Kullanicigirisyapti(Users users)
        {
            UserSession.CurrentUser = users;
            CurrentView = _dashboardViewModel;

            OnPropertyChanged(nameof(UserPanelgor));
            OnPropertyChanged(nameof(UserPanelVisibility));
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
                return UserSession.IsAdmin
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

        }

    }
}
