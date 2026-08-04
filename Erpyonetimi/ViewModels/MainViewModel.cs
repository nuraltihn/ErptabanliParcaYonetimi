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
       public bool AdminPaneligor
        {
            get
            {
                return UserSession.IsAdmin;
            }
        }

        public ICommand AdminPanelCommand { get; }
        public ICommand DashboardCommand { get; }
        public ICommand TedarikciCommand { get; }
        public ICommand ParcaCommand { get; }
      
        public MainViewModel()
        {
            //_dashboardViewModel = dashboardViewModel;
            //_tedarikciViewModel= tedarikciViewModel;
            CurrentView = new LoginViewModel(this);
            AdminPanelCommand = new RelayCommand(OpenAdminPanel);
            DashboardCommand = new RelayCommand(OpenDashboard);
            TedarikciCommand = new RelayCommand(OpenTedarikci);
            ParcaCommand = new RelayCommand(OpenParca);

        }

        public void Kullanicigirisyapti(Users users)
        {
            UserSession.CurrentUser = users;
            CurrentView = UserSession.IsAdmin ? new AdminPanelViewModel()
                 :  _dashboardViewModel;

            OnPropertyChanged(nameof(AdminPaneligor));
            OnPropertyChanged(nameof(AdminPanelVisibility));
        }
      
        private void OpenParca()
        {
            CurrentView = new ParcaViewModel();
        }
        private void OpenTedarikci()
        {
            CurrentView = _tedarikciViewModel;
        }
        private void OpenDashboard()
        {
            CurrentView = _dashboardViewModel;
        }
        private void OpenAdminPanel()
        {
            if (!UserSession.IsAdmin)
            {
                MessageBox.Show("Erişim yetkiniz yok");
                return;
            }
            CurrentView = new AdminPanelViewModel();
        }
        public Visibility AdminPanelVisibility
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
