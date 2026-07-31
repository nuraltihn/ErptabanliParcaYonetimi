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
using Erpyonetimi.Services;
using Erpyonetimi.Data.Helpers;


namespace Erpyonetimi.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private string _kullaniciAdi = "";
        private readonly AuthService _authservice;
        public string KullaniciAdi
        {
            get => _kullaniciAdi;
            set
            {
                _kullaniciAdi = value; 
                OnPropertyChanged();
            }
        }


        private string _sifre = "";
        public string Sifre
        {
            get => _sifre;
            set { _sifre = value; 
            OnPropertyChanged(nameof(Sifre));  
            }
        }

        public ICommand GirisCommand { get; }
        private readonly MainViewModel _mainViewModel;
        public LoginViewModel(MainViewModel mainViewModel)
        { _authservice = new AuthService();
            _mainViewModel = mainViewModel;
            GirisCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            var user = _authservice.Login(KullaniciAdi, Sifre);
            
            if(user != null)
            {
                _mainViewModel.Kullanicigirisyapti(user);
            }
            else
            {
                MessageBox.Show("Kullanıcı yada şifre hatalı.");
            }
        }

    }
}
