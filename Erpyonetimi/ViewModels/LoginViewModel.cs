using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D.Converters;

namespace Erpyonetimi.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private string _kullaniciAdi = "";
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
        {
            _mainViewModel = mainViewModel;
            GirisCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            
            var factory = new ErpDbContextFactory();
            using var db = factory.CreateDbContext(Array.Empty<string>());
            string hash = PasswordHelper.HashPassword(Sifre);

            var user = db.Users.FirstOrDefault(x => x.KulAd == KullaniciAdi && x.Sifre == hash);

            if(user != null)
            {
                MessageBox.Show("Giriş Başarılı.");

                //dashboard geçme
            }
            else
            {
                MessageBox.Show("Kullanıcı yada şifre hatalı.");
            }
        }

    }
}
