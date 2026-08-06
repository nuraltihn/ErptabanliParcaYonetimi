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
    public class ParcaViewModel: BaseViewModel
    {
        public ObservableCollection<Parca> Parcalar { get; set; }
        public ObservableCollection<Kategori> Kategoriler { get; set; }
        public ObservableCollection<Tedarikci> Tedarikciler { get; set; }
        private string _parcaKodu = string.Empty;
        public string ParcaKodu { get => _parcaKodu; set { _parcaKodu = value; OnPropertyChanged(); } }
        private string _parcAdi = string.Empty;
        public string ParcAdi { get => _parcAdi; set { _parcAdi = value; OnPropertyChanged(); } }
        private string _marka = string.Empty;
        public string Marka { get => _marka; set { _marka = value; OnPropertyChanged(); } }
        private int _kategoriId;
        public int KategoriId { get => _kategoriId; set { _kategoriId = value; OnPropertyChanged(); } }
        private int _tedarikciId;
        public int TedarikciId { get => _tedarikciId; set { _tedarikciId = value; OnPropertyChanged(); } }
        private decimal _alisFiyat;
        public decimal AlisFiyat { get => _alisFiyat; set { _alisFiyat = value; OnPropertyChanged(); } }
        private decimal _satisFiyat;
        public decimal SatisFiyat { get => _satisFiyat; set { _satisFiyat = value; OnPropertyChanged(); } }
        private int _mevcutStok;
        public int MevcutStok { get => _mevcutStok; set { _mevcutStok = value; OnPropertyChanged(); } }
        private int _minimumStok;
        public int MinimumStok { get => _minimumStok; set { _minimumStok = value; OnPropertyChanged(); } }
        private string _aciklama = string.Empty;
        public string Aciklama { get => _aciklama; set { _aciklama = value; OnPropertyChanged(); } }
        private Kategori _secilikategori;
        public Kategori SeciliKategori
        {
            get => _secilikategori;
            set
            {
                _secilikategori = value;
                OnPropertyChanged();
            }
        }
        private Tedarikci _seciliTedarikci;
        public Tedarikci SeciliTedarikci
        {
            get => _seciliTedarikci;
            set
            {
                _seciliTedarikci = value;
                OnPropertyChanged();
            }
        }
        private string _aramaMetni = string.Empty;
      public string AramaMetni
        {
            get => _aramaMetni; set {  _aramaMetni = value; OnPropertyChanged(); }
        }
        private Parca? _seciliParca;
        public Parca? SeciliParca{
            get => _seciliParca;
            set { _seciliParca = value;
            if(value != null)
                {
                    ParcaKodu= value.ParcaKodu;
                    ParcAdi = value.ParcAdi;
                    Marka = value.Marka;
                    KategoriId= value.KategoriId;
                    TedarikciId = value.TedarikciId;
                    AlisFiyat = value.AlisFiyat;
                    SatisFiyat = value.SatisFiyat;
                    MevcutStok = value.MevcutStok;
                    MinimumStok = value.MinimumStok;
                    Aciklama = value.Aciklama;
                    OnPropertyChanged(nameof(ParcaKodu));
                    OnPropertyChanged(nameof(ParcAdi));
                    OnPropertyChanged(nameof(Marka));
                    OnPropertyChanged(nameof(KategoriId));
                    OnPropertyChanged(nameof(TedarikciId));
                    OnPropertyChanged(nameof(AlisFiyat));
                    OnPropertyChanged(nameof(SatisFiyat));
                    OnPropertyChanged(nameof(MevcutStok));
                    OnPropertyChanged(nameof(MinimumStok));
                    OnPropertyChanged(nameof(Aciklama));

                }
                OnPropertyChanged();
            }

            
        }
        public ICommand EkleCommand { get; }
        public ICommand GuncelleCommand { get; }
        public ICommand SilCommand { get; }

        public ICommand ListeleCommand { get; }
        public ICommand AraCommand { get; }
        public ICommand StokGirisCommand { get; }
        public ICommand StokCikisCommanet { get; }

        private readonly IParcaService _parcaService;
        public ParcaViewModel(IParcaService parcaService)
        {
            _parcaService = parcaService;
            Parcalar = new ObservableCollection<Parca>(
                _parcaService.GetAllParca());
            EkleCommand = new RelayCommand(Ekle);
            GuncelleCommand = new RelayCommand(Guncelle);
            SilCommand = new RelayCommand(Sil);
            ListeleCommand = new RelayCommand(Listele);
            AraCommand = new RelayCommand(Ara);

            OnPropertyChanged(nameof(CrudVisibility));
            
        }

        public Visibility CrudVisibility
        {
            get
            {
                return UserSession.IsAdmin
                    ?Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        
        private void Ekle()
        {
            MessageBox.Show(
    $"ParcaKodu = {ParcaKodu}\n" +
    $"ParcAdi = {ParcAdi}"
);
            MessageBox.Show($"kategoriId:{KategoriId}");
            var parca = new Parca
            {
                ParcaKodu = ParcaKodu,
                ParcAdi = ParcAdi,
                Marka = Marka,
                KategoriId = 1,
                TedarikciId = 1,
                AlisFiyat = AlisFiyat,
                SatisFiyat = SatisFiyat,
                MevcutStok = MevcutStok,
                MinimumStok = MinimumStok,
                Aciklama = Aciklama
            };

            _parcaService.AddParca(parca);
            Parcalar.Add(parca);
            MessageBox.Show("parça eklendi");
        }

        private void Guncelle()
        {
            if (SeciliParca == null)
                return;
            SeciliParca.ParcaKodu = ParcaKodu;
            SeciliParca.ParcAdi = ParcAdi;
            SeciliParca.Marka = Marka;
            SeciliParca.KategoriId = KategoriId;
            SeciliParca.TedarikciId= TedarikciId;
            SeciliParca.AlisFiyat= AlisFiyat;
            SeciliParca.SatisFiyat = SatisFiyat;
            SeciliParca.MevcutStok = MevcutStok;
            SeciliParca.MinimumStok = MinimumStok;
            SeciliParca.Aciklama= Aciklama;

            _parcaService.UpdateParca(SeciliParca);
            
            Parcalar = new ObservableCollection<Parca>(
                _parcaService.GetAllParca());
            OnPropertyChanged(nameof(Parcalar));
            MessageBox.Show("Parça güncellendi");

        }

        private void Sil()
        {
            if (SeciliParca == null)
                return;
            _parcaService.RemoveParca(SeciliParca);
            Parcalar.Remove(SeciliParca);
            MessageBox.Show("Parça silindi");
        }

        private void Ara()
        {
            var sonuc = _parcaService.GetAllParca()
                .Where(x => x.ParcAdi.Contains(AramaMetni));

            Parcalar = new ObservableCollection<Parca>(sonuc);
            OnPropertyChanged(nameof(Parcalar));

        }
        private void Listele()
        {
            Parcalar = new ObservableCollection<Parca>(
                _parcaService.GetAllParca());
            OnPropertyChanged(nameof(Parcalar));
        }
    }
}
