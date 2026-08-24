using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
using Erpyonetimi.Context;
using Erpyonetimi.Data.Helpers;
using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Erpyonetimi.ViewModels
{
    public class TedarikciViewModel : BaseViewModel
    {
        
        private string _tedarikciKodu;
        private string _firmaAdi;
        private string _yetkiliKisi;
        private string _tel;
        private string _email;
        private string _adres;
        private string _vergino;
        private string _fax;
        private Tedarikci _seciliTedarikci;
        public Tedarikci SeciliTedarikci
        {
            get => _seciliTedarikci;
            set
            {
                _seciliTedarikci = value;
                if (_seciliTedarikci != null)
                {
                    TedarikciKodu = _seciliTedarikci.TedarikciKodu;
                    FirmaAdi = _seciliTedarikci.FirmaAdi;
                    YetkiliKisi = _seciliTedarikci.YetkiliKisi;
                    Tel = _seciliTedarikci.Tel;
                    Email = _seciliTedarikci.Email;
                }
                OnPropertyChanged();
            }
        }
        public Visibility AdminButtonVisibil
        {
            get
            {
                return UserSession.IsAdmin
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }
        public string TedarikciKodu
        {
            get => _tedarikciKodu;
            set
            {
                _tedarikciKodu = value;
                OnPropertyChanged();
            }
        }
        public string Adres
        {
            get => _adres;
            set
            {
                _adres = value;
                OnPropertyChanged();
            }
        }
        public string VergiNo
        {
            get => _vergino;
            set
            {
                _vergino = value;
                OnPropertyChanged();
            }
        }
        public string Fax
        {
            get => _fax;
            set
            {
                _fax = value;
                OnPropertyChanged();
            }
        }
        
        public string FirmaAdi
        {
            get => _firmaAdi;
            set
            {
                _firmaAdi = value;
                OnPropertyChanged();
            }
        }

        
        public string YetkiliKisi
        {
            get => _yetkiliKisi;
            set { _yetkiliKisi = value; OnPropertyChanged(); }

        }
        
        public string Tel
        {
            get => _tel;
            set { _tel = value; OnPropertyChanged(); }
        }
        
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        private List<Tedarikci> _tumtedarikciler
;        private string _aramaMetni;
        public string AramaMetni
        {
            get => _aramaMetni;
            set { _aramaMetni = value; OnPropertyChanged();
                Filtrele();
            }
        }
        public ObservableCollection<Tedarikci> Tedarikciler { get; set;  }

        public ICommand TedarikciEkleCommand { get;}
        public ICommand TedarikciGuncelleCommand { get; }
        public ICommand TedarikciTemizleCommand { get; }
        public ICommand TedarikciSilCommand { get; }

        public ICommand TedarikciListeleCommand { get; }
        private readonly ITedarikciService _tedarikciService;

        public TedarikciViewModel(ITedarikciService tedarikciService)
        {
            _tedarikciService = tedarikciService;

               Tedarikciler = new ObservableCollection<Tedarikci>();
            TedarikciEkleCommand = new RelayCommand(Ekle);
            TedarikciGuncelleCommand = new RelayCommand(Guncelle);
            TedarikciSilCommand = new RelayCommand(Sil);
            TedarikciListeleCommand = new RelayCommand(Listele);
            TedarikciTemizleCommand = new RelayCommand(Temizle);
         
            _ = Listele();
        }
        private void Filtrele()
        {
            if(_tumtedarikciler ==null)
                return;
          var sonuc= _tumtedarikciler
                .Where(x => string.IsNullOrWhiteSpace(AramaMetni)
                    || x.FirmaAdi?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true
                    || x.TedarikciKodu?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true
                    || x.YetkiliKisi?.Contains(AramaMetni ?? "", StringComparison.OrdinalIgnoreCase) == true));
            Tedarikciler = new ObservableCollection<Tedarikci>(sonuc);
            OnPropertyChanged(nameof(Tedarikciler));
        }

        private async Task Listele()
        {
            var tedarikciler = await _tedarikciService.GetAllTedarikciAsync();
            _tumtedarikciler = tedarikciler;
            Tedarikciler = new ObservableCollection<Tedarikci>(_tumtedarikciler);

            OnPropertyChanged(nameof(Tedarikciler));
        }

        private async Task Ekle()
        {
            var mevcut = await _tedarikciService.GetByKodAsync(TedarikciKodu);
            if(mevcut != null)
            {
                MessageBox.Show("Bu tedarikçi kodu zaten kayıtlı");
                return;
            }
            if (string.IsNullOrWhiteSpace(TedarikciKodu))
            {
                MessageBox.Show("Tedarikçi kodu boş olamaz.");
                return;
            }
            if(!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veribağlantısı yok.Bu işlem yapılamaz");
                return;
            }
            if (string.IsNullOrWhiteSpace(FirmaAdi) || string.IsNullOrWhiteSpace(YetkiliKisi))
            {
                MessageBox.Show("lütfen gerekli yerleri doldurunuz.");
                return;
 
            }
            if (string.IsNullOrWhiteSpace(TedarikciKodu))
            {
                MessageBox.Show("Tedarikçi kodu boş olamaz.");
                return;
            }
            if (string.IsNullOrWhiteSpace(VergiNo)|| VergiNo.Length != 10)
            {
                MessageBox.Show("Vergi numarası 10 haneli olmak zorundadır.");
                return;
            }
            
                
            if (!UserSession.IsAdmin)
                return;

            await _tedarikciService.AddTedarikciAsync(
                new Tedarikci
                {
                    TedarikciKodu = TedarikciKodu,
                    FirmaAdi = FirmaAdi,
                    YetkiliKisi = YetkiliKisi,
                    Tel = Tel,
                    Email = Email,
                    Adres= Adres,
                    VergiNo= VergiNo,
                    Fax= Fax
                    
                });

            await Listele();
        }
        private void Temizle()
        {
            TedarikciKodu = "";
            FirmaAdi = "";
            YetkiliKisi = "";
            Tel = "";
            Email = "";
            Adres = "";
            VergiNo = "";
            Fax = "";
        }
        private async Task Sil()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veribağlantısı yok.Bu işlem yapılamaz");
                return;
            }
            if (!UserSession.IsAdmin)
                return;  


            if (SeciliTedarikci != null) {
               ;
                var cevap = MessageBox.Show(
                    "Seçili tedarikçiyi silmek istediğinize emin misiniz?",
                    "Silme Onayı", MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (cevap !=MessageBoxResult.Yes)
                {
                    return;
                }
               await _tedarikciService.DeleteTedarikciAsync(SeciliTedarikci.Id);
                await Listele();
            }
        }
        private async Task Guncelle()
        {
            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veribağlantısı yok.Bu işlem yapılamaz");
                return;
            }
            if(string.IsNullOrWhiteSpace(VergiNo)|| VergiNo.Length != 10)
            {
                MessageBox.Show("Vergi numarası 10 haneli olmak zorundadır");
                return;
            }
            if (TedarikciKodu == null || FirmaAdi == null || YetkiliKisi == null)
            {
                MessageBox.Show("lütfen gerekli yerleri doldurunuz.");
                return;

            }
            if (!UserSession.IsAdmin)
                return;

            if (SeciliTedarikci == null)
                return;

            SeciliTedarikci.TedarikciKodu = TedarikciKodu;
            SeciliTedarikci.FirmaAdi= FirmaAdi;
            SeciliTedarikci.YetkiliKisi= YetkiliKisi;
            SeciliTedarikci.Tel= Tel;
            SeciliTedarikci.Email = Email;
            SeciliTedarikci.Adres = Adres;
            SeciliTedarikci.VergiNo = VergiNo;
            SeciliTedarikci.Fax = Fax;


           await  _tedarikciService.UpdateTedarikciAsync(SeciliTedarikci);
            await Listele();
           
        }

    }
}
