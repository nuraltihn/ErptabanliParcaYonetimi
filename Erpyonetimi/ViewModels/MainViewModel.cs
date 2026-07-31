using Erpyonetimi.Commands;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Erpyonetimi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView= value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Sidemenugorunme));
                OnPropertyChanged(nameof(Sidemenugenisligi));
            }
        }

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
        public MainViewModel()
        {
            
            CurrentView = new LoginViewModel(this);
            AdminPanelCommand = new RelayCommand(OpenAdminPanel);
        }

        public void Kullanicigirisyapti(Users users)
        {
            UserSession.CurrentUser = users;
            CurrentView = UserSession.IsAdmin ? new AdminPanelViewModel()
                 : new DashboardViewModel();

            OnPropertyChanged(nameof(AdminPaneligor));
            OnPropertyChanged(nameof(AdminPanelVisibility));
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
