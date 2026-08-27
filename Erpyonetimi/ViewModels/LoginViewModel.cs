using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D.Converters;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.ViewModels;
using Erpyonetimi.Application.Services;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Data.Interfaces;
using Erpyonetimi.Data.Repositories;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Erpyonetimi.Nav;

namespace Erpyonetimi.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private string _kullaniciAdi = "";
        private readonly IAuthService _authservice;
       
        public string KullaniciAdi
        {
            get => _kullaniciAdi;
            set
            {
                _kullaniciAdi = value; 
                OnPropertyChanged();
            }
        }


        //private string _sifre = "";
        //public string Sifre
        //{
        //    get => _sifre;
        //    set { _sifre = value; 
        //    OnPropertyChanged(nameof(Sifre));  
        //    }
        //}
        private readonly INavigationService _navigationservice;
        public ICommand GirisCommand { get; }
        //private readonly MainViewModel _mainViewModel;
        public LoginViewModel(/*MainViewModel mainViewModel,*/ IAuthService authService, INavigationService navigationService)
        {
            _authservice = authService ?? throw new ArgumentNullException(nameof(authService));
            //_mainViewModel = mainViewModel;
           _navigationservice = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            
            GirisCommand = new RelayCommand(async (param) => await Login(param));
        }

        private async Task Login(object parameter)
        {

            if (string.IsNullOrWhiteSpace(KullaniciAdi))
            {
                MessageBox.Show("Lütfen kullanıcı adınızı giriniz");
                return;
            }
            DatabaseHelper.CheckConnection();
            if (!DatabaseHelper.IsConnected)
            {
                UserSession.CurrentUser = new Users
                {
                    AdSoyad = "Çevrimdışı Kullanıcı",
                    KulAd = "offline"
                };
                MessageBox.Show(
                    "Veritabanına bağlanılamadı.\n" +
                    "Çevrimdışısınız.",
                    "Çevrimdışı mod", MessageBoxButton.OK, MessageBoxImage.Information);
                //_mainViewModel.Kullanicigirisyapti(UserSession.CurrentUser);
                return;
            }

            if(parameter is PasswordBox passwordBox)
            {
                var password = passwordBox.Password ?? "";
                if (string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Lütfen şifrenizi giriniz.");
                    return;
                }

                 var factory = new ErpDbContextFactory();
                using var context = factory.CreateDbContext(Array.Empty<string>());

                IUsersRepository repo = new UsersRepository(context);
                IAuthService authService = new AuthService(repo);
                
                var user = await _authservice.LoginAsync(KullaniciAdi.Trim(), password);

                if (user != null)
                {
                    UserSession.CurrentUser = user;
                    _navigationservice.Navigate(Pages.dashboard);
                    //UserSession.CurrentUser = user;
                    //var rol = user.Rol?.RolAdi;
                    //switch (rol)
                    //{
                    //    case "Admin":
                    //        _navigationservice.Navigate(Pages.dashboard);
                    //        break;
                    //    case "Satış":
                    //        _navigationservice.Navigate(Pages.dashboard);
                    //        break;

                    //    case "Depo":
                    //        _navigationservice.Navigate(Pages.dashboard);
                    //        break;

                    //    default:
                    //        MessageBox.Show(
                    //            "Kullanıcının geçerli bir rolü bılınamadı",
                    //            "Giriş Hatası",
                    //            MessageBoxButton.OK,
                    //            MessageBoxImage.Warning);
                    //        break;
                    //}

                    _navigationservice.Navigate(Pages.dashboard);
                    //_mainViewModel.Kullanicigirisyapti(user);
                }
                else
                {
                    MessageBox.Show("Kullanıcı yada şifre hatalı.",
                        "Giriş Hatası",MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
          
        }

    }
}
