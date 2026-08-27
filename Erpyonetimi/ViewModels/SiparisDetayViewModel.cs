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
    public class SiparisDetayViewModel : BaseViewModel
    {
        private readonly ISiparisDetayService _siparisDetayService;
        private readonly IParcaService _parcaService;
        private readonly ISiparisService _siparisService;
        public ObservableCollection<SiparisDetaylari> Detaylar { get; set; }
        public ObservableCollection<Parca> Parcalar { get; set; }
        public ObservableCollection<Siparis> Siparisler { get; set; }
        private List<SiparisDetaylari> _tumDetaylar = new();
        private int _miktar;
        public int Miktar
        {
            get => _miktar;
            set
            {
                _miktar = value;
                ToplamFiyat = BirimFiyat * value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToplamFiyat));
            }
        }

      
        private string _aramaMetni = "";
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
        private decimal _birimFiyat;
        public decimal BirimFiyat
        {
            get => _birimFiyat;
            set {
                _birimFiyat = value;
                ToplamFiyat = Miktar * value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ToplamFiyat));
            }
        }

        private decimal _toplamFiyat;
        public decimal ToplamFiyat
        {
            get => _toplamFiyat;
            set {  _toplamFiyat = value;
                OnPropertyChanged();
            }
        }
        private SiparisDetaylari? _seciliDetay;
        public SiparisDetaylari? SeciliDetay
        {
            get => _seciliDetay;
            set
            {
                _seciliDetay = value;
                if (value != null)
                {
                   
                    Miktar = value.Miktar;
                    BirimFiyat = value.BirimFiyat;
                    ToplamFiyat = value.ToplamFiyat;
                    SeciliParca = Parcalar?.FirstOrDefault(x => x.Id == value.ParcaId);
                 

                
                    OnPropertyChanged(nameof(SeciliParca));
                    OnPropertyChanged(nameof(Miktar));
                    OnPropertyChanged(nameof(BirimFiyat));
                    OnPropertyChanged(nameof(ToplamFiyat));
                }
                OnPropertyChanged();
            }
        }
        private Parca? _seciliParca;
        public Parca? SeciliParca
        {
            get => _seciliParca;
            set
            {
                _seciliParca = value;
                if (value != null)
                {
                    BirimFiyat = value.SatisFiyat;
                    ToplamFiyat = Miktar * BirimFiyat;
                }
                OnPropertyChanged();
            }
        }
        private Siparis? _seciliSiparis;
        public Siparis? SeciliSiparis
        {
            get => _seciliSiparis;
            set
            {
                _seciliSiparis = value;
                OnPropertyChanged();
                if (value != null)
                {
                    _=Listele();
                }
            }
        }
    

        public ICommand SiparisDetayEkleCommand { get; }
        public ICommand SiparisDetayGuncelleCommand { get; }
        public ICommand SiparisDetaySilCommand { get; }
        public ICommand SiparisDetayListeleCommand { get; }
        public ICommand SiparisDetayTemizleCommand { get; }
        public SiparisDetayViewModel(ISiparisDetayService siparisDetayService, ISiparisService siparisService, IParcaService parcaService)
        {
           _siparisService = siparisService;
            _siparisDetayService = siparisDetayService;
            _parcaService = parcaService;
            Detaylar = new ObservableCollection<SiparisDetaylari>();
            Parcalar = new ObservableCollection<Parca>();
            Siparisler = new ObservableCollection<Siparis>();
            SiparisDetayEkleCommand = new RelayCommand(Ekle);
            SiparisDetayGuncelleCommand = new RelayCommand(Guncelle);
            SiparisDetaySilCommand = new RelayCommand(Sil);
            SiparisDetayListeleCommand = new RelayCommand(Listele);
            SiparisDetayTemizleCommand = new RelayCommand(Temizle);

            _ = Yukle();

        }
        private async Task Yukle()
        {
            var parcalar = await _parcaService.GetAllParcaAsync();
            Parcalar = new ObservableCollection<Parca>(
                parcalar ?? new List<Parca>());
            var siparisler = await _siparisService.GetAllAsync();
            Siparisler = new ObservableCollection<Siparis>(
               siparisler ?? new List<Siparis>());
          
            var detaylar = await _siparisDetayService.GetAllAsync();
            _tumDetaylar = detaylar ?? new List<SiparisDetaylari>();
            Detaylar = new ObservableCollection<SiparisDetaylari>(_tumDetaylar);
            OnPropertyChanged(nameof(Parcalar));
            OnPropertyChanged(nameof(Siparisler));
            OnPropertyChanged(nameof(Detaylar));
        }
        public Visibility CrudVisibility
        {
            get
            {
                return UserSession.IsAdmin || UserSession.IsSatis
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void Filtrele()
        {
            if (string.IsNullOrWhiteSpace(AramaMetni))
            {
                Detaylar = new ObservableCollection<SiparisDetaylari>(_tumDetaylar);
                OnPropertyChanged(nameof(Detaylar));
                return;
            }
            var arama = AramaMetni.ToLower();
            var sonuc = _tumDetaylar
                .Where(x => (x.Parca?.ParcAdi?.ToLower().Contains(arama) ?? false) ||
                            (x.Parca?.ParcaKodu?.ToLower().Contains(arama) ?? false) ||
                            (x.Siparis?.SiparisNo?.ToLower().Contains(arama) ?? false) ||
                            (x.Siparis?.Musteri?.Soyad?.ToLower().Contains(arama) ?? false))
                .ToList();
            Detaylar = new ObservableCollection<SiparisDetaylari>(sonuc);
            OnPropertyChanged(nameof(Detaylar));
        }
        private async Task Listele()
        {
            if(SeciliSiparis==null)
                return;
            _tumDetaylar = await _siparisDetayService.GetAllAsync();
            Detaylar = new ObservableCollection<SiparisDetaylari>(_tumDetaylar
                .Where(x=>x.SiparisId == SeciliSiparis.Id));

            OnPropertyChanged(nameof(Detaylar));
        }
        private async Task Ekle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }

            if (SeciliSiparis == null)
            {
                MessageBox.Show("Sipariş seçiniz.");
                return;
            }
            if (SeciliParca == null)
            {
                MessageBox.Show("Parça seçiniz.");
                return;
            }
            if (Miktar <= 0)
            {
                MessageBox.Show("Miktar giriniz");
                return;

            }
            if (SeciliParca.MevcutStok < Miktar)
            {
                MessageBox.Show("Yeterli stok yok");
                return;
            }
            var detay = new SiparisDetaylari
            {
                SiparisId = SeciliSiparis.Id,
                ParcaId = SeciliParca.Id,
                Miktar = Miktar,
                BirimFiyat = SeciliParca.SatisFiyat,
                ToplamFiyat = Miktar * SeciliParca.SatisFiyat
            };
            await _siparisDetayService.AddDetayAsync(detay);

            SeciliParca.MevcutStok -= Miktar;
            await _parcaService.UpdateParcaAsync(SeciliParca);
            await Listele();
           

            var siparis = await _siparisService.GetByIdAsync(SeciliSiparis.Id);
            if(siparis != null)
            {
                var detaylar = await _siparisDetayService.GetAllAsync();
                siparis.ToplamTutar = detaylar
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
                await _siparisService.UpdateSiparisAsync(siparis);
            }
            MessageBox.Show("Sipariş Hareketi eklendi");
        }
        private void Temizle()
        {
            Miktar = 0;
            BirimFiyat = 0;
            ToplamFiyat = 0;
            SeciliSiparis = null;
              SeciliParca = null;


        }
        private async Task Guncelle()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }

            if (SeciliDetay==null || SeciliParca == null||SeciliSiparis==null)
                return;
            var eskiParca = await _parcaService.GetByIdAsync(SeciliDetay.ParcaId);

            if (eskiParca == null)
            {
                MessageBox.Show("Eski parça bulunamadı.");
                return;
            }

            // Eski parçanın siparişten düşen miktarını geri ver
            eskiParca.MevcutStok += SeciliDetay.Miktar;

            if (SeciliParca.Id == eskiParca.Id)
            {
                // Aynı parça seçildiyse eski miktarı geri verdiğimiz
                // için yeni miktarı buradan kontrol ediyoruz.
                if (eskiParca.MevcutStok < Miktar)
                {
                    MessageBox.Show("Yeterli stok yok.");
                    return;
                }

                eskiParca.MevcutStok -= Miktar;

                await _parcaService.UpdateParcaAsync(eskiParca);
            }
            else
            {
                // Yeni parça farklıysa yeni parçanın stok kontrolü
                if (SeciliParca.MevcutStok < Miktar)
                {
                    MessageBox.Show("Yeterli stok yok.");
                    return;
                }

                await _parcaService.UpdateParcaAsync(eskiParca);

                SeciliParca.MevcutStok -= Miktar;

                await _parcaService.UpdateParcaAsync(SeciliParca);
            }
            
            SeciliDetay.ParcaId = SeciliParca.Id;
            SeciliDetay.Miktar = Miktar;
            SeciliDetay.BirimFiyat= BirimFiyat;
            SeciliDetay.ToplamFiyat = Miktar * BirimFiyat;
           await _siparisDetayService.UpdateDetayAsync(SeciliDetay);
            await Listele();
            var siparis = await _siparisService.GetByIdAsync(SeciliSiparis.Id);
            if (siparis != null)
            {
                var detaylar = await _siparisDetayService.GetAllAsync();
                siparis.ToplamTutar = detaylar
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
               await _siparisService.UpdateSiparisAsync(siparis);
            }
            MessageBox.Show("Sipariş detayı güncellendi");
         

        }
        private async Task Sil()
        {
            DatabaseHelper.CheckConnection();

            if (!DatabaseHelper.IsConnected)
            {
                MessageBox.Show("Veritabanı bağlantısı yok. Bu işlem yapılamaz.");
                return;
            }
            if (SeciliDetay == null||SeciliParca == null)
                return;
            var cevap = MessageBox.Show(
                "Seçili sipariş detayını silmek ister misiniz?",
                "Silme Onayı", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (cevap != MessageBoxResult.Yes)
                return;
            SeciliParca.MevcutStok += SeciliDetay.Miktar;
            await _parcaService.UpdateParcaAsync(SeciliParca);
            await _siparisDetayService.DeleteDetayAsync(SeciliDetay);
            

          
            
            
           ;
            
            await _siparisDetayService.DeleteDetayAsync(SeciliDetay);
       
            
            if(SeciliSiparis == null)
                return;
            await Listele();
            var siparis = await _siparisService.GetByIdAsync(SeciliSiparis.Id);
            if (siparis != null)
            {
                var detaylar = await _siparisDetayService.GetAllAsync();
                siparis.ToplamTutar =detaylar
                    .Where(x => x.SiparisId == siparis.Id)
                    .Sum(x => x.ToplamFiyat);
               await _siparisService.UpdateSiparisAsync(siparis);
            }
            await Listele();
            MessageBox.Show("Sipariş Hareketi silindi");
        }

       

    }
}
