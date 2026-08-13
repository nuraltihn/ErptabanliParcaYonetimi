using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Repositories;
using Erpyonetimi.Domain.Entities;
using Erpyonetimi.Application.Services;
using Erpyonetimi.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Erpyonetimi.Helpers;
using System.Windows;

namespace Erpyonetimi.ViewModels
{
    public class UsersYonetimViewModel :BaseViewModel
    {
        private readonly IUsersService _usersService;
        public ObservableCollection<Users> Userlist { get; set; }
        public ObservableCollection<Roles> Roller { get; set; }
        private readonly ErpDbContext _context;
        private List<Users> _tumKullanicilar;
        private Users? _selecteduser;
        public Users? SelectedUser
        {
            get => _selecteduser;
            set
            {
                _selecteduser = value;
                if (_selecteduser != null)
                {
                    AdSoyad = _selecteduser.AdSoyad;
                    KulAd = _selecteduser.KulAd;
                    RolId=_selecteduser.RolId ?? 0;
    SeciliRol = Roller.FirstOrDefault(
                        x => x.Id == _selecteduser.RolId);
                    Sifre = "";
                }
                OnPropertyChanged();
            }
        }
        private string _aramaMetni;
        public string AramaMetni
        {
            get => _aramaMetni;
            set
            {
                _aramaMetni = value;
                OnPropertyChanged();
                Filtrele();
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
        private Roles? _seciliRol;
        public Roles? SeciliRol
        {
            get => _seciliRol;
            set
            {
                _seciliRol = value;
                if (_seciliRol != null)
                    RolId = _seciliRol.Id;

                OnPropertyChanged();

            }
        }

        public ICommand UsersEkleCommand { get; }
        public ICommand UsersGuncelCommand { get; }
        public ICommand UsersSilCommand { get; }
        public UsersYonetimViewModel()
        {
            _context = new ErpDbContextFactory()
                .CreateDbContext(Array.Empty<string>());

            var repo = new UsersRepository(_context);
            _usersService = new UsersService(repo);

           Roller = new ObservableCollection<Roles>(
                _context.Roles.ToList());

           Userlist = new ObservableCollection<Users>(
                _usersService.GetAllUsers());
            UsersEkleCommand = new RelayCommand(UsersEkleme);
            UsersGuncelCommand = new RelayCommand(UsersGuncelleme);
            UsersSilCommand = new RelayCommand(UsersSilme);
            _tumKullanicilar = _usersService.GetAllUsers();
            Userlist = new ObservableCollection<Users>(_tumKullanicilar);
        }

        private void UsersEkleme()
        {
            if (string.IsNullOrWhiteSpace(KulAd))
            {
                MessageBox.Show("Kullanıcı adı boş olamaz");
                return;
            }
            var user = new Users
            {
                AdSoyad= AdSoyad,
                KulAd= KulAd,
                Sifre= PasswordHelper.HashPassword(Sifre),
                RolId= RolId
            };

            _usersService.AddUser(user);
            Listele();
            Temizle();
            MessageBox.Show("Kullanıcı eklendi.");
        }

        private void Filtrele()
        {
            var sonuc = _tumKullanicilar
                .Where(x => string.IsNullOrWhiteSpace(AramaMetni) ||
                x.AdSoyad.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)
                || x.KulAd.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)).ToList();
            Userlist= new ObservableCollection<Users>(sonuc);
            OnPropertyChanged(nameof(Userlist));
        }

        private void Listele()
        {
            Userlist = new ObservableCollection<Users>(
                _usersService.GetAllUsers());
            OnPropertyChanged(nameof(Userlist));
        }


        private void UsersGuncelleme()
        {
            if (SelectedUser == null)
                return;

            SelectedUser.AdSoyad = AdSoyad;
            SelectedUser.KulAd = KulAd;
            SelectedUser.RolId = RolId;
            if (!string.IsNullOrWhiteSpace(Sifre))
            {
                SelectedUser.Sifre = PasswordHelper.HashPassword(Sifre);
            }
                _usersService.UpdateUser(SelectedUser);
            Listele();

        }
        private void UsersSilme()
        {
            if (SelectedUser == null)
                return;

            _usersService.DeleteUser(SelectedUser.Id);
            Listele();
            Temizle();
        }
        private void Temizle()
        {
            SelectedUser = null;
            AdSoyad = "";
            KulAd = "";
            Sifre = "";
            RolId = 0;
            SeciliRol = null;
            OnPropertyChanged(nameof(AdSoyad));
            OnPropertyChanged(nameof(KulAd));
            OnPropertyChanged(nameof(Sifre));
            OnPropertyChanged(nameof(RolId));
            OnPropertyChanged(nameof(SeciliRol));
               
        }
    }
}
