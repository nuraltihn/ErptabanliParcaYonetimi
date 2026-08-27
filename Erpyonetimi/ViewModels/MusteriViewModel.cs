using Erpyonetimi.Application.Services.Interfaces;
using Erpyonetimi.Commands;
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
    public class MusteriViewModel :BaseViewModel
    {
        private readonly IMusteriService _musteriService;
        public ObservableCollection<Musteri> Musteriler { get; set; }
        private List<Musteri> _tumMusteriler;
        private Musteri? _seciliMusteri;
        public Musteri? SeciliMusteri
        {
            get => _seciliMusteri;
            set
            {
                _seciliMusteri = value;
                if (_seciliMusteri != null)
                {
                    MusteriKodu = _seciliMusteri.MusteriKodu ?? "";
                    FirmaAdi = _seciliMusteri.FirmaAdi ?? "";
                    YetkiliKisi = _seciliMusteri.YetkiliKisi ?? "";
                    Ad = _seciliMusteri.Ad ?? "";
                    Soyad = _seciliMusteri.Soyad ?? "";
                    Adres = _seciliMusteri.Adres ?? "";
                    Sehir = _seciliMusteri.Sehir ?? "";
                    Tel = _seciliMusteri.Tel ?? "";
                    Email = _seciliMusteri.Email ??"";
                    Fax = _seciliMusteri.Fax??"";
                    VergiNo = _seciliMusteri.VergiNo ??"";

                   

                }
                OnPropertyChanged();
            }
        }
        private string _musteriKodu;
        public string MusteriKodu
        {
            get => _musteriKodu;
            set
            {
                _musteriKodu = value;
                OnPropertyChanged();
            }
        }

        private string _firmaAdi;
        public string FirmaAdi
        {
            get => _firmaAdi;
            set
            {
                _firmaAdi= value;
                OnPropertyChanged();
            }
        }
        private string _yetkiliKisi;
        public string YetkiliKisi
        {
            get => _yetkiliKisi;
            set
            {
                _yetkiliKisi = value;
                OnPropertyChanged();
            }
        }
        private string? _ad = "";
        private string _soyad;
        private string _adres;
        private string? _sehir;
        private string? _tel;
        private string? _email;
        private string? _vergiNo;
        private string? _fax;
   
        public string? Ad { get => _ad; set { _ad = value;  OnPropertyChanged(); } }
        public string? Soyad { get => _soyad; set { _soyad = value; OnPropertyChanged(); } }
        public string Adres { get => _adres; set { _adres = value; OnPropertyChanged(); } }
        public string? Sehir { get => _sehir; set { _sehir = value; OnPropertyChanged(); } }
        public string? Tel { get => _tel; set { _tel = value; OnPropertyChanged(); } }
        public string? Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        public string? VergiNo { get => _vergiNo; set { _vergiNo = value; OnPropertyChanged(); } }
        public string? Fax { get => _fax; set { _fax = value; OnPropertyChanged(); } }
        private string _aramaMetni;
        public string AramaMetni
        {
            get => _aramaMetni;
            set { _aramaMetni = value;
                OnPropertyChanged();
                Filtrele();
            }
        }
        public ICommand MusteriEkleCommand { get; }
        public ICommand MusteriGuncelleCommand { get; }
        public ICommand MusteriSilCommand { get; }
        public ICommand MusteriListeleCommand { get; }
        public ICommand MusteriTemizleCommand { get; }
        public MusteriViewModel(IMusteriService musteriService)
        {
            _musteriService = musteriService;

Musteriler = new ObservableCollection<Musteri>();
            MusteriEkleCommand = new RelayCommand(async ()=>await Ekle());
            MusteriGuncelleCommand = new RelayCommand(async()=> await Guncelle());
            MusteriSilCommand = new RelayCommand(async ()=> await Sil());
            MusteriListeleCommand = new RelayCommand(async()=>await Listele());
            MusteriTemizleCommand= new RelayCommand(Temizle);

            _ = Listele();
        }
        private void Filtrele()
            
        {
            if (_tumMusteriler == null) return;
            var arama = AramaMetni?.Trim();

            var sonuc = _tumMusteriler
                .Where(p =>
                string.IsNullOrWhiteSpace(arama)
                || (!string.IsNullOrWhiteSpace(p.MusteriKodu)&&p.MusteriKodu.Contains(arama,StringComparison.OrdinalIgnoreCase))
                || (!string.IsNullOrWhiteSpace(p.FirmaAdi)&&p.FirmaAdi.Contains(arama, StringComparison.OrdinalIgnoreCase))
                ||(!string.IsNullOrWhiteSpace(p.YetkiliKisi)&&p.YetkiliKisi.Contains(arama, StringComparison.OrdinalIgnoreCase))).ToList();

            Musteriler = new ObservableCollection<Musteri>(sonuc);
            OnPropertyChanged(nameof(Musteriler));
        }
        private async Task Listele()
        {
            var musteriler = await _musteriService.GetAllAsync();
            _tumMusteriler= musteriler?.ToList() ?? new List<Musteri>();
            if (!string.IsNullOrWhiteSpace(AramaMetni))
            {
                Filtrele();
            }
            else { 
            Musteriler = new ObservableCollection<Musteri>(musteriler);
            OnPropertyChanged(nameof(Musteriler));
            }
        }
        private void Temizle()
        {
            MusteriKodu = "";
            FirmaAdi = "";
            Ad = "";
            Soyad = "";
            Adres = "";
            Sehir = "";
            Tel = "";
            Email = "";
            VergiNo = "";
            Fax = "";
            YetkiliKisi = "";
            SeciliMusteri = null;
        }

        private async Task Ekle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }

            if (string.IsNullOrWhiteSpace(MusteriKodu)|| string.IsNullOrWhiteSpace(FirmaAdi))
            {
                MessageBox.Show("Firma adı ve müşteri zorunludur");
                return;
            }
            if (string.IsNullOrWhiteSpace(Tel)||Tel.Length!=11)
            {
                MessageBox.Show("Telefon numaranız 11 haneli olmak zorundadır. ");
                return;
            }
            if (string.IsNullOrWhiteSpace(Email)|| !Email.Contains("@"))
            {
                MessageBox.Show("Geçerli bir email giriniz.");
                return;
            }
           
               var mevcut = await _musteriService.GetByKodAsync(MusteriKodu);
            if(mevcut != null)
            {
                MessageBox.Show("Bu müşteri kodu zaten mevcut");
                return;
            }
            if (string.IsNullOrWhiteSpace(Ad))
            {
                MessageBox.Show("Ad zorunlu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Soyad))
            {
                MessageBox.Show("Soyad zorunlu.");
                return;
            }
            var musteri = new Musteri
            {
                MusteriKodu= MusteriKodu,
                FirmaAdi= FirmaAdi,
                YetkiliKisi= YetkiliKisi,
                Ad= Ad,
                Soyad= Soyad,
                Adres= Adres,
                Sehir=Sehir,
                Tel= Tel,
                Email= Email,
                VergiNo= VergiNo,
                Fax = Fax
            };
            await _musteriService.AddMusteriAsync(musteri);
            await Listele();
             Temizle();
            MessageBox.Show("Müşteri eklendi.");
           
        }
        private async Task Guncelle()

        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }


            if (string.IsNullOrWhiteSpace(Ad))
            {
                MessageBox.Show("Ad zorunlu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Soyad))
            {
                MessageBox.Show("Soyad zorunlu.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Tel) || Tel.Length != 11)
            {
                MessageBox.Show("Telefon numaranız 11 haneli olmak zorundadır. ");
                return;
            }
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@"))
            {
                MessageBox.Show("Geçerli bir email giriniz.");
                return;
            }

            if (MusteriKodu == null || FirmaAdi == null)
            {
                MessageBox.Show("Firma adı ve müşteri zorunludur");
                return;
            }
            if (SeciliMusteri == null)
                return;
            
            SeciliMusteri.MusteriKodu = MusteriKodu;
            SeciliMusteri.FirmaAdi= FirmaAdi;
            SeciliMusteri.YetkiliKisi = YetkiliKisi;
            SeciliMusteri.Ad = Ad;
            SeciliMusteri.Soyad = Soyad;
            SeciliMusteri.Adres = Adres;
            SeciliMusteri.Sehir = Sehir;
            SeciliMusteri.Tel = Tel;
            SeciliMusteri.Email = Email;
            SeciliMusteri.VergiNo = VergiNo;
            SeciliMusteri.Fax = Fax;
             await _musteriService.UpdateMusteriAsync(SeciliMusteri);
            await Listele();
            MessageBox.Show("Müşteri güncellendi");
        }

        private async Task Sil()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok.");
                return;
            }
            if (SeciliMusteri == null)
                return;
            var cevap = MessageBox.Show(
                "Seçili müşteriyi silmek istediğinize emin misiniz?", "silme onayı", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;
            await _musteriService.DeleteMusteriAsync(SeciliMusteri);
            await Listele();
            Temizle();
            MessageBox.Show("Müşteri silindi.");
        }
    }
}
