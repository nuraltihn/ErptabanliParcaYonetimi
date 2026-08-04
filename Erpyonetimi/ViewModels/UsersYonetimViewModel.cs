using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Services;
using Erpyonetimi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class UsersYonetimViewModel :BaseViewModel
    {
        private readonly IUsersService _usersService;
        public ObservableCollection<Users> Userlist { get; set; }

        private Users _selecteduser;
        public Users SelectedUser
        {
            get => _selecteduser;
            set
            {
                _selecteduser = value;
                OnPropertyChanged();
            }
        }
        private string _adSoyad;
        public string AdSoyad
        {
            get => _adSoyad;
            set
            {
                _adSoyad = value;
                OnPropertyChanged();
            }
        }

        private string _kulAd;
        public string KulAd
        {
            get => _kulAd;
            set
            {
                _kulAd = value;
                OnPropertyChanged();
            }
        }

        private string _sifre;
        public string Sifre
        {
            get => _sifre;
            set
            {
                _sifre = value;
                OnPropertyChanged();
            }
        }

        private int _rolId;
        public int RolId
        {
            get => _rolId;
            set
            {
                _rolId = value;
                OnPropertyChanged();
            }
        }


        public ICommand UsersEkleCommand { get; }
        public ICommand UsersGuncelCommand { get; }
        public ICommand UsersSilCommand { get; }
        public UsersYonetimViewModel()
        {
            var context = new ErpDbContextFactory()
                .CreateDbContext(Array.Empty<string>());
            var repo = new UsersRepository(context);
            _usersService = new UsersService(repo);
            

            Userlist = new ObservableCollection<Users>(
                _usersService.GetAllUsers());
            UsersEkleCommand = new RelayCommand(UsersEkleme);
            UsersGuncelCommand = new RelayCommand(UsersGuncelleme);
            UsersSilCommand = new RelayCommand(UsersSilme);
        }

        private void UsersEkleme()
        {
            var user = new Users
            {
                AdSoyad= AdSoyad,
                KulAd= KulAd,
                Sifre= Sifre,
                RolId= RolId
            };
            _usersService.AddUser(user);
        }
        private void UsersGuncelleme()
        {
            if (SelectedUser != null)
                _usersService.UpdateUser(SelectedUser);

        }
        private void UsersSilme()
        {
            if (SelectedUser == null)
                return;

            _usersService.DeleteUser(SelectedUser.Id);
            Userlist.Remove(SelectedUser);
        }
    }
}
