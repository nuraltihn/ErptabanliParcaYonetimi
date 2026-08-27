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
using Microsoft.EntityFrameworkCore.Update.Internal;
using Erpyonetimi.Data.Helpers;
namespace Erpyonetimi.ViewModels
{
    public class UsersYonetimViewModel :BaseViewModel
    {
        private readonly IUsersService _usersService; 
        private readonly ErpDbContext _context;
        private ObservableCollection<Users> _userlist = new();
        public ObservableCollection<Users> Userlist
        {
            get => _userlist;
            set
            {
                _userlist = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Roles> _roller = new();
        public ObservableCollection<Roles> Roller
        {
            get => _roller; set
            {
                _roller = value; OnPropertyChanged();
            }
        }

        private List<Users> _tumKullanicilar = new();
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
                if (value == null)
                    return;
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
                if (value == null)
                    return;
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
        public ICommand UsersTemizleCommand { get; }
        public UsersYonetimViewModel()
        {
             UsersEkleCommand = new RelayCommand(async()=>await UsersEkleme());
            UsersGuncelCommand = new RelayCommand(async()=>await UsersGuncelleme());
            UsersSilCommand = new RelayCommand(async()=>await UsersSilme());
            UsersTemizleCommand = new RelayCommand(Temizle);
             Roller = new ObservableCollection<Roles>();
             Userlist = new ObservableCollection<Users>();

            DatabaseHelper.CheckConnection();
            if (!DatabaseHelper.IsConnected)
                return;

            _context=new ErpDbContextFactory().CreateDbContext(Array.Empty<string>());
           
            var repo = new UsersRepository(_context);
            _usersService = new UsersService(repo);

            Roller = new ObservableCollection<Roles>(_context.Roles.ToList());

            _ = Listele();
        }

        private async Task UsersEkleme()
            
        {
            var mevcut = await _usersService.GetByAdSoyadAsync(AdSoyad);
            if (mevcut != null)
            {
                MessageBox.Show("Bu kullanıcı zaten var.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AdSoyad))
            {
                MessageBox.Show("Lütfen gerekli verileri giriniz..");
                return;
            }
         
            if (Sifre.Length<5)
            {
                MessageBox.Show("Şifreniz en az 5 karakter uzunlukta olmalıdır");
                return;
            }
            if(KulAd.Contains(" "))
            {
                MessageBox.Show("Kullanıcı adı boşluk içeremez.");
                return;
            }
            if (string.IsNullOrWhiteSpace(KulAd))
            {
                MessageBox.Show("Kullanıcı adı boş olamaz.");
                return;
            }
            
            if (SeciliRol == null)
            {
                MessageBox.Show("Rol seçiniz");
                return;
            }
            var user = new Users
            {
                AdSoyad= AdSoyad,
                KulAd= KulAd,
                Sifre= PasswordHelper.HashPassword(Sifre),
                RolId= RolId
            };

            await _usersService.AddUserAsync(user);
            await Listele();
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

        private async Task Listele()
        {
            var users = await _usersService.GetAllUsersAsync();
            _tumKullanicilar = users?.ToList() ?? new List<Users>();
            Userlist = new ObservableCollection<Users>(
              _tumKullanicilar);
            
        }


        private async Task UsersGuncelleme()
        {
            if (string.IsNullOrWhiteSpace(KulAd))
            {
                MessageBox.Show("Kullanıcı adı boş olamaz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(AdSoyad))
            {
                MessageBox.Show("Ad Soyad zorunlu.");
                return;
            }
            if (SeciliRol == null)
            {
                MessageBox.Show("Rol seçiniz");
                return;
            }
            if (SelectedUser == null)
                return;
            if(SelectedUser.Id != UserSession.CurrentUser?.Id)
            {
                if(SelectedUser.Sifre != Sifre)
                {
                    MessageBox.Show("Başka kullanıcıların şifresi değiştirilemez.");
                    return;
                }
            }
            SelectedUser.AdSoyad = AdSoyad;
            SelectedUser.KulAd = KulAd;
            SelectedUser.RolId = RolId;
            if (!string.IsNullOrWhiteSpace(Sifre))
            {
                SelectedUser.Sifre = PasswordHelper.HashPassword(Sifre);
            }
               await _usersService.UpdateUserAsync(SelectedUser);
            await Listele();

        }
        private async Task UsersSilme()
        {
            if (SelectedUser == null)
                return;
            var cevap = MessageBox.Show("Seçili kullanıcıyı silmek ister misiniz?",
                "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;
           await _usersService.DeleteUserAsync(SelectedUser.Id);
           await Listele();
            Temizle();
        }
        private void Temizle()
        {
            _selecteduser = null;
            OnPropertyChanged(nameof(SelectedUser));
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
