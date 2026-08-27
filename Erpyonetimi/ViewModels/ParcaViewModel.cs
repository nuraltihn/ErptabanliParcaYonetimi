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
using Microsoft.Win32;
using System.IO;
using System.Linq;

namespace Erpyonetimi.ViewModels
{
    public class ParcaViewModel: BaseViewModel
    {
        public ObservableCollection<Parca> Parcalar { get; set; } = new();
        public ObservableCollection<Kategori> Kategoriler { get; set; } = new();
        public ObservableCollection<Tedarikci> Tedarikciler { get; set; } = new();
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


     
    
        private Kategori? _secilikategori;
        public Kategori? SeciliKategori
        {
            get => _secilikategori;
            set
            {
                _secilikategori = value;
                OnPropertyChanged();
            }
        }
        private Tedarikci? _seciliTedarikci;
        public Tedarikci? SeciliTedarikci
        {
            get => _seciliTedarikci;
            set
            {
                _seciliTedarikci = value;
                OnPropertyChanged();
            }
        }
        private List<Parca> _tumParcalar = new();
        private string _aramaMetni = string.Empty;
      public string AramaMetni
        {
            get => _aramaMetni; 
            set {  _aramaMetni = value; OnPropertyChanged();
                Filtrele();
            }
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
                    SeciliKategori= Kategoriler?.FirstOrDefault(x=>x.Id== value.Kategori.Id);
                    SeciliTedarikci= Tedarikciler?.FirstOrDefault(x=>x.Id== value.Tedarikci.Id);
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
    
       public ICommand ParcaTemizleCommand { get; }
        public ICommand KritikStoklarCommand { get; }
     

        private readonly IParcaService _parcaService;
        private readonly IKategoriService _kategoriService;
        private readonly ITedarikciService _tedarikciService;
        
        public ParcaViewModel(IParcaService parcaService, IKategoriService kategoriService, ITedarikciService tedarikciService)
        {
            _parcaService = parcaService;
        _kategoriService = kategoriService;
            _tedarikciService = tedarikciService;
      
            EkleCommand = new RelayCommand(async()=> await Ekle());
            KritikStoklarCommand = new RelayCommand(KritikStoklariGetir);
            GuncelleCommand = new RelayCommand(async()=> await Guncelle());
            SilCommand = new RelayCommand(async()=> await Sil());
            ListeleCommand = new RelayCommand(async()=> await Listele());
            ParcaTemizleCommand = new RelayCommand(Temizle);
          
          
            OnPropertyChanged(nameof(CrudVisibility));
            _ = Yukle();
    
        }
  
        private async Task Yukle()
        {
            try {
                var katList = await _kategoriService.GetAllKategoriAsync();
                Kategoriler = new ObservableCollection<Kategori>(katList ?? new List<Kategori>());

                var tedList = await _tedarikciService.GetAllTedarikciAsync();
                Tedarikciler = new ObservableCollection<Tedarikci>(tedList ?? new List<Tedarikci>());
                OnPropertyChanged(nameof(Tedarikciler));
                OnPropertyChanged(nameof(Kategoriler));
                await Listele();
            }
            catch (Exception ex) {
                MessageBox.Show("Parça verileri yüklenirken hata oluştu");


            } }
        public Visibility CrudVisibility
        {
            get
            {
                return UserSession.IsAdmin && DatabaseHelper.IsConnected
                    ?Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        public Visibility SatisVisibility =>
         UserSession.CurrentUser?.Rol?.RolAdi == "Sistem Yöneticisi" ||
         UserSession.CurrentUser?.Rol?.RolAdi == "Satış Personeli"
         ? Visibility.Visible : Visibility.Collapsed;
        
        private async Task Ekle()
            
        {
            if (string.IsNullOrWhiteSpace(ParcAdi) || string.IsNullOrWhiteSpace(ParcaKodu))
            {
                MessageBox.Show("Lütfen gerekli verileri giriniz.");return; 
            }

            if (AlisFiyat < 0 || SatisFiyat < 0)
            {
                MessageBox.Show("Fiyat negatif olamaz");
                return;
            }
            if (MinimumStok > MevcutStok)
            {
                MessageBox.Show("Minimum stok mevcut stoktan büyük olamaz.");
                return;
            }
            var mevcut = await _parcaService.GetByKodAsync(ParcaKodu);
            if (mevcut !=null)
            {
                MessageBox.Show("Bu parça kodu zaten mevcut.");
                return;
            }
            if (SeciliKategori == null)
            {
                MessageBox.Show("Kategori seçiniz ");
                return;
            }
            if (SeciliTedarikci == null)
            {
                MessageBox.Show("Tedarikçi seçiniz");
                return;
            }
          
                
 
            var parca = new Parca
            {
                
                ParcaKodu = ParcaKodu,
                ParcAdi = ParcAdi,
                Marka = Marka,
           
                KategoriId = SeciliKategori.Id,
                TedarikciId = SeciliTedarikci.Id,
                AlisFiyat = AlisFiyat,
                SatisFiyat = SatisFiyat,
                MevcutStok = MevcutStok,
                MinimumStok = MinimumStok,
                Aciklama = Aciklama,
                
                
                
            };

            try
            {
              await  _parcaService.AddParcaAsync(parca);
                await Listele();
                MessageBox.Show("Parça eklendi.");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Temizle();
        }
        private void Filtrele()
        {
            var arama = AramaMetni?.Trim();

            var sonuc = _tumParcalar
                .Where(p =>
                    string.IsNullOrWhiteSpace(arama)
                    || (!string.IsNullOrWhiteSpace(p.ParcaKodu) &&
                        p.ParcaKodu.Contains(arama, StringComparison.OrdinalIgnoreCase))
                    || (!string.IsNullOrWhiteSpace(p.ParcAdi) &&
                        p.ParcAdi.Contains(arama, StringComparison.OrdinalIgnoreCase))
                    || (!string.IsNullOrWhiteSpace(p.Marka) &&
                        p.Marka.Contains(arama, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();

            Parcalar = new ObservableCollection<Parca>(sonuc);

            OnPropertyChanged(nameof(Parcalar));
        }
        private async Task Guncelle()
        {
            if (SeciliParca == null)
                return;
            if (string.IsNullOrWhiteSpace(ParcAdi) || string.IsNullOrWhiteSpace(ParcaKodu))
            {
                MessageBox.Show("Parça adı ve parça kodu zorunludur."); return;
            }
            if (AlisFiyat < 0 || SatisFiyat < 0)
            {
                MessageBox.Show("Fiyat negatif olamaz");
                return;
            }
            if (MevcutStok < 0 || MinimumStok < 0)
            {
                MessageBox.Show("Stok değerleri negatif  olamaz.");
                return;
            }
            if (MinimumStok > MevcutStok)
            {
                MessageBox.Show("Minimum stok mevcut stoktan büyük olamaz.");
                return;
            }
            if (SeciliKategori == null || SeciliTedarikci == null)
            {
                MessageBox.Show("Kategori ve tedarikçi seçiniz.");
                return;
            }

            var mevcut = await _parcaService.GetByKodAsync(ParcaKodu);
            if (mevcut != null && mevcut.Id != SeciliParca.Id)
            {
                MessageBox.Show("Bu parça kodu zaten mevcut");
                return;
            }

            SeciliParca.ParcaKodu = ParcaKodu;
            SeciliParca.ParcAdi = ParcAdi;
            SeciliParca.Marka = Marka;

            SeciliParca.KategoriId = SeciliKategori.Id;
            SeciliParca.TedarikciId = SeciliTedarikci.Id;
            SeciliParca.AlisFiyat = AlisFiyat;
            SeciliParca.SatisFiyat = SatisFiyat;
            SeciliParca.MevcutStok = MevcutStok;
            SeciliParca.MinimumStok = MinimumStok;
            SeciliParca.Aciklama = Aciklama;
         
            try
            {
                await _parcaService.UpdateParcaAsync(SeciliParca);
                await Listele();
                Temizle();


                MessageBox.Show("Parça güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Parça yüklenirken bir hata oluştu:\n{ex.Message}");
            }
        }

        private async Task Sil()
        {
            if (SeciliParca == null)
                return;
            var cevap = MessageBox.Show(
                "Seçili parçayı silmek istediğinize emin misiniz?",
                "Silme Onayı",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;

            try
            {
                await _parcaService.RemoveParcaAsync(SeciliParca);
                await Listele();
                Temizle();
                MessageBox.Show("Parça silindi");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Parça silinirken bir hata oluştu:\n{ex.Message}");
            }
        }


        private async Task Listele()
        {
            try
            {
                var parcalar = await _parcaService.GetAllParcaAsync();
                _tumParcalar = parcalar?.ToList() ?? new List<Parca>();
               
                Filtrele();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Parçalar yüklenirken hata oluştu:\n{ex.Message}");
            } }
        private async Task KritikStoklariGetir()
        {
            var parcalar = await _parcaService.GetAllParcaAsync();
            
            var kritikler = parcalar
                .Where(x => x.MevcutStok < x.MinimumStok);
            Parcalar = new ObservableCollection<Parca>(kritikler);
            OnPropertyChanged(nameof(Parcalar));
        }
        
        private void Temizle()
        {
            ParcaKodu = "";
            ParcAdi = "";
            Marka = "";
            Aciklama = "";
            AlisFiyat = 0;
            SatisFiyat = 0;
            MevcutStok = 0;
            MinimumStok = 0;
            SeciliKategori = null;
            SeciliTedarikci = null;
     
        }
    }
}
