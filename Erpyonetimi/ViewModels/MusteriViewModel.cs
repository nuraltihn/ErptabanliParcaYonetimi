using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Domain.Entities;


using Erpyonetimi.Application.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Erpyonetimi.Commands;
using System.Windows;

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
                if (value != null)
                {
                    MusteriKodu = value.MusteriKodu;
                    FirmaAdi = value.FirmaAdi;
                    YetkiliKisi = value.YetkiliKisi;
                    Ad = value.Ad;
                    Soyad = value.Soyad;
                    Adres = value.Adres;
                    Sehir = value.Sehir;
                    Tel = value.Tel;
                    Email = value.Email;
                    Fax = value.Fax;
                    VergiNo = value.VergiNo;

                    OnPropertyChanged(nameof(MusteriKodu));
                    OnPropertyChanged(nameof(FirmaAdi));
                    OnPropertyChanged(nameof(YetkiliKisi));
                    OnPropertyChanged(nameof(Ad));
                    OnPropertyChanged(nameof(Soyad));
                    OnPropertyChanged(nameof(Adres));
                    OnPropertyChanged(nameof(Sehir));
                    OnPropertyChanged(nameof(Tel));
                    OnPropertyChanged(nameof(Email));
                    OnPropertyChanged(nameof(Fax));
                    OnPropertyChanged(nameof(VergiNo));

                }
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

        public MusteriViewModel(IMusteriService musteriService)
        {
            _musteriService = musteriService;

            Musteriler = new ObservableCollection<Musteri>(_musteriService.GetAll());

            MusteriEkleCommand = new RelayCommand(Ekle);
            MusteriGuncelleCommand = new RelayCommand(Guncelle);
            MusteriSilCommand = new RelayCommand(Sil);
            _tumMusteriler = _musteriService.GetAll();
            Musteriler = new ObservableCollection<Musteri>(_tumMusteriler);
        }
        private void Filtrele()
        {
            var sonuc = _tumMusteriler
                .Where(p =>
                string.IsNullOrWhiteSpace(AramaMetni)
                || p.MusteriKodu.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)
                || p.FirmaAdi.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)
                || p.YetkiliKisi.Contains(AramaMetni, StringComparison.OrdinalIgnoreCase)).ToList();

            Musteriler = new ObservableCollection<Musteri>(sonuc);
            OnPropertyChanged(nameof(Musteriler));
        }
        private void Listele()
        {
            Musteriler = new ObservableCollection<Musteri>(_musteriService.GetAll());
            OnPropertyChanged(nameof(Musteriler));
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

            SeciliMusteri = null;
        }

        private void Ekle()
        {
            if (MusteriKodu == null || FirmaAdi == null)
                return;
            if(_musteriService.GetByKod(MusteriKodu) != null)
            {
                MessageBox.Show("Bu müşteri kodu zaten mevcut");
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
            _musteriService.AddMusteri(musteri);
            Listele();
             Temizle();
            MessageBox.Show("Müşteri eklendi.");
           
        }
        private void Guncelle()
        {
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
            _musteriService.UpdateMusteri(SeciliMusteri);
            Listele();
            MessageBox.Show("Müşteri güncellendi");
        }

        private void Sil()
        {
            if (SeciliMusteri == null)
                return;
            _musteriService.DeleteMusteri(SeciliMusteri);
            Listele();
            Temizle();
            MessageBox.Show("Müşteri silindi.");
        }
    }
}
